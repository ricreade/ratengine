using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataSource;

namespace RatEngine.DataModel.UserAccount
{
    /// <summary>
    /// This class represents an application user on the client.  This class provides some context
    /// to PlayerCharacter.  Its main purpose is to identify a user on the system.  It can also
    /// provide a means to identify players who are using two characters at once - something that
    /// is usually frowned upon.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class User
    {
        
        /// <summary>
        /// Instantiates a new User object based on the specified unique Game ID.  
        /// If this value is provided, this object will be populated based on the data source.  
        /// If this is a new record, specify null for this value.
        /// </summary>
        /// <param name="GameID">The game id of this User object, or null if this 
        /// is a new record.</param>
        public User() { }

        [DataMember]
        public virtual string UserID { get; protected set; }

        [DataMember]
        public virtual string UserName { get; protected set; }

        [DataMember]
        public virtual string Email { get; set; }

        
    }
}
