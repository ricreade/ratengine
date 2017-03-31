//using System;
//using System.Collections.Generic;
//using System.Collections.Concurrent;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Threading.Tasks;

//namespace RatEngine.DataModel.Questing
//{
//    /// <summary>
//    /// This class references all possible quests in the game.  This class is hydrated at
//    /// service start up.
//    /// </summary>
//    [Serializable]
//    [DataContract(IsReference = true)]
//    public class QuestManager
//    {
//        // A collection of all quests in the game.
//        private ConcurrentDictionary<Quest, string> _quests;

//        /// <summary>
//        /// LoadQuests
//        /// This method loads all quests from the database.  This method is intended to be
//        /// called at service start up.
//        /// </summary>
//        public void LoadQuests()
//        {

//        }
//    }
//}
