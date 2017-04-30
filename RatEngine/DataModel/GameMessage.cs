using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RatEngine.DataModel
{
    /// <summary>
    /// Encapsulates a pre-defined game message.  The instance defines any parameters required
    /// to construct the message.
    /// </summary>
    /// <remarks>Supports a game response to a command string by providing a structured framework
    /// to construct a contextually appropriate string.  When evaluating the instructions for a
    /// <see cref="CommandListener"/>, an instruction may reference a specific message, specifying
    /// any arguments instead of providing a hard-coded response.  This supports organized and
    /// structured responses.  Once the compiled string is returned, only the resulting string is
    /// included in the result set.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class GameMessage
    {
        private IDictionary<string, string> _arguments;
        private Regex _regex;

        public GameMessage() { }
        
        public GameMessage(string text)
        {
            Text = text;
        }

        [DataMember]
        public virtual Guid GameElementID { get; protected set; }

        /// <summary>
        /// The <see cref="GameElement"/> instance associated with this message. 
        /// </summary>
        /// <remarks>This property provides a way to organize messages for an element.  While any
        /// given message can be referenced by its ID value, this property allows the designer to
        /// create messages in a structured way.</remarks>
        [DataMember]
        public virtual GameElement Owner { get; protected set; }

        /// <summary>
        /// The text of this message.  Any parameters must be enclosed in curly braces (e.g. {field}).
        /// All such parameters must be satisfied with matching arguments before the final message
        /// can be constructed.
        /// </summary>
        [DataMember]
        public virtual string Text { get; protected set; }

        /// <summary>
        /// Applies any message arguments to the message text and constructs and returns the final
        /// message.  This message is not cached and must be rebuilt each time.
        /// </summary>
        /// <returns>The constructed message text.</returns>
        protected virtual string BuildMessage()
        {
            if (CanBuildMessage())
            {
                string message = Text;

                if (_arguments != null)
                {
                    foreach (string key in _arguments.Keys)
                    {
                        message = message.Replace(string.Format("{{{0}}}", key), _arguments[key]);
                    }
                }
                
                return message;
            }
            else
                throw new ArgumentException();
        }

        /// <summary>
        /// Indicates whether the final message can be constructed.  If the original message text
        /// contains no parameters, this method will always return true.  Otherwise, this method
        /// compares any provided arguments to the message text and verifies that all message
        /// parameters have been satisfied.
        /// </summary>
        /// <returns>True if all arguments required to build the final message are available.</returns>
        public virtual bool CanBuildMessage()
        {
            bool paramsSatisfied = true;
            
            InitializeRegex();
            MatchCollection matches = _regex.Matches(Text);

            if (matches.Count != (_arguments == null ? 0 : _arguments.Count))
                return false;

            if (_arguments != null)
            {
                List<string> parameters = matches.Cast<Match>().Select(match => match.Value).ToList();
                foreach (string arg in _arguments.Keys)
                {
                    paramsSatisfied &= parameters.Contains(string.Format("{{{0}}}", arg));
                }
            }
            
            return paramsSatisfied;
        }

        public virtual string[] GetArgumentNames()
        {
            InitializeRegex();
            MatchCollection matches = _regex.Matches(Text);
            return matches.Cast<Match>().Select(match => match.Value).ToArray();
        }

        /// <summary>
        /// Initializes the regular expression, if it has not already been initialized.
        /// </summary>
        private void InitializeRegex()
        {
            if (_regex == null)
                _regex = new Regex(@"{\w+}");
        }

        /// <summary>
        /// Sets the arguments list to use for constructing the final message.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <exception cref="NullReferenceException">The argument is null.</exception>
        public virtual void SetArguments(IDictionary<string, string> arguments)
        {
            if (arguments == null)
                throw new NullReferenceException();
            _arguments = arguments;
        }

        /// <summary>
        /// Returns the final message text.  If any defined parameters have not been satisfied
        /// by an argument via the SetArguments() method, this method throws an
        /// <see cref="ArgumentException"/>. 
        /// </summary>
        /// <exception cref="ArgumentException">A required argument to construct the message is
        /// missing.</exception>
        /// <returns>The final constructed message string.</returns>
        public override string ToString()
        {
            return BuildMessage();
        }
    }
}
