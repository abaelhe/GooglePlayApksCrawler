using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayStoreWebAPI.Models
{
    public class MobileAppResponse
    {
        public IList<AppModel> appsFound     { get; set; }
        public bool            error         { get; set; }
        public string          statusMessage { get; set; }
    }
}