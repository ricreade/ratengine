using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Mob;
using RatEngine.DataModel.Mob.Advancement;
using RatEngine.DataModel.World;


namespace RatEngine.DataModel
{
    public class GameManager
    {
        /// <summary>
        /// GameManager
        /// Static constructor to instantiate the class dictionaries the first time
        /// the class is instantiated.  All instances of GameManager will require
        /// access to these dictionaries.
        /// </summary>
        static GameManager()
        {
            _players = new ConcurrentDictionary<string, PlayerCharacter>();
            _realms = new ConcurrentDictionary<string, Realm>();
            _ladders = new ConcurrentDictionary<string, AbilityLadder>();

            LoadGameData();
        }

        /// <summary>
        /// GameManager
        /// Instance constructor to populate the Realms dictionary from the specified
        /// list.
        /// </summary>
        /// <param name="Realms">[List<Realm>Realms] The list of realms with which to
        /// populate the class dictionary.</param>
        public GameManager(List<Realm> Realms)
        {
            foreach (Realm r in Realms)
            {
                if (!_realms.TryAdd(r.Name, r))
                {
                    throw new OperationFailedException("Could not add Realm " +
                        r.Name + " to the collection.");
                }
            }
        }

        // This is a collection of all players currently active in the game.
        private static ConcurrentDictionary<string, PlayerCharacter> _players;

        // This is a reference to the keyword manager object for the application.
        // private static KeywordManager _kywrdmgr;

        // A collection of all realms in the application.
        private static ConcurrentDictionary<string, Realm> _realms;

        // A collection of all ability ladders in the game.
        private static ConcurrentDictionary<string, AbilityLadder> _ladders;

        /// <summary>
        /// GetPlayer
        /// Returns the PlayerCharacter object from the players list that matches the
        /// specified sessionid.  If no such player is found, throws a new
        /// PlayerNotFoundException and returns null.
        /// </summary>
        /// <param name="GameID">[string] The game id of the character to retrieve.</param>
        /// <returns>[PlayerCharacter] The player character associated with the game ID,
        /// or null if no such character was found.</returns>
        public PlayerCharacter GetCharacter(string GameID)
        {
            PlayerCharacter pc = null;
            try
            {
                if (_players.TryGetValue(GameID, out pc))
                {
                    return pc;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// AddCharacter
        /// Retrieves a PlayerCharacter object from the database identified by the the specified
        /// GameID.  If the retrieval is successful, adds the new PlayerCharacter object
        /// to the local dictionary and returns it.  Returns an uninitiated PlayerCharacter
        /// object if any step in the process failed.
        /// </summary>
        /// <param name="GameID">[string] The unique game ID of the character to be retrieved.</param>
        /// <returns>[PlayerCharacter] The PlayerCharacter retrieved from the database.</returns>
        public PlayerCharacter AddCharacter(string GameID)
        {
            PlayerCharacter pc = null;

            try
            {
                pc = new PlayerCharacter(null);
                _players.TryAdd(pc.ID.ToString(), pc);
            }
            catch (Exception ex)
            {
                return null;
            }

            // Add the new PlayerCharacter obj to the game world.
            foreach (Realm rlm in _realms.Select(item => item.Value))
            {
                foreach (Region reg in rlm.Regions)
                {
                    foreach (Room rm in reg.Rooms)
                    {
                        if (pc.Location.ID == rm.ID)
                        {
                            lock (pc.Location)
                            {
                                pc.Location = rm;
                            }
                            rm.AddCombatant(pc);
                            return pc;
                        }
                    }
                }
            }
            return new PlayerCharacter(null);
        }

        /// <summary>
        /// LoadGameData
        /// Loads all data associated with the application.  This method causes all other
        /// managers to load their respective elements.  All rooms, npcs, items, keywords,
        /// flag comparisons, and all other pieces of information are loaded with this
        /// method call.
        /// </summary>
        private static void LoadGameData()
        {
            /* This method is not currently used because the Realms are instantiated by the
               WorldContainer and the AbilityLadders have not yet been implemented.*/
        }
    }
}
