using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Tagging;
using RatEngine.DataModel.Mob.Advancement;
using RatEngine.DataModel.Questing;
using RatEngine.DataModel.World;
using RatEngine.DataSource;
using RatEngine.Engine.Command;

namespace RatEngine.DataModel.Mob
{
    /// <summary>
    /// This class represents a player and their character in the game.  All command strings
    /// reaching the application service are processed through this class because all commands
    /// are executed from the perspective of the player.
    /// </summary>
    public class PlayerCharacter : Creature
    {
        // The character's current xp count.  The total number of ladder levels available for
        // a player to allocate are calculated based on the total number of levels the player
        // should have for this level of experience minus the number of levels already allocated.
        private int _xp;
        private int _unspentXP;

        // The user associated with this character.  The User object handles the original login,
        // but thereafter has a minimal role in the application.  One benefit for tracking it here
        // is to identify cases where multiple characters belonging to the same player are active
        // at once.
        private int _user;

        // When a user logs in with a character, SignalR provides the sessionid, which it uses to
        // keep track of clients.  This same value is stored on the application service so that when
        // the application service sends a request to SignalR to transmit a response, both layers
        // are referencing the same value.
        private string _sessionid;

        // The quest goals currently active for the character.  The original class diagram notes
        // Quest rather than QuestGoal.  This reference is a little more appropriate to the player.
        private ConcurrentDictionary<QuestGoal, string> _questgoals;

        public override RatDataModelAdapter DataAdapter
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Instantiates a new PlayerCharacter object based on the specified unique Game ID.  
        /// If this value is provided, this object will be populated based on the data source.  
        /// If this is a new record, specify null for this value.
        /// </summary>
        /// <param name="GameID">The game id of this PlayerCharacter object, or null if this 
        /// is a new record.</param>
        public PlayerCharacter(RatDataModelAdapter Adapter) : base(Adapter)
        {
            //InitializeComponents();
            string charprocedure = "mspGetCharacter";

            IList<SqlParameter> idparam = new List<SqlParameter>();
            idparam.Add(new SqlParameter("@CharID", GameID));

            RecordManager rm = new RecordManager();
            DataTable var = rm.SendReadRequest(charprocedure, idparam);
            _user = Convert.ToInt32(var.Rows[0]["fkUserID"]);
            _id = Convert.ToInt32(var.Rows[0]["ID"]);
            _descr = Convert.ToString(var.Rows[0]["Description"]);
            _name = Convert.ToString(var.Rows[0]["Name"]);
            _hpmax = Convert.ToInt32(var.Rows[0]["maxHP"]);
            _hpcurr = Convert.ToInt32(var.Rows[0]["currentHP"]);
            _mpmax = Convert.ToInt32(var.Rows[0]["maxMP"]);
            _mpcurr = Convert.ToInt32(var.Rows[0]["currentMP"]);
            _str = Convert.ToInt32(var.Rows[0]["Str"]);
            _dex = Convert.ToInt32(var.Rows[0]["Dex"]);
            _int = Convert.ToInt32(var.Rows[0]["Int"]);
            //_location = Convert.ToInt32(var.Rows[0]["fkRoom"]);
            //_location = new Room(var.Rows[0]["fkRoom"].ToString());
            //_level = Convert.ToInt32(var.Rows[0]["Level"]);
            _xp = Convert.ToInt32(var.Rows[0]["TotalXP"]);
            _unspentXP = Convert.ToInt32(var.Rows[0]["unspentXP"]);

        }

