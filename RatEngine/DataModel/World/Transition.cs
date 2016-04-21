using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatEngine.DataModel.Effects;

namespace RatEngine.DataModel.World
{
    /// <summary>
    /// This class defines a transition between Rooms.  This class provides PCs and NPCs with
    /// a means to travel from room to room.  The class supports flag challenges, so some
    /// transitions may not be easy to use.  Transition properties define the descriptions users
    /// receive when another user enters or leaves a room.
    /// </summary>
    public class Transition : Effectable
    {
        // Database field names.
        public struct Fields
        {
            public const string ID = "ID";
            public const string NAME = "Name";
            public const string DESC = "Description";
            public const string DESCFROM = "descriptionFrom";
            public const string DESCTO = "descriptionTo";
            public const string KEYWORD = "KeyWord";
            public const string ROOM_FROM = "fkRoomFrom";
            public const string ROOM_TO = "fkRoomTo";
        }

        // Database stored procedures.
        public struct StoredProcedures
        {
            public const string SELECT = "";
            public const string SELECTALL = "mspGetRoomTransitions";
            public const string DELETE = "";
            public const string INSERT = "";
            public const string UPDATE = "";
        }

        public struct SPArguments
        {
            public const string ID = "@RoomID";
        }

        /// <summary>
        /// Constructor
        /// This default constructor is provided for simple testing purposes.  It is not used in
        /// production.
        /// </summary>
        public Transition(string GameID) : base(GameID)
        {
            InitializeComponents();
        }

        /// <summary>
        /// Constructor
        /// Initializes this Transition from a database record.  Because this object must have been
        /// created from a Room, that Room reference is provided as well to save a step during
        /// initialization.  Throws a NullReferenceException if a null value is passed for StartingRoom.
        /// Throws a NullReferenceException if a null value is passed for Row.
        /// </summary>
        /// <param name="Row">[DataRow] The record from which to initialize this Transition.</param>
        /// <param name="StartingRoom">[Room] The room used to access this Transition.</param>
        public Transition(string GameID, DataRow Row, Room StartingRoom) : base(GameID)
        {
            InitializeComponents();

            if (StartingRoom != null)
                _roomfrom = StartingRoom;
            else
                throw new NullReferenceException("The starting room of a transition cannot be null.");

            if (Row != null)
                LoadDataRow(Row);
            else
                throw new NullReferenceException("The DataRow record for a Transition was null.  " +
                    "Cannot initialize the Transition.");

            // Load Effects

            // Load Flags
        }


        // The Room this transition leads from.  This room will display this transition keyword as
        // a potential exit and only Combatants in this Room may use this transition.  Players in this
        // Room will receive a broadcast if they see a player leave through this transition.
        private Room _roomfrom;

        // The Room this transition leads to.  This transition is not accessible to Combatants from
        // this room.  Players in this room will receive a broadcast if they see a player enter through
        // this transition.
        private Room _roomto;

        // The string description players in RoomFrom will receive when they see someone leave through
        // this transition.
        private string _descriptfrom;

        // The string description players in RoomTo will receive when they see someone enter from
        // this transition.
        private string _descriptto;

        // The keyword a player must use with the move command to enter this transition.
        private string _kywrd;

        public Room RoomFrom
        {
            get { return _roomfrom; }
        }

        public Room RoomTo
        {
            get { return _roomto; }
        }

        public string DescriptionFrom
        {
            get { return _descriptfrom; }
        }

        public string DescriptionTo
        {
            get { return _descriptto; }
        }

        public string Keyword
        {
            get { return _kywrd; }
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public void InitializeComponents()
        {

        }

        /// <summary>
        /// LoadDataRow
        /// Populates the object properties from values in a database record.  Any failed property
        /// initialization will throw an exception, forcing all values to be initialized correctly
        /// for the application to proceed.  The Room from which users access this transition is
        /// provided by the constructor (though it is check against the database record in this
        /// method).  The Room ID targeted by this Transition is stored in a temporary Room object
        /// which will be replaced by a reference to the actual room as stored in the appropriate
        /// Region.  This is because all Rooms must be initialized before this link can be made, and
        /// because it is possible for a Transition to join Rooms from two different Regions.
        /// </summary>
        /// <param name="Row">[DataRow] The database record from which this object will be hydrated.</param>
        public override void LoadDataRow(DataRow Row)
        {
            int tmp = 0;
            try
            {
                PopulatePropertyFromDataRow<int>(Row, Fields.ID, out this._id);
                PopulatePropertyFromDataRow<string>(Row, Fields.NAME, out this._name);
                PopulatePropertyFromDataRow<string>(Row, Fields.DESC, out this._descr);
                PopulatePropertyFromDataRow<string>(Row, Fields.DESCFROM, out this._descriptfrom);
                PopulatePropertyFromDataRow<string>(Row, Fields.DESCTO, out this._descriptto);
                //PopulatePropertyFromDataRow<string>(Row, Fields.KEYWORD, out this._kywrd);

                // The starting room was obtained in the constructor, so this value should already be available.
                // Check it against the database record to verify that the object stored in the RoomFrom
                // property is correct.
                PopulatePropertyFromDataRow<int>(Row, Fields.ROOM_FROM, out tmp);
                if (RoomFrom.ID != tmp)
                    throw new OperationFailedException("The Room referenced when creating this transition " +
                        "does not match the value stored in the database.");

                // Get the target Room ID
                // The intent of this process is to get the room ID and store it somewhere convenient.
                // It will be necessary later to traverse the Realm and find the room in the realm that matches
                // the given ID.  Because all Rooms must be populated before that can happen, the target
                // room ID will have to be stored temporarily.  The object created below will be replaced
                // with a reference to the actual room in the realm.
                PopulatePropertyFromDataRow<int>(Row, Fields.ROOM_TO, out tmp);
                if (tmp > 0)
                    _roomto = new Room(null, tmp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// ResolveRoomReferences
        /// Navigates the world map and attempts to locate the target Room specified in the RoomTo
        /// property.  This method checks the ID value given in the RoomTo property and locates the
        /// appropriate Room in the world map.  If no room was found, this method throws a new
        /// OperationFailedException.
        /// </summary>
        /// <param name="WorldMap">[Realm] The game world.</param>
        public void ResolveRoomReferences(Realm WorldMap)
        {
            if (WorldMap != null)
            {
                foreach (Region reg in WorldMap.Regions)
                {
                    foreach (Room rm in reg.Rooms)
                    {
                        if (rm.ID == RoomTo.ID)
                        {
                            _roomto = rm;
                            return;
                        }
                    }
                }
                // foreach(Region reg in WorldMap.Regions)

                // Find the Room with the ID the matches the ID stored in RoomTo

                // Set RoomTo equal to that room.

                // If no match was found, throw a new OperationFailedException("Could not locate the " +
                //      "target room for Transition " + Name + " (" + ID + ").");
                throw new OperationFailedException("Could not locate the target room for Transition " +
                    Name + " (" + ID + ").");
            }
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
