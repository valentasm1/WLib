using System;
using System.Collections.Generic;
using System.Text;
using Realms;

namespace WLib.Core.Mobile.Services.Data
{
    public interface IRepository
    {
        Realm Instance { get; }
    }


    /// <summary>
    /// By default it delete if migration needed
    /// </summary>
    public class Repository : IRepository
    {
        public Realm Instance { get; }

        private RealmConfiguration _configuration = null;
        public virtual RealmConfiguration Configuration
        {
            get
            {
                if (_configuration != null) return _configuration;

                _configuration = new RealmConfiguration();
                _configuration.ShouldDeleteIfMigrationNeeded = true;

                return _configuration;
            }
        }

        public Repository()
        {


            Instance = Realm.GetInstance(Configuration);
        }
    }
}
