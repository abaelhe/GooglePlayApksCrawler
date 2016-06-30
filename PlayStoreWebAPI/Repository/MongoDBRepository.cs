using NLog;
using SharedLibrary;
using SharedLibrary.Models;
using SharedLibrary.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayStoreWebAPI.Repository
{
    public class MongoDBRepository : IDisposable
    {
        private Logger          _logger;
        private MongoDBWrapper _mongoDB;

        // Static Constructor
        public MongoDBRepository()
        {
            _logger = LogManager.GetCurrentClassLogger ();

            _mongoDB = new MongoDBWrapper ();
            string fullServerAddress = String.Join (":", Consts.MONGO_SERVER, Consts.MONGO_PORT);
            _mongoDB.ConfigureDatabase (Consts.MONGO_USER, Consts.MONGO_PASS, Consts.MONGO_AUTH_DB, fullServerAddress, Consts.MONGO_TIMEOUT, Consts.MONGO_DATABASE, Consts.MONGO_COLLECTION);
        }

        public bool IsAppOnTheDatabase(string appId)
        {
            return _mongoDB.AppProcessedById (appId);
        }

        public IEnumerable<AppModel> FindAppsByID(string appId)
        {
            return _mongoDB.FindAllById (appId);
        }

        public void Dispose()
        {
            _mongoDB = null;
        }
    }
}