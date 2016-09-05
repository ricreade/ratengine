using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Inventory;
using RatEngine.DataModel.Mob;
using RatEngine.DataSource;

namespace RatEngine.DataModel.World
{
    /// <summary>
    /// Room
    /// Class that represents a Room is the game map.  This class is a major source of coordination
    /// between a number of game objects that interact in game.  PlayerCharacters exist in the game
    /// via Rooms, NPCs encounter the players via Rooms, and Items exist only via Rooms.  The network
    /// of rooms provides the gameplay structure for the MUD.
    /// </summary>
    [DataContract]
    public class Room : Inventoried
    {
        
        /// <summary>
        /// Constructor.  This constructor exists for testing purposes only.  It has no function
        /// in production.  This constructor initializes the room collections.
        /// </summary>
        //public Room(string GameID) : base(GameID)
        //{
        //    InitializeComponents();
        //}

        ///// <summary>
        ///// Constructor.  This constructor exists to provide a means to create a dummy object
        ///// referencing only the record id.  This option is provided so that a Transition can
        ///// maintain a record of the ID of its target room until the reference to the actual
        ///// target room can be resolved.  A functional game Room cannot be created through
        ///// this method.  Room collections are not initialized.
        ///// </summary>
        ///// <param name="ID">[int] The database record primary key of this room.</param>
        //public Room(string GameID, int ID) : base(GameID)
        //{
        //    _id = ID;
        //}

        /// <summary>
        /// Constructor.  This constructor provides a means to hydrate the Room object from
        /// a database record.  If either the Row or Region are null, this method throws a new
        /// NullReferenceException.
        /// </summary>
        /// <param name="Row">[DataRow] The database record from which to hydrate the Room.</param>
        /// <param name="MapRegion">[Region] The Region for this room.</param>
        public Room(RatDataModelAdapter Adapter, Region MapRegion) : base(Adapter)
        {
            InitializeComponents();

            if (MapRegion != null)
                _region = MapRegion;
            else
                throw new NullReferenceException("The region of a room cannot be null.");

            LoadFromAdapter(_adapter);

            //if (Row != null)
            //    LoadDataRow(Row);
            //else
            //    throw new NullReferenceException("The DataRow record for a Room was null.  " +
            //        "Cannot initialize the Room.");
        }

        // A collection of all combatants (PCs and NPCs) currently in the room.  The key is the
        // Combatant name.
        private ConcurrentDictionary<string, Creature> _creatures;

        // A collection of all transitions available from this room.  The key is the Transition name.
        // This property is initialized at server startup and should never change while the application
        // is running.  For this reason, this object is kept encapsulated and a GetTransition method
        // is provided to prevent any tampering with the transition list.  Transition properties are
        // all read only.
        private ConcurrentDictionary<string, Transition> _transitions;

        // The Region that contains this Room.  This property provides a way to walk up the map
        // hierarchy.  It is initialized at service startup.
        private Region _region;

        [DataMember]
        public Region Region
        {
            get { return _region; }
        }

        [DataMember]
        public IEnumerable<Creature> Creatures
        {
            get { return _creatures.Select(item => item.Value); }
        }

        [DataMember]
        public IEnumerable<Transition> Transitions
        {
            get { return _transitions.Select(item => item.Value); }
        }

