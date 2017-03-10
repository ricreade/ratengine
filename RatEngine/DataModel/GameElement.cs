using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Tagging;
using RatEngine.DataSource;

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
    public abstract class GameElement : Effectable
    {
        protected int _id;
        protected Guid _gameid;
        protected string _name;
        protected string _descr;
        
        /// <summary>
        /// The base constructor for a GameElement.  All instances deriving from GameElement
        /// must specify a unique GameID value.  If this is a new record to be saved to the
        /// database, specify null for this value.
        /// </summary>
        /// <param name="GameID"></param>
        public GameElement(RatDataModelAdapter Adapter) : base(Adapter)
        {
            _adapter = Adapter;
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
        /// The unique game identifier for this game object.
        /// </summary>
        [DataMember]
        public Guid GameID
        {
            get { return _gameid; }
            set { _gameid = value; }
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

        /// <summary>
        /// The description of this game object.
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return _descr; }
            set { _descr = value; }
        }

        /// <summary>
        /// Deletes this record from the data source.
        /// </summary>
        /// <returns>True if the delete operation was successful.</returns>
        public override bool Delete()
        {
            return Delete(_adapter);
        }

        /// <summary>
        /// Hydrates the data object using the specified adapter.
        /// </summary>
        /// <param name="Adapter">The data adapter to use to hydrate this object.</param>
        public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        {
            _gameid = Adapter.ResultSet.GetValue<Guid>(RatDataModelAdapter.GameRegistryFields.GAME_ID);
        }

        /// <summary>
        /// Inserts or updates this record at the data source.
        /// </summary>
        /// <returns>True if the save operation was successful.</returns>
        public override bool Save()
        {
            return Save(_adapter);
        }
    }
}
