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
        // A collection of all Rooms in this Region.  The key is the Room name.
        private ConcurrentDictionary<string, Room> _rooms;

        //private variable containing the Realm containing the Region. Gives a way to walk up to the Realm. instantiated in constructor.
        private Realm _realm;

        /// <summary>
        /// Instantiates a new instance with the specified GameID value and data adapter.  If
        /// the adapter is not null and contains data for Regions, the new object is hydrated
        /// using its internal result set.
        /// </summary>
        public Region(Realm realm)
        {
            //instantiate _rooms variable
            _rooms = new ConcurrentDictionary<string, Room>();

            if (realm != null)
                _realm = realm;
            else
                throw new NullReferenceException("The realm of a region cannot be null.");

            //LoadFromAdapter(_adapter);

        }

        [DataMember]
        public Realm Realm
        {
            get { return _realm; }
            set { }
        }

        [DataMember]
        public IEnumerable<Room> Rooms
        {
            get { return _rooms.Select(item => item.Value); }
            set { }
        }

        //public override RatDataModelAdapter DataAdapter
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public override bool Delete()
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool Delete(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        //{
        //    if (Adapter != null && Adapter.LastRetrievedModel == RatDataModelType.Region)
        //    {
        //        _id = Adapter.ResultSet.GetValue<int>(RatDataModelAdapter.RegionFields.ID);
        //        _name = Adapter.ResultSet.GetValue<string>(RatDataModelAdapter.RegionFields.NAME);
        //        _descr = Adapter.ResultSet.GetValue<string>(RatDataModelAdapter.RegionFields.DESCRIPTION);
        //    }
        //}
        
        /// <summary>
        /// LoadRooms
        /// Loads all Rooms in this Region from the database and hydrates them.  This method is only
        /// called at service start up.
        /// </summary>
        public void LoadRooms()
        {

            //Create instance of RecordManager for retrieving Data
            //RecordManager recordManager = new RecordManager();

            ////Create empty DataTable for results
            //DataTable dtResults = new DataTable();
            ////attempt to retrieve table
            //try
            //{
            //    //create IList<SqlParam> and sp name for SendReadRequest 
            //    IList<SqlParameter> sqlParams = new List<SqlParameter>();
            //    SqlParameter regionID = new SqlParameter("@RegionID", this._id);
            //    string spName = "mspGetRooms";

            //    //add SqlParam to Ilist
            //    sqlParams.Add(regionID);

            //    //fill table by calling SendReadRequest
            //    dtResults = recordManager.SendReadRequest(spName, sqlParams);

            //    //foreach loop to create and add each Room to _rooms list with the <Tkey> as name of Room
            //    foreach (DataRow row in dtResults.Rows)
            //    {
            //        Room newRoom;

            //        //create Room
            //        try
            //        {
            //            newRoom = new Room(null, row, this);//TODO: change parameters to what they need to be after Room class is implemented
            //            //add newRoom to _room list
            //            if (!_rooms.TryAdd(newRoom.Name, newRoom))
            //                throw new OperationFailedException("Room " + newRoom.Name +
            //                    " could not be added to Region " + this.Name + ".");
            //        }
            //        catch (Exception ex)
            //        {
            //            //Console.WriteLine(ex.ToString());
            //            throw;
            //        }
            //    }

            //}
            //catch (Exception e)
            //{
            //    //Console.WriteLine(e.ToString());//TODO: After testing, write Exception code for each Exception found
            //    throw;
            //}
        }

        public void ResolveTransitionReferences()
        {
            foreach (Room r in Rooms)
            {
                r.ResolveTransitionReferences();
            }
        }

        //public override bool Save()
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool Save(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
