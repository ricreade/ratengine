using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.Engine.Command
{
    /// <summary>
    /// Response
    /// Contains a collection of all messages to return when a game action is taken.
    /// </summary>
    [DataContract(IsReference = true)]
    public class Response
    {
        /// <summary>
        /// Constructor
        /// Initializes the Messages list.
        /// </summary>
        public Response()
        {
            Messages = new List<Message>();
        }

        // The list of messages to return with this response.
        [DataMember]
        public List<Message> Messages;

        /// <summary>
        /// AddResponseMessages
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
        /// Message
        /// A specific message to return to a specific user.
        /// </summary>
        [DataContract(IsReference = true)]
        public class Message
        {
            // The text of the message.
            [DataMember]
            public string Text;

            // The type of the message to provide context to the client.
            [DataMember]
            public string Type;

            // The character id indicating which user should receive this message.
            [DataMember]
            public string CharacterID;
        }
    }
}
