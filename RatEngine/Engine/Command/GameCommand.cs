using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Mob;

namespace RatEngine.Engine.Command
{
    /// <summary>
    /// Encapsulates an issued game command and its associated response.
    /// </summary>
    /// <remarks>Used to issue commands to the game engine and return the
    /// game response.  Typically, a command is issued by a player and the
    /// source is the <see cref="PlayerCharacter"/> instance representing
    /// that character.  A game command may also be issued by the game via
    /// a <see cref="NonPlayerCharacter"/> and the response will be routed
    /// appropriately beginning with <see cref="CommandListener"/> elements
    /// on that instance.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class GameCommand
    {
        
        public GameCommand() { }

        public GameCommand(Creature source, string commandString)
        {
            Source = source;
            CommandString = CommandString;
            ResultSet = new CommandResultSet();
        }

        [DataMember]
        public string CommandString { get; protected set; }

        [DataMember]
        public CommandResultSet ResultSet { get; protected set; }

        [DataMember]
        public Creature Source { get; protected set; }

    }
}
