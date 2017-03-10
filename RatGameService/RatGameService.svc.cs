using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using RatEngine.DataModel;
using RatEngine.DataModel.World;
using RatEngine.DataSource;

namespace RatGameService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class RatGameService: IRatGameService
    {
        public List<Realm> GetRealmList()
        {

            Realm realm = null;
            List<Realm> realms = new List<Realm>();
            RatDataModelAdapter adapter = new RatDataModelAdapter();
            adapter.Retrieve(RatDataModelType.Realm, new List<DataParameter>());
            for (int i = 0; i < adapter.ResultSet.RecordCount; i++)
            {
                adapter.ResultSet.MoveToRecord(i);
                realm = new Realm(adapter);
                realms.Add(realm);
            }
            //return new List<Realm>() { new Realm("Test Realm", "My test realm") };
            return realms;
        }

        public List<string> GetRealmNames()
        {
            Realm realm = null;
            List<Realm> realms = new List<Realm>();
            RatDataModelAdapter adapter = new RatDataModelAdapter();
            adapter.Retrieve(RatDataModelType.Realm, new List<DataParameter>());
            for (int i = 0; i < adapter.ResultSet.RecordCount; i++)
            {
                adapter.ResultSet.MoveToRecord(i);
                realm = new Realm(adapter);
                realms.Add(realm);
            }
            return realms.ConvertAll(item => item.Name);
        }
    }
}
