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
    [Serializable]
    [DataContract(IsReference = true)]
    public class Realm : GameElement
    {
        private static readonly RatLogger log = LogUtil.Instance.GetLogger(typeof(Realm));
        
        /// <summary>
        /// Instantiates a new instance with the specified GameID value and data adapter.  If
        /// the adapter is not null and contains data for Realms, the new object is hydrated
        /// using its internal result set.
        /// </summary>
        public Realm()
        {

        }

        [DataMember]
        public virtual List<Region> Regions { get; protected set; }

        public virtual void AddRegion(Region region)
        {
            if (region != null)
            {
                InitializeList(Regions);
                Regions.Add(region);
            }
        }

        public virtual bool RemoveRegion(Region region)
        {
            InitializeList(Regions);
            return Regions.Remove(region);
        }
        
    }
}