        public void setUserID(int ID)
        {
            _user = ID;
        }
        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override bool Delete(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetScore
        /// Returns a string containing a character score sheet.
        /// </summary>
        /// <returns>[string] The character score sheet.</returns>
        public string GetScore()
        {
            return "";
        }

        /// <summary>
        /// GetStatus
        /// Returns a string containing basic status information.
        /// </summary>
        /// <returns>[string] The character status information.</returns>
        public string GetStatus()
        {
            return "";
        }

        /// <summary>
        /// GetTrain
        /// Returns a string containing a list of all available character abilities.
        /// </summary>
        /// <returns>[string] The character ability list.</returns>
        public string GetTrain()
        {
            return "";
        }

        /// <summary>
        /// Kill
        /// This is an override of the base Combatant.Kill method.  This may or may not be necessary
        /// depending on any special requirements specific to players.
        /// </summary>
        /// <param name="Target">[Combatant] The target of the Kill command.</param>
        public override void Kill(Creature Target)
        {
            base.Kill(Target);

        }

        public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        //public override void LoadDataRow(System.Data.DataRow Row)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// ProcessCommandString
        /// This method is called by the application server to process the command string provided
        /// by the user.  All processing is kicked off by this method.  It calls the KeywordManager
        /// to identify the appropriate keyword and take the appropriate actions.
        /// </summary>
        /// <param name="CommandString">[string] The command string input by the user, including
        /// the keyword and all arguments.</param>
        /// <returns>[string] The return string to the application service.</returns>
        public Task<Response> ProcessCommandString(string CommandString)
        {
            Creature CommandPlayer = this;
            Task<Response> resp = null;
            if (CommandPlayer != null)
            {
                //KeywordManager.GetKeyword(CommandString);
                //resp = Keyword.ExecuteCommandString(CommandString, CommandPlayer);
                Keyword kw = KeywordManager.GetKeyword(CommandString);
                resp = kw.ExecuteCommandString(CommandString, CommandPlayer);
                resp.Wait();
            }

            return resp;
        }

        public override bool Save()
        {
            string saveprocedure;
            IList<SqlParameter> charparam = new List<SqlParameter>();
            if (_id == 0)
            {
                saveprocedure = "mspNewPlayerCharacter";
                //IList<SqlParameter> charparam = new List<SqlParameter>();
                charparam.Add(new SqlParameter("@userID", _user));
                charparam.Add(new SqlParameter("@Description", _descr));
                charparam.Add(new SqlParameter("@Name", _name));
                charparam.Add(new SqlParameter("@maxHP", _hpmax));
                charparam.Add(new SqlParameter("@maxMP", _mpmax));
                charparam.Add(new SqlParameter("@Str", _str));
                charparam.Add(new SqlParameter("@Dex", _dex));
                charparam.Add(new SqlParameter("@Int", _int));
                //charparam.Add(new SqlParameter("@RoomID", _location.ID));
                charparam.Add(new SqlParameter("@RoomID", 1));
                charparam.Add(new SqlParameter("@RealmID", 1));
            }
            else
            {
                saveprocedure = "mspSavePlayerCharacter";
                charparam.Add(new SqlParameter("@CharacterID", _id));
                charparam.Add(new SqlParameter("@Description", _descr));
                charparam.Add(new SqlParameter("@Level", _lvl));
                charparam.Add(new SqlParameter("@maxHP", _hpmax));
                charparam.Add(new SqlParameter("@CurrentHP", _hpcurr));
                charparam.Add(new SqlParameter("@maxMP", _mpmax));
                charparam.Add(new SqlParameter("@CurrentMP", _mpcurr));
                charparam.Add(new SqlParameter("@Str", _str));
                charparam.Add(new SqlParameter("@Dex", _dex));
                charparam.Add(new SqlParameter("@Int", _int));
                //charparam.Add(new SqlParameter("@RoomID", _location.ID));
                charparam.Add(new SqlParameter("@TotalXP", _xp));
                charparam.Add(new SqlParameter("@unspentXP", _unspentXP));
                charparam.Add(new SqlParameter("@RoomID", 1));

            }
            RecordManager rm = new RecordManager();
            rm.SendWriteRequest(saveprocedure, charparam);

            //throw new NotImplementedException();
            return true;
        }

        public override bool Save(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// UseAbility
        /// Attempts to apply the specified ability to the target.  Some targets may not be valid
        /// for certain abilities.  Analysis of the ability list should be considered to determine
        /// whether special handling is required.
        /// </summary>
        /// <param name="Ability">[Ability] The abiity to use.</param>
        /// <param name="Target">[Effectable] The target of the ability.</param>
        public void UseAbility(Advancement.Ability Ability, Effectable Target)
        {

        }
    }
}
