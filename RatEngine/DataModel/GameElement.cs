using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Tagging;
using RatEngine.DataSource;
using RatEngine.Engine.Command;

namespace RatEngine.DataModel
{
    /// <summary>
    /// GameElement
    /// This class is the base class for all game objects populated from the database.
    /// All items included in the database must have ID, Name, and Description properties.
    /// This provides a common set of properties for all game elements to more easily
    /// support commands like "Look".
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public abstract class GameElement : IComparable
    {
        protected int _id;
        protected Guid _gameid;
        protected string _name;
        protected string _descr;
        protected List<Flag> _flags;
        protected List<Effect> _effects;
        protected List<CommandListener> _listeners;
        
        public GameElement()
        {
            _flags = new List<Flag>();
            _effects = new List<Effect>();
            _listeners = new List<CommandListener>();
        }

        /// <summary>
        /// The description of this game object.
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return _descr; }
            set { _descr = value; }
        }

        [DataMember]
        public virtual List<Effect> Effects
        {
            get { return _effects; }
            set { }
        }

        [DataMember]
        public virtual List<Flag> Flags
        {
            get { return _flags; }
            set { }
        }

        /// <summary>
        /// The unique game identifier for this game object.
        /// </summary>
        [DataMember]
        public Guid GameID
        {
            get { return _gameid; }
            set { _gameid = value; }
        }

        /// <summary>
        /// The database primary key.
        /// </summary>
        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        
        /// <summary>
        /// The name of this game object.
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int CompareTo(object obj)
        {
            return ((IComparable)_gameid).CompareTo(obj);
        }
    }
}
