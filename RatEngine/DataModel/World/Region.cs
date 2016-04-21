using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.World
{
    /// <summary>
    /// This class represents a collection of Rooms into a Region.  The Rooms in a Region typically
    /// share common characteristcis, such as being part of the same town, or part of the same dungeon.
    /// This class largely serves as a way to organize Rooms into groups so they can be easily
    /// categorized.  It also allows Rooms from different Regions to share the same name.
    /// </summary>
    public class Region : GameElement
    {
        // A collection of all Rooms in this Region.  The key is the Room name.
        private ConcurrentDictionary<string, Room> _rooms;

        //private variable containing the Realm containing the Region. Gives a way to walk up to the Realm. instantiated in constructor.
        private Realm _realm;

        // Database field names.
        public struct Fields
        {
            public const string ID = "Id";
            public const string NAME = "Name";
            public const string DESCRIPTION = "Description";
            public const string REALM = "fkRealm";
        }

        // Database stored procedures.
        public struct StoredProcedures
        {
            public const string SELECT = "";
            public const string DELETE = "";
            public const string INSERT = "";
            public const string UPDATE = "";
        }

        public struct SPArguments
        {
            public const string ID = "@RegionID";
        }

        /// <summary>
        /// Region(int regionID)
        /// Constructor will handle hydrating the region and instantiating the _rooms variable
        /// </summary>
        public Region(string GameID, DataRow regionRow, Realm regionRealm) : base(GameID)
        {
            //instantiate _rooms variable
            _rooms = new ConcurrentDictionary<string, Room>();

            if (regionRealm != null)
                _realm = regionRealm;
            else
                throw new NullReferenceException("The region of a room cannot be null.");

            if (regionRow != null)
                LoadDataRow(regionRow);
            else
                throw new NullReferenceException("The DataRow record for a Region was null.  " +
                    "Cannot initialize the Region.");


        }

        public Realm Realm
        {
            get { return _realm; }
        }

        public IEnumerable<Room> Rooms
        {
            get { return _rooms.Select(item => item.Value); }
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override void LoadDataRow(DataRow Row)
        {
            //parse through DataRow making sure data types are correct in each field
            try
            {
                PopulatePropertyFromDataRow<int>(Row, Fields.ID, out this._id);
                PopulatePropertyFromDataRow<string>(Row, Fields.NAME, out this._name);
                PopulatePropertyFromDataRow<string>(Row, Fields.DESCRIPTION, out this._descr);
                //check to see if _realm is equal to retrieved Realm
                int tmp = 0;
                PopulatePropertyFromDataRow<int>(Row, Fields.REALM, out tmp);

                if (_realm.ID != tmp)
                {
                    throw new Exception("Realm retrieved from the Database does not match Realm created");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //call loadRooms method to fill _rooms
            LoadRooms();
        }

        /// <summary>
        /// LoadRooms
        /// Loads all Rooms in this Region from the database and hydrates them.  This method is only
        /// called at service start up.
        /// </summary>
        public void LoadRooms()
        {
            //Create instance of RecordManager for retrieving Data
            RecordManager recordManager = new RecordManager();

            //Create empty DataTable for results
            DataTable dtResults = new DataTable();
            //attempt to retrieve table
            try
            {
                //create IList<SqlParam> and sp name for SendReadRequest 
                IList<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter regionID = new SqlParameter("@RegionID", this._id);
                string spName = "mspGetRooms";

                //add SqlParam to Ilist
                sqlParams.Add(regionID);

                //fill table by calling SendReadRequest
                dtResults = recordManager.SendReadRequest(spName, sqlParams);

                //foreach loop to create and add each Room to _rooms list with the <Tkey> as name of Room
                foreach (DataRow row in dtResults.Rows)
                {
                    Room newRoom;

                    //create Room
                    try
                    {
                        newRoom = new Room(null, row, this);//TODO: change parameters to what they need to be after Room class is implemented
                        //add newRoom to _room list
                        if (!_rooms.TryAdd(newRoom.Name, newRoom))
                            throw new OperationFailedException("Room " + newRoom.Name +
                                " could not be added to Region " + this.Name + ".");
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.ToString());
                        throw;
                    }
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());//TODO: After testing, write Exception code for each Exception found
                throw;
            }
        }

        public void ResolveTransitionReferences()
        {
            foreach (Room r in Rooms)
            {
                r.ResolveTransitionReferences();
            }
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
