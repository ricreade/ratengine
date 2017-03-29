using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataSource;

namespace RatEngine.DataModel.World
{
    /// <summary>
    /// This class represents a collection of Rooms into a Region.  The Rooms in a Region typically
    /// share common characteristcis, such as being part of the same town, or part of the same dungeon.
    /// This class largely serves as a way to organize Rooms into groups so they can be easily
    /// categorized.  It also allows Rooms from different Regions to share the same name.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Region : GameElement
    {
        protected Region()
        {

        }

        /// <summary>
        /// Instantiates a new instance with the specified GameID value and data adapter.  If
        /// the adapter is not null and contains data for Regions, the new object is hydrated
        /// using its internal result set.
        /// </summary>
        public Region(Realm realm)
        {
            Realm = realm;
        }

        [DataMember]
        public virtual Realm Realm { get; protected set; }

        [DataMember]
        public virtual List<Room> Rooms { get; protected set; }

        public virtual void AddRoom(Room room)
        {
            if (room != null)
            {
                InitializeList(Rooms);
                Rooms.Add(room);
            }
        }

        public virtual bool RemoveRoom(Room room)
        {
            InitializeList(Rooms);
            return Rooms.Remove(room);
        }
    }
}
