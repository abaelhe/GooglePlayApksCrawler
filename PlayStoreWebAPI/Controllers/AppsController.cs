using Newtonsoft.Json.Serialization;
using PlayStoreWebAPI.Models;
using PlayStoreWebAPI.Repository;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Helpers;
using System.Web.Http;

namespace PlayStoreWebAPI.Controllers
{
    public class AppsController : ApiController
    {
        // GET api/apps?id=app_id
        public HttpResponseMessage Get (string id)
        {
            MobileAppResponse response = new MobileAppResponse ();

            // List of Apps Found with that given ID
            List<AppModel> result = new List<AppModel> ();

            using (MongoDBRepository mongoHandler = new MongoDBRepository ())
            {
                // Is the App Already on The Database ?            
                if (mongoHandler.IsAppOnTheDatabase(id))
                {
                    response.error         = true;
                    response.statusMessage = "App Received Is Already On the Database. Won't be processed again";
                    response.appsFound     = null;

                    // NotModified Response Code
                    return Request.CreateResponse (HttpStatusCode.NotModified, response, GetFormatter ());
                }

                // Finding The Apps
                result.AddRange (mongoHandler.FindAppsByID (id));
            }

            // Filling Up Web Response
            response.error         = false;
            response.statusMessage = String.Empty;
            response.appsFound     = result;
            return Request.CreateResponse (HttpStatusCode.OK, response, GetFormatter ());
        }

        private static JsonMediaTypeFormatter GetFormatter()
        {
            var formatter = new JsonMediaTypeFormatter ();
            var json      = formatter.SerializerSettings;

            json.DateFormatHandling   = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            json.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            json.NullValueHandling    = Newtonsoft.Json.NullValueHandling.Ignore;
            json.Formatting           = Newtonsoft.Json.Formatting.Indented;
            json.Culture              = new CultureInfo ("en-US");
            json.ContractResolver     = new CamelCasePropertyNamesContractResolver ();
            return formatter;
        }
    }
}