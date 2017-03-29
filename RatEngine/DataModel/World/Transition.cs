using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Tagging;
using RatEngine.DataSource;

namespace RatEngine.DataModel.World
{
    /// <summary>
    /// This class defines a transition between Rooms.  This class provides PCs and NPCs with
    /// a means to travel from room to room.  The class supports flag challenges, so some
    /// transitions may not be easy to use.  Transition properties define the descriptions users
    /// receive when another user enters or leaves a room.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Transition : GameElement
    {
        protected Transition() { }

        /// <summary>
        /// Constructor
        /// Initializes this Transition from a database record.  Because this object must have been
        /// created from a Room, that Room reference is provided as well to save a step during
        /// initialization.  Throws a NullReferenceException if a null value is passed for StartingRoom.
        /// Throws a NullReferenceException if a null value is passed for Row.
        /// </summary>
        /// <param name="Row">[DataRow] The record from which to initialize this Transition.</param>
        /// <param name="room">[Room] The room used to access this Transition.</param>
        public Transition(Room fromRoom, Room toRoom)
        {
            FromRoom = fromRoom;
            ToRoom = toRoom;
        }

        [DataMember]
        public virtual Room FromRoom { get; protected set; }

        [DataMember]
        public virtual Room ToRoom { get; protected set; }

        
    }
}