        /// <summary>
        /// AddCombatant
        /// Adds a Combatant to the room.  This is typically called when a player logs in, when the
        /// service starts (to load NPCs), or when a PC or NPC enters the room via a Transition.
        /// This method should verify that the Combatant does not already exist and handle cases of
        /// key collisions (such as when a PC has the same name as an NPC).
        /// </summary>
        /// <param name="NewCombatant">[Combatant] The new combatant to add to the collection.</param>
        public bool AddCombatant(Creature NewCombatant)
        {
            if (NewCombatant != null)
            {
                if (_creatures.ContainsKey(NewCombatant.Name))
                {
                    return false;
                    //throw new OperationFailedException("The combatant name " + NewCombatant.Name +
                    //    " already exists in this room.");
                }
                return _creatures.TryAdd(NewCombatant.Name, NewCombatant);
                //if (_combatants.TryAdd(NewCombatant.Name, NewCombatant))
                //{
                //    return true;
                //    //throw new OperationFailedException("The combatant name " + NewCombatant.Name +
                //    //    " could not be added to the room.");
                //}
                //else
                //    return false;
            }
            else
                return false;
            //throw new OperationFailedException("Cannot add null Combatant to room.");
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override bool Delete(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetElement
        /// Returns a game element from the room.  This will be either a PC, an NPC, an Item, or a
        /// transition.  This is intended to provide a convenient way for the Examine command to
        /// return information about anything referenced in the room without necessarily needing to
        /// know what it is.  This method should search Combatants, then Items, then Transitions to
        /// find a match and return the best possible match.  If no match is found, the method
        /// should return null.
        /// </summary>
        /// <param name="ElementName">[string] A keyword string of the GameElement name property.
        /// Since this is really a keyword, the method should make the best possible effort to perform
        /// partial matches against the available dictionary keys.</param>
        /// <returns>[GameElement] The GameElement object found, or null if no match was found.</returns>
        public GameElement GetElement(string ElementName)
        {
            // Search the Combatant collection
            if (_creatures.ContainsKey(ElementName))
            {
                return _creatures[ElementName];
            }

            // Search the Transition collection
            if (_transitions.ContainsKey(ElementName))
            {
                return _transitions[ElementName];
            }

            // Search the Item collection
            if (_inv.ContainsKey(ElementName))
            {
                return _inv[ElementName];
            }

            // The element was not found.
            return null;
        }

        /// <summary>
        /// GetTransition
        /// Returns a Transition available from the room.  This will be either a PC or an NPC.  This 
        /// method should search Transitions to find a match and return the best possible match.  If 
        /// no match is found, the method should return null. Transitions might be known by either its
        /// name or keyword, so this method should check both.
        /// </summary>
        /// <param name="TransitionKeyword">[string] A keyword string of the Transition name or keyword
        /// properties.  Since this is really a keyword, the method should make the best possible effort
        /// to perform partial matches against the available dictionary keys and Transition properties.</param>
        /// <returns>[Transition] The Transition object found, or null if no match was found.</returns>
        public Transition GetTransition(string TransitionKeyword)
        {
            
            try
            {
                return _transitions.First(item => item.Value.Name == TransitionKeyword).Value;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// GetCombatant
        /// Returns a Combatant from the room.  This will be either a PC or an NPC.  This method should 
        /// search Combatants to find a match and return the best possible match.  If no match is found, 
        /// the method should return null.
        /// </summary>
        /// <param name="CombatantKeyword">[string] A keyword string of the Combatant name property.
        /// Since this is really a keyword, the method should make the best possible effort to perform
        /// partial matches against the available dictionary keys.</param>
        /// <returns>[Combatant] The Combatant object found, or null if no match was found.</returns>
        public Creature GetCreature(string CreatureKeyword)
        {
            if (_creatures.ContainsKey(CreatureKeyword))
            {
                return _creatures[CreatureKeyword];
            }

            return null;
        }

        /// <summary>
        /// InitializeComponents
        /// Initialzes class properties.  This method modularizes property initialization.
        /// </summary>
        private void InitializeComponents()
        {
            _creatures = new ConcurrentDictionary<string, Creature>();
            _transitions = new ConcurrentDictionary<string, Transition>();
        }

        public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        {
            if (Adapter != null && Adapter.LastRetrievedModel == RatDataModelType.Room)
            {
                _id = Adapter.ResultSet.GetValue<int>(RatDataModelAdapter.RoomFields.ID);
                _name = Adapter.ResultSet.GetValue<string>(RatDataModelAdapter.RoomFields.NAME);
                _descr = Adapter.ResultSet.GetValue<string>(RatDataModelAdapter.RoomFields.DESCRIPTION);
            }
        }

        /// <summary>
        /// LoadDataRow
        /// Loads the contents of a data row from the database.  Database field names are obtained
        /// from the appropriate class constant.
        /// </summary>
        /// <param name="Row">[DataRow] The database record containing data supporting this class.</param>
        //public override void LoadDataRow(DataRow Row)
        //{
        //    int tmp = 0;

        //    try
        //    {
        //        PopulatePropertyFromDataRow<int>(Row, Fields.ID, out this._id);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.NAME, out this._name);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.DESCRIPTION, out this._descr);

        //        // The region for this room was obtained in the constructor, so this value should already 
        //        // be available.  Check it against the database record to verify that the object stored 
        //        // in the MapRegion property is correct.
        //        PopulatePropertyFromDataRow<int>(Row, Fields.REGION, out tmp);
        //        if (Region.ID != tmp)
        //            throw new OperationFailedException("The Region referenced when creating this Room " +
        //                "does not match the value stored in the database.");

        //        LoadTransitions();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// LoadInventory
        /// Loads all Items for this Room from the database and hydrates them.  This method is only
        /// called at service start up.
        /// </summary>
        public void LoadInventory()
        {
            RecordManager rm = new RecordManager();
            DataTable dt = rm.SendReadRequest("", null);


        }

        /// <summary>
        /// LoadNonPlayerCharacters
        /// Loads all NonPlayerCharacters for this Room from the database and hydrates them.  This 
        /// method is only called at service start up.
        /// </summary>
        public void LoadNonPlayerCharacters()
        {
            RecordManager rm = new RecordManager();
            DataTable dt = rm.SendReadRequest("", null);
        }

        /// <summary>
        /// LoadTransitions
        /// Loads all transitions for this Room from the database and hydrates them.  This method is 
        /// only called at service start up.
        /// </summary>
        public void LoadTransitions()
        {
            RatDataModelAdapter a = new RatDataModelAdapter();
            a.Retrieve(RatDataModelType.Transition, new List<DataParameter>() {
                new DataParameter(RatDataModelAdapter.TransitionFields.ROOM_FROM, ID) });

            for (int i = 0; i < a.ResultSet.RecordCount; i++)
            {
                a.ResultSet.MoveToRecord(i);
                Transition t = new Transition(a, this);
                _transitions.TryAdd(t.GameID, t);
            }
            //List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(new SqlParameter(Transition.SPArguments.ID, _id));
            //RecordManager rm = new RecordManager();
            //DataTable dt = null;

            //try
            //{
            //    dt = rm.SendReadRequest(Transition.StoredProcedures.SELECTALL, p);
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

            //try
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        Transition t = new Transition(this, new RatDataModelAdapter());
            //        if (!_transitions.TryAdd(t.ID.ToString(), t))
            //            throw new OperationFailedException("Could not add Transition " + t.Name +
            //                " to Room " + Name + ".");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        /// <summary>
        /// RemoveCombatant
        /// Removes the specified combatant from this room.  If the Combatant is not present in this
        /// room, raise an OperationException.  This occurs when a player logs out, a player dies, or 
        /// when a PC or NPC leaves the room through a Transition.  When an NPC dies, it remains in
        /// the room collection and respawns at its own time.
        /// </summary>
        /// <param name="OldCombatant">[Combatant] The Combatant object to remove from the room
        /// collection.</param>
        public bool RemoveCombatant(Creature OldCombatant)
        {
            Creature com;
            if (_creatures.ContainsKey(OldCombatant.Name))
            {
                return _creatures.TryRemove(OldCombatant.Name, out com);
                //if (!_combatants.TryRemove(OldCombatant.Name, out com))
                //{
                //    throw new OperationFailedException("The combatant name " + OldCombatant.Name +
                //        " could not be removed from the room.");
                //}
            }
            return false;
        }

        /// <summary>
        /// ResolveTransitionReferences
        /// Causes each transition in this room to re-reference the Room specified by its RoomTo.ID
        /// property.  This method is necessary because all Room objects in the world map must be
        /// instantiated before any Transition can reliably link to its target Room.
        /// </summary>
        public void ResolveTransitionReferences()
        {
            if (_transitions != null)
            {
                foreach (Transition t in _transitions.Select(item => item.Value))
                {
                    try
                    {
                        t.ResolveRoomReferences(Region.Realm);
                    }
                    catch (OperationFailedException ex)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        public override bool Save(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }
    }
}
