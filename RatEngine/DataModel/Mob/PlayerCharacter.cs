using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Tagging;
using RatEngine.DataModel.Mob.Advancement;
using RatEngine.DataModel.Questing;
using RatEngine.DataModel.UserAccount;
using RatEngine.DataModel.World;
using RatEngine.DataSource;
using RatEngine.Engine.Command;


namespace RatEngine.DataModel.Mob
{
    /// <summary>
    /// Represents a character controlled by a player.
    /// </summary>
    /// <remarks>This class serves as the point of entry for all player command
    /// statements.  When a <see cref="User"/> issues a command through the service,
    /// the first action is to locate the player character instance associated with
    /// that player and pass the command to that instance to process.  The command
    /// is then filtered through all associated <see cref="GameElement"/> instances.
    /// </remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class PlayerCharacter : Creature
    {
        public PlayerCharacter() 
        {
            
        }

        public PlayerCharacter(User player)
        {
            Player = player;
        }
        
        [DataMember]
        public virtual List<Quest> Quests { get; protected set; }

        [DataMember]
        public virtual User Player { get; protected set; }

        public virtual void AddQuest(Quest quest)
        {
            if (quest != null)
            {
                InitializeList(Quests);
                Quests.Add(quest);
            }
        }

        /// <summary>
        /// ProcessCommandString
        /// This method is called by the application server to process the command string provided
        /// by the user.  All processing is kicked off by this method.  It calls the KeywordManager
        /// to identify the appropriate keyword and take the appropriate actions.
        /// </summary>
        /// <param name="CommandString">[string] The command string input by the user, including
        /// the keyword and all arguments.</param>
        /// <returns>[string] The return string to the application service.</returns>
        public Response ProcessCommandString(string commandString)
        {
            GameCommand command = new GameCommand(this, commandString);
            ProcessCommand(command);

            if (Location != null)
                Location.ProcessCommand(command);

            InitializeList(Quests);
            foreach (Quest quest in Quests)
            {
                quest.ProcessCommand(command);
            }
            
            return command.CommandResponse;
        }

        public virtual bool RemoveQuest(Quest quest)
        {
            InitializeList(Quests);
            return Quests.Remove(quest);
        }
        
    }
}
