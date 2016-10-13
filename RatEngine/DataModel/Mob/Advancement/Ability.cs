using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel;
using RatEngine.DataModel.Tagging;
using RatEngine.DataSource;

namespace RatEngine.DataModel.Mob.Advancement
{
    /// <summary>
    /// Ability
    /// This class defines a PlayerCharacter or NonPlayerCharacter game ability
    /// that they can use.  Abilities do not have to be magical - this class is any
    /// action a PC or NPC can use to manipulate other entities in game.  Abilities
    /// are also keywords and are referenced by the Keyword class.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Ability : GameElement
    {
        // Database field names.
        public struct Fields
        {
            public const string ID = "ID";
            public const string NAME = "Name";
            public const string DESCRIPTION = "Description";
            public const string POWER = "Power";
            public const string COOLDOWN = "Cooldown";
            public const string ACCURACY = "Accuracy";
            public const string LEVEL = "Level";
        }

        // Database stored procedures.
        public struct StoredProcedures
        {
            public const string SELECT = "spGetAbilityEffects";
            public const string DELETE = "";
            public const string INSERT = "";
            public const string UPDATE = "";
        }

        public struct SPArguments
        {
            public const string ID = "Id";
        }

        public Ability(RatDataModelAdapter Adapter) : base(Adapter)
        {
            InitializeComponents();
            //if (Row != null)
            //    LoadDataRow(Row);
            //else
            //    throw new NullReferenceException("The DataRow record for an Ability was null.  " +
            //        "Cannot initialize the Ability.");
        }

        // The time the user must wait before using another ability after this one.
        private int _cooldown;

        // The relative power of this ability.  Think of this as the ability's level.
        // This value will affect the relative impact of the ability when used.
        private int _power;

        // The ability's accuracy.  This is a measure of how likely this ability is 
        // to work correctly on an average target.
        private double _accur;

        // The list of effects associated with this Ability.  Depending on the ability,
        // it may confer flags upon the target.  Those flags are organized into dicrete
        // groups that compose an effect.  This is intended as a way for an ability with
        // a duration longer than instantaneous to linger on the target.  The
        // ConcurrentDictionary class is used here to avoid collisions from multithreading.
        private ConcurrentDictionary<string, Effect> _effects;

        //private variable containing the level of the ability for the associated ladder.
        //this provides the ability to reference its own level instead of relying on the ladder
        //object.
        private int _level;

        //private variable containing the AbilityLadder reference. This is needed since Ability
        //will be instantiated for every AbilityLadder. This variable will distinguish which Ability
        //is contained in which ladder
        private AbilityLadder _ladder;

        public int Cooldown
        {
            get { return _cooldown; }
            set { _cooldown = value; }
        }

        public int Power
        {
            get { return _power; }
            set { _power = value; }
        }

        public double Accuracy
        {
            get { return _accur; }
            set { _accur = value; }
        }

        public AbilityLadder Ladder
        {
            get { return _ladder; }
        }

        public ConcurrentDictionary<string, Effect> Effects
        {
            get { return _effects; }
        }

        public int Level
        {
            get { return _level; }
        }

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
        /// Delete
        /// Deletes the record from the database represented by this object.
        /// </summary>
        /// <returns>[bool] True if the delete operation was successful, otherwise false.</returns>
        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override bool Delete(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// InitializeComponents
        /// Sets all object properties to their initial values.
        /// </summary>
        public void InitializeComponents()
        {
            _effects = new ConcurrentDictionary<string, Effect>();
        }

        /// <summary>
        /// Ability
        /// Constructor will take the AbilityLadder and fill the _ladder variable as will as parse
        /// the dataRow and fill the Construct Fields and the variables
        /// </summary>
        /// <param name="Row">[DataRow] The database record containing data supporting this class.</param>
        public Ability(AbilityLadder ladder, RatDataModelAdapter Adapter) : base(Adapter)
        {
            InitializeComponents();

            if (ladder != null)
                //fill the _ladder property
                _ladder = ladder;
            else
                throw new NullReferenceException("The ability of an AbilityLadder cannot be null.");

            //if (abilityRow != null)
            //    LoadDataRow(abilityRow);
            //else
            //    throw new NullReferenceException("The DataRow record for a Region was null.  " +
            //        "Cannot initialize the Region.");
        }


        /// <summary>
        /// LoadDataRow
        /// Loads the contents of a data row from the database.  Database field names are obtained
        /// from the appropriate class constant.
        /// </summary>
        /// <param name="Row">[DataRow] The database record containing data supporting this class.</param>
        //public override void LoadDataRow(DataRow Row)
        //{
        //    try
        //    {
        //        PopulatePropertyFromDataRow<int>(Row, Fields.ID, out this._id);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.NAME, out this._name);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.DESCRIPTION, out this._descr);
        //        PopulatePropertyFromDataRow<int>(Row, Fields.POWER, out this._power);
        //        PopulatePropertyFromDataRow<int>(Row, Fields.COOLDOWN, out this._cooldown);
        //        PopulatePropertyFromDataRow<double>(Row, Fields.ACCURACY, out this._accur);
        //        PopulatePropertyFromDataRow<int>(Row, Fields.LEVEL, out this._level);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    //if no exceptions, run method to fill _levels
        //    LoadEffects();
        //}

        /// <summary>
        /// LoadEffects
        /// Retrieves the correct table from the Database and injects DataRows into AbilityEffects objects
        /// to instantiate themselves. Also will add AbilityEffects into _effects
        /// </summary>
        public void LoadEffects()
        {
            //instantiate RecordManager to gain access to Database
            RecordManager recordManager = new RecordManager();

            //Create empty DataTable for results
            DataTable dtResults = new DataTable();
            //attempt to retrieve table
            try
            {
                //create IList<SqlParam> and sp name for SendReadRequest 
                IList<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter abilityID = new SqlParameter("@abilityID", this._id);
                string spName = StoredProcedures.SELECT;

                //add SqlParam to Ilist
                sqlParams.Add(abilityID);

                //fill table by calling SendReadRequest
                dtResults = recordManager.SendReadRequest(spName, sqlParams);

                //foreach loop to create and add each AbilityEffect to _effects list with the <Tkey> as name of AbilityEffect
                foreach (DataRow row in dtResults.Rows)
                {
                    AbilityEffect newAbilityEffect;

                    //create AbilityEffect
                    try
                    {
                        newAbilityEffect = new AbilityEffect(null);//TODO: change parameters to what they need to be after Room class is implemented
                        //add newRoom to _room list
                        //_effects.TryAdd(newAbilityEffect.Name, newAbilityEffect);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());//TODO: After testing, write Exception code for each Exception found
            }
        }

        public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        public override bool Save(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }
    }
}
