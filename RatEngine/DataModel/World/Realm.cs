using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using LoggingEngine;
using RatEngine.DataSource;

namespace RatEngine.DataModel.World
{
    /// <summary>
    /// This class represents a collection of regions and organizes them together in a structured way.
    /// The Realm represents the overall game map and as such, any given game should have only one
    /// Realm.  This provides a way to introduce versions of the same game at different difficulties
    /// or completely different games using the same engine.
    /// </summary>
    [DataContract]
    public class Realm : GameElement
    {
        // A collection of all Regions in the Realm.
        private ConcurrentDictionary<Guid, Region> _regions;
        private static readonly RatLogger log = LogUtil.Instance.GetLogger(typeof(Realm));
        
        /// <summary>
        /// Instantiates a new instance with the specified GameID value and data adapter.  If
        /// the adapter is not null and contains data for Realms, the new object is hydrated
        /// using its internal result set.
        /// </summary>
        public Realm(RatDataModelAdapter Adapter) : base(Adapter)
        {
            //instantiate Dictionary of Regions
            _regions = new ConcurrentDictionary<Guid, Region>();
            LoadFromAdapter(_adapter);
        }

        [DataMember]
        public IEnumerable<Region> Regions
        {
            get { return _regions.Select(item => item.Value); }
        }

        public override bool Delete(RatDataModelAdapter Adapter)
        {
            try
            {
                Adapter.Delete(RatDataModelType.Realm, new List<DataParameter>() {
                    new DataParameter(RatDataModelAdapter.RealmParameters.ID, ID) });
            }
            catch (Exception ex)
            {
                log.Error(string.Format("The attempt to delete realm '{0}' failed.", Name), ex);
                return false;
            }
            return true;
        }

        public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        {
            if (Adapter != null && Adapter.LastRetrievedModel == RatDataModelType.Realm && Adapter.ResultSet.RecordCount > 0)
            {
                _id = Adapter.ResultSet.GetValue<int>(RatDataModelAdapter.RealmFields.ID);
                _name = Adapter.ResultSet.GetValue<string>(RatDataModelAdapter.RealmFields.NAME);
                _descr = Adapter.ResultSet.GetValue<string>(RatDataModelAdapter.RealmFields.DESCRIPTION);
                base.LoadFromAdapter(Adapter);
            }
        }

        /// <summary>
        /// Loads all regions associated with this realm.
        /// </summary>
        public void LoadRegions()
        {
            RatDataModelAdapter a = new RatDataModelAdapter();
            a.Retrieve(RatDataModelType.Region, null);

            for (int i = 0; i < a.ResultSet.RecordCount; i++)
            {
                a.ResultSet.MoveToRecord(i);
                Region reg = new Region(a, this);
                _regions.TryAdd(reg.GameID, reg);
            }
            
        }

        public void ResolveTransitionReferences()
        {
            foreach (Region reg in Regions)
            {
                reg.ResolveTransitionReferences();
            }
        }

        public override bool Save(RatDataModelAdapter Adapter)
        {
            try
            {
                List<DataParameter> parameters = new List<DataParameter>();
                parameters.Add(new DataParameter(RatDataModelAdapter.RealmParameters.NAME, Name));
                parameters.Add(new DataParameter(RatDataModelAdapter.RealmParameters.DESCRIPTION, Description));

                if (ID > 0)
                {
                    parameters.Add(new DataParameter(RatDataModelAdapter.RealmFields.ID, ID));
                }
                Adapter.Save(RatDataModelType.Realm, parameters);
                _id = Adapter.NewRecordID;
                _gameid = Adapter.NewGameID;
                //LoadFromAdapter(Adapter);
                return ID > 0;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("The attempt save Realm '{0}' failed.", Name), ex);
                return false;
            }
        }
    }
}
