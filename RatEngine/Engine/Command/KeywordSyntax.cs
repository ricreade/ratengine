using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using RatEngine.DataModel;
using RatEngine.DataSource;
using RatEngine.Engine.Instruction;

namespace RatEngine.Engine.Command
{
    /// <summary>
    /// This class represents a single valid keyword syntax for an input command string.  All
    /// input commands must match a syntax value to be considered valid.  This class contains a
    /// SystemInstruction collection to tell the system what to do when a valid syntax is found.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class KeywordSyntax : GameElement
    {
        // Database field names.
        public struct Fields
        {
            public const string ID = "ID";
            //public const string NAME = "Name";
            //public const string DESCRIPTION = "Description";
            public const string SYNTAX = "syntax";
            public const string KEYWORD = "fkKeyWord";
        }

        // Database stored procedures.
        public struct StoredProcedures
        {
            public const string SELECT = "";
            public const string SELECTALL = "mspGetSyntaxesByKeyword";
            public const string DELETE = "";
            public const string INSERT = "";
            public const string UPDATE = "";
        }

        // Database stored procedure arguments.
        public struct SPArguments
        {
            public const string ID = "@SyntaxID";
        }

        /// <summary>
        /// Constructor
        /// Initializes this KeywordSyntax object using the data stored in the specified DataRow
        /// with a reference back to its Keyword.  This method throws a NullReferenceException
        /// if either the row or keyword is null.
        /// </summary>
        /// <param name="Row">[DataRow] The database record containing the data used to hydrate
        /// this object.</param>
        /// <param name="Keyword">[Keyword] The keyword object to which this syntax belongs.</param>
        public KeywordSyntax(RatDataModelAdapter Adapter, Keyword Keyword) : base(Adapter)
        {
            InitializeComponents();

            if (Keyword != null)
                _keyword = Keyword;
            else
                throw new NullReferenceException("The Keyword for this KeywordSyntax cannot be null.");

            //if (Row != null)
            //{
            //    LoadDataRow(Row);
            //}
            //else
            //    throw new NullReferenceException("The DataRow record for a KeywordSyntax was null.  " +
            //        "Cannot initialize the KeywordSyntax.");
        }

        // The regular expression object used to validate all incoming command strings.
        // This object is instantiated when the _syntax property is set and will remain
        // for the life of the object.  This should increase response times by working
        // from a cached expression.
        private Regex _regex;

        // The syntax expression against which the command string must validate.
        private string _syntax;

        // The keyword that owns this syntax.
        private Keyword _keyword;

        // A collection of system instructions to be executed when a syntax match is found.  The
        // key is the SystemInstruction name.
        private ConcurrentDictionary<Guid, SystemInstruction> _instructions;

        // The possible types of argument in a keyword syntax expressions.
        public enum ArgumentType { Target, Ability, Location, Container, Message };

        public Keyword Keyword
        {
            get { return _keyword; }
        }

        public string Syntax
        {
            get { return _syntax; }
        }

        public Regex RegularExpression
        {
            get { return _regex; }
        }

        public List<SystemInstruction> Instructions
        {
            get { return new List<SystemInstruction>(_instructions.Select(item => item.Value)); }
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
        /// Deletes this syntax from the database.
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
        /// GetArgumentString
        /// Returns the portion of the command string that represents the specified argument.  In
        /// some cases, this could include multiple words.  The logic of this method must consider
        /// all possible syntactial variations to return the correct string.
        /// </summary>
        /// <param name="CommandString">[string] The command string as input by the user.</param>
        /// <param name="Argument">[ArgumentType] The portion of the command string to return.</param>
        /// <returns></returns>
        public string GetArgumentString(string CommandString, ArgumentType Argument)
        {
            MatchCollection mc = _regex.Matches(CommandString);
            GroupCollection gc = mc[0].Groups;
            switch (Argument)
            {
                case ArgumentType.Ability:
                    return gc["ability"].Value;
                    break;
                case ArgumentType.Container:
                    return gc["container"].Value;
                    break;
                case ArgumentType.Location:
                    return gc["location"].Value;
                    break;
                case ArgumentType.Target:
                    return gc["target"].Value;
                    break;
                case ArgumentType.Message:
                    return gc["message"].Value;
                    break;
                default:
                    throw new OperationFailedException("The specified argument type " +
                        Argument.ToString() + " does not exist within the syntax expression '" +
                        _syntax + "'.");
                    break;
            }
        }

        /// <summary>
        /// InitializeComponents
        /// Initializes all instance members for this object.
        /// </summary>
        public void InitializeComponents()
        {
            _instructions = new ConcurrentDictionary<Guid, SystemInstruction>();
        }

        /// <summary>
        /// InitializeRegex
        /// Initializes the Regex object used to validate keyword syntax expressions strings
        /// with standard options.
        /// </summary>
        private void InitializeRegex()
        {
            _regex = new Regex(_syntax, RegexOptions.ExplicitCapture);
        }

        /// <summary>
        /// IsSyntaxMatch
        /// Indicates whether the given command string match this KeywordSyntax.
        /// </summary>
        /// <param name="CommandString">[string] The command string to evaluate.</param>
        /// <returns>[bool] True if CommandString is a valid expression for this syntax,
        /// otherwise false.</returns>
        public bool IsSyntaxMatch(string CommandString)
        {
            if (_regex != null)
            {
                return _regex.IsMatch(CommandString);
            }
            throw new NullReferenceException("The Regex object required to handle " +
                "syntax validation of expression '" + CommandString + "' was not " +
                "initialized.");
        }

        public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// LoadDataRow
        /// Hydrates this keyword syntax using the data stored in the specified DataRow.
        /// </summary>
        /// <param name="Row">[DataRow] The database record containing the data used to hydrate
        /// this keyword syntax object.</param>
        //public override void LoadDataRow(DataRow Row)
        //{
        //    int tmp = 0;

        //    try
        //    {
        //        PopulatePropertyFromDataRow<int>(Row, Fields.ID, out this._id);
        //        //PopulatePropertyFromDataRow<string>(Row, Fields.NAME, out this._name);
        //        //PopulatePropertyFromDataRow<string>(Row, Fields.DESCRIPTION, out this._descr);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.SYNTAX, out this._syntax);

        //        PopulatePropertyFromDataRow<int>(Row, Fields.KEYWORD, out tmp);
        //        if (Keyword.ID != tmp)
        //            throw new OperationFailedException("The Keyword referenced when creating this KeywordSyntax " +
        //                "does not match the value stored in the database.");

        //        // Now is a good time to instantiate the regex object with the stored
        //        // syntax pattern.
        //        InitializeRegex();
        //        LoadInstructions();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// LoadInstructions
        /// Loads all system instructions for this keyword syntax from the database.  This method
        /// throws a new OperationFailedException if the load operation fails.  This method is
        /// called during server initialization.
        /// </summary>
        public void LoadInstructions()
        {
            RatDataModelAdapter a = new RatDataModelAdapter();
            a.Retrieve(RatDataModelType.SystemInstruction, null);
            
            for (int i = 0; i < a.ResultSet.RecordCount; i++)
            {
                a.ResultSet.MoveToRecord(i);
                SystemInstruction si = new SystemInstruction(a, this);
                _instructions.TryAdd(si.GameID, si);
            }
            //List<SqlParameter> p = new List<SqlParameter>();
            //p.Add(new SqlParameter(SPArguments.ID, _id));
            //RecordManager rm = new RecordManager();
            //DataTable dt = null;

            //try
            //{
            //    dt = rm.SendReadRequest(SystemInstruction.StoredProcedures.SELECTALL, p);
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

            //try
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        SystemInstruction s = new SystemInstruction(null, dr, this);
            //        if (!_instructions.TryAdd(s.ID.ToString(), s))
            //            throw new OperationFailedException("Could not add SystemInstruction " + s.Name +
            //                " to KeywordSyntax " + Name + ".");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        /// <summary>
        /// Save
        /// Saves this keyword syntax to the database or creates a new database record if this is a new
        /// syntax.
        /// </summary>
        /// <returns></returns>
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
