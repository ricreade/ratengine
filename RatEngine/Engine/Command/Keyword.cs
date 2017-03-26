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
using RatEngine.DataModel.Mob;
using RatEngine.DataModel.Mob.Advancement;
using RatEngine.DataSource;
using RatEngine.Engine.Instruction;

namespace RatEngine.Engine.Command
{
    /// <summary>
    /// This class defines a command keyword that a user can use to precede a command string.
    /// Input commands that do not precede with a Keyword are considered invalid, so the
    /// application need only look at the first word of the string to determine the appropriate
    /// keyword to invoke.  The Keyword then checks the command string against the available
    /// KeywordSyntax objects to find a match.  This class is hydrated from the database at
    /// server start up and thereafter does not change while the service is running.  Its values
    /// are read only.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Keyword : GameElement
    {
        // Database field names.
        //public struct Fields
        //{
        //    public const string ID = "ID";
        //    public const string NAME = "Name";
        //    //public const string DESCRIPTION = "Description";
        //    public const string ALIAS = "Alias";
        //    public const string HELP = "HelpText";
        //}

        //// Database stored procedures.
        //public struct StoredProcedures
        //{
        //    public const string SELECT = "";
        //    public const string SELECT_ALL = "mspGetKeyWords";
        //    public const string DELETE = "";
        //    public const string INSERT = "";
        //    public const string UPDATE = "";
        //}

        //public struct SPArguments
        //{
        //    public const string ID = "@KeywordID";
        //}

        /// <summary>
        /// Constructor.  This constructor provides a means to hydrate the Keyword object from
        /// a database record.  If either the Row is null, this method throws a new
        /// NullReferenceException.
        /// </summary>
        /// <param name="Row">[DataRow] The database record from which to hydrate the Room.</param>
        public Keyword() 
        {
            InitializeComponents();

            //if (Row != null)
            //{
            //    LoadDataRow(Row);
            //    //LoadAbility();
            //    //LoadAssociatedFlags();
            //}
            //else
            //    throw new NullReferenceException("The DataRow record for a Keyword was null.  " +
            //        "Cannot initialize the Keyword.");
        }

        // The keyword value.
        //private string _kywrd;

        // A collection of the available syntaxes for this keyword.  The command string must validate
        // against one of these syntaxes to be considered valid.  The key is the syntax name.
        //private ConcurrentDictionary<Guid, KeywordSyntax> _syntaxes;
        private List<KeywordSyntax> _syntaxes;

        // The string to provide the user when the help command is executed with this keyword as an
        // argument.
        //private string _help;

        // An alternate keyword string for this keyword.
        //private string _alias;

        // All abilities are also keywords.  This connection provides the application with a way to
        // associate the use of an ability with the target if the effect has a duration greater than
        // instantaneous.  It also identifies keywords as abilities.  This value is null if the keyword
        // is not an ability.
        //private Ability _ability;

        // A collection of flags that can oppose this keyword.
        //private ConcurrentDictionary<string, Flag> _associatedflags;

        //public string Alias
        //{
        //    get { return _alias; }
        //}

        //public string Help
        //{
        //    get { return _help; }
        //}

        //public Ability Ability
        //{
        //    get { return _ability; }
        //}

        //public override RatDataModelAdapter DataAdapter
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public override bool Delete()
        //{
        //    RecordManager rm = new RecordManager();
        //    return rm.SendWriteRequest(StoredProcedures.DELETE, null) > 0;
        //}

        //public override bool Delete(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// ExecuteCommandString
        /// This method executes the specified command string on behalf of the specified PlayerCharacter.
        /// If the command string syntax is incorrect, this method raises InvalidCommandSyntaxException.
        /// NPCs do not execute commands this way because their instructions to the application do
        /// not have to be interpreted.
        /// </summary>
        /// <param name="CommandString">[string] The command string for this keyword to execute.</param>
        /// <param name="Player">[PlayerCharacter] A reference to the player who input this command.</param>
        public Task<Response> ExecuteCommandString(string CommandString, Creature Player)
        {
            KeywordSyntax ks = null;
            Task<Response> t = null;
            try
            {
                ks = GetSyntax(CommandString);

                // Send syntax and all other references to an instruction manager to
                // execute instructions.
                InstructionManager im = new InstructionManager();
                //t = im.ExecuteInstructions(CommandString, this, ks, _associatedflags.Select(item => item.Value), Player);

                // Pause the task in case an exception will be thrown.
                t.Wait();
            }

            // Thrown by GetSyntax()
            catch (InvalidCommandSyntaxException ex)
            {
                return null;
            }

            // Thrown by the Task
            catch (AggregateException ex)
            {
                foreach (var e in ex.Flatten().InnerExceptions)
                {
                    return null;
                }
            }
            return t;
        }

        public void ExecuteCommand(GameCommand command)
        {
            KeywordSyntax syntax = null;

            try
            {
                syntax = GetSyntax(command.CommandString);
                InstructionManager manager = new InstructionManager();
                // execute syntax instructions.
            }
            catch (InvalidCommandSyntaxException ex)
            {
                return;
            }
        }

        /// <summary>
        /// GetSyntax
        /// Returns the appropriate KeywordSyntax object for the specified input command
        /// string.
        /// </summary>
        /// <param name="CommandString">[string] The input command string.</param>
        /// <returns>[KeywordSyntax] The KeywordSyntax object appropriate for the specified
        /// command string.</returns>
        public KeywordSyntax GetSyntax(string CommandString)
        {
            foreach (KeywordSyntax ks in _syntaxes)
            {
                if (ks.IsSyntaxMatch(CommandString))
                    return ks;
            }
            return null;
        }

        /// <summary>
        /// InitializeComponents
        /// Sets all object properties to their initial values.
        /// </summary>
        public void InitializeComponents()
        {
            _syntaxes = new List<KeywordSyntax>(); //new ConcurrentDictionary<Guid, KeywordSyntax>();
            //_associatedflags = new ConcurrentDictionary<string, Flag>();
            //_ability = null;
        }

        public bool IsMatch(string command)
        {
            foreach (KeywordSyntax syntax in _syntaxes)
            {
                if (syntax.IsSyntaxMatch(command))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// LoadAbility
        /// Loads the Ability object (if any) related to this Keyword.  This provides a link
        /// to the Ability so that its properties can be applied when the keyword is invoked.
        /// </summary>
        //private void LoadAbility()
        //{
        //    List<SqlParameter> p = new List<SqlParameter>();
        //    p.Add(new SqlParameter(Ability.SPArguments.ID, _id));
        //    RecordManager rm = new RecordManager();
        //    DataTable dt = null;

        //    try
        //    {
        //        dt = rm.SendReadRequest(Ability.StoredProcedures.SELECT, p);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    if (dt.Rows.Count > 0)
        //    {
        //        try
        //        {
        //            //_ability = new Ability(null, dt.Rows[0]);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}

        /// <summary>
        /// LoadAssociatedFlags
        /// Loads all flags associated with this keyword.  These flags must be overcome before
        /// a command invoking this keyword can be successfully executed.
        /// Note: Flag checks are not yet implemented.
        /// </summary>
        //private void LoadAssociatedFlags()
        //{

        //}

        //public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// LoadDataRow
        /// Hydrates this keyword using the data stored in the specified DataRow.
        /// </summary>
        /// <param name="Row">[DataRow] The database record containing the data used to hydrate
        /// this keyword object.</param>
        //public override void LoadDataRow(DataRow Row)
        //{
        //    try
        //    {
        //        PopulatePropertyFromDataRow<int>(Row, Fields.ID, out this._id);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.NAME, out this._name);
        //        //PopulatePropertyFromDataRow<string>(Row, Fields.DESCRIPTION, out this._descr);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.ALIAS, out this._alias);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.HELP, out this._help);

        //        LoadSyntaxes();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// LoadSyntaxes
        /// Loads all keyword syntaxes associated with this keyword from the database.  This method
        /// throws an OperationFailedException if the operation fails.  This method is called as part
        /// of server initialization.
        /// </summary>
        public void LoadSyntaxes()
        {
            RatDataModelAdapter a = new RatDataModelAdapter();
            a.Retrieve(RatDataModelType.KeywordSyntax, null);

            for (int i = 0; i < a.ResultSet.RecordCount; i++)
            {
                a.ResultSet.MoveToRecord(i);
                KeywordSyntax ks = new KeywordSyntax(this);
                _syntaxes.Add(ks);
                //_syntaxes.TryAdd(ks.GameID, ks);
            }
            //List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(new SqlParameter(SPArguments.ID, _id));
            //RecordManager rm = new RecordManager();
            //DataTable dt = null;

            //try
            //{
            //    dt = rm.SendReadRequest(KeywordSyntax.StoredProcedures.SELECTALL, p);
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

            //try
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        KeywordSyntax ks = new KeywordSyntax(null, dr, this);
            //        if (!_syntaxes.TryAdd(ks.ID.ToString(), ks))
            //        {
            //            throw new OperationFailedException("Could not add Syntax " + ks.Syntax +
            //                " to Keyword " + Name + ".");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        /// <summary>
        /// Save
        /// Saves changes to this keyword to the database, or creates a new database record if this is
        /// a new keyword.
        /// </summary>
        /// <returns>[bool] True if the save operation was successful, otherwise false.</returns>
        //public override bool Save()
        //{
        //    RecordManager rm = new RecordManager();
        //    if (_id > 0)
        //    {
        //        return rm.SendWriteRequest(StoredProcedures.UPDATE, null) > 0;
        //    }
        //    else
        //    {
        //        _id = rm.SendWriteRequest(StoredProcedures.INSERT, null);
        //        return _id > 0;
        //    }
        //}

        //public override bool Save(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
