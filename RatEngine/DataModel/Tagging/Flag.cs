using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataSource;

namespace RatEngine.DataModel.Tagging
{
    /// <summary>
    /// Flag
    /// This class defines a specific tag associated with some other game object
    /// that defines how that game object interacts with other game objects.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Flag : GameElement // : IDataObject
    {
        
        /// <summary>
        /// Instantiates a declarative flag.
        /// </summary>
        /// <param name="GameID">The game id of this flag object, or null if this is a new record.</param>
        /// <param name="Name">The name of the flag.</param>
        protected Flag()
        {
            
        }

        public Flag(Guid id) : base(id)
        {

        }

        [DataMember]
        public virtual byte[] Data { get; protected set; }

        [DataMember]
        public virtual FlagTemplate Template { get; protected set; }

        public virtual bool IsConforming()
        {
            if (ReferenceEquals(Template, null))
                return false;
            return Template.IsConforming(this);
        }
        
    }
}
