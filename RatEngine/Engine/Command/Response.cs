using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.Engine.Command
{
    /// <summary>
    /// Contains a collection of all messages to return when a game action is taken.
    /// </summary>
    /// <remarks></remarks>
    [DataContract(IsReference = true)]
    public class Response
    {
        
        public Response() { }

        [DataMember]
        public List<Message> Messages { get; protected set; }

        public void AddMessage(Message message)
        {
            if (message != null)
            {
                if (Messages == null)
                    Messages = new List<Message>();
                Messages.Add(message);
            }
        }

        /// <summary>
        /// Adds the Message objects stored in the specified Response to
        /// the local Message list.
        /// </summary>
        /// <param name="Response"></param>
        public void AddResponseMessages(Response Response)
        {
            foreach (Message msg in Response.Messages)
            {
                Messages.Add(msg);
            }
        }

        /// <summary>
        /// A specific message to return to a specific user.
        /// </summary>
        /// <remarks></remarks>
        [DataContract(IsReference = true)]
        public class Message
        {
            /// <summary>
            /// The message text.
            /// </summary>
            [DataMember]
            public string Text { get; set; }

            /// <summary>
            /// Context information for the message.
            /// </summary>
            [DataMember]
            public string Type { get; set; }

            /// <summary>
            /// The player ID representing the player to whom the messages
            /// should be delivered.
            /// </summary>
            [DataMember]
            public string CharacterID { get; protected set; }
        }
    }
}
