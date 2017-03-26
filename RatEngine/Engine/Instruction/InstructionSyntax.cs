using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using RatEngine.DataModel;
using RatEngine.DataSource;

namespace RatEngine.Engine.Instruction
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class InstructionSyntax : GameElement
    {
        // Database field names.
        public struct Fields
        {
            public const string ID = "ID1";
            //public const string NAME = "Name";
            //public const string DESCRIPTION = "Description";
            public const string SYNTAX = "regExpression";
            public const string NOTE = "note1";
        }

        // Database stored procedures.
        public struct StoredProcedures
        {
            public const string SELECT = "";
            public const string SELECTALL = "";
            public const string DELETE = "";
            public const string INSERT = "";
            public const string UPDATE = "";
        }

        // Stored procedure arguments.
        public struct SPArguments
        {
            public const string ID = "Id";
        }

        /// <summary>
        /// Constructor.  Accepts a DataRow containing the data required to hydrate this
        /// object.
        /// </summary>
        /// <param name="Row">[DataRow] The record data used to hydrate this object.</param>
        public InstructionSyntax() 
        {
            //if (Row != null)
            //{
            //    LoadDataRow(Row);
            //}
            //else
            //    throw new NullReferenceException("The DataRow record for an InstructionSyntax was null.  " +
            //        "Cannot initialize the InstructionSyntax.");
        }

        // The regular expression object used to validate all incoming command strings.
        // This object is instantiated when the _syntax property is set and will remain
        // for the life of the object.  This should increase response times by working
        // from a cached expression.
        private Regex _regex;

        // The regex expression against which the instruction string must validate.
        private string _syntax;

        // An optional note that may be stored with this syntax expression for documentation
        // purposes.
        private string _note;

        // The possible types of argument in an instruction syntax expressions.
        public enum ArgumentType
        {
            Action, Other, Caller, Keyword, Property, Room, EndLine, Value, Message,
            From, To, Target, Ability, Location, Container, AppendProperty,
            AppendMessage, AppendValue
        };

        public string Note
        {
            get { return _note; }
        }

        public string Syntax
        {
            get { return _syntax; }
        }

        public Regex RegularExpression
        {
            get { return _regex; }
        }

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
        //    throw new NotImplementedException();
        //}

        //public override bool Delete(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// GetArgumentString
        /// Returns the argument string from the instruction string as specified by the
        /// argument type.
        /// </summary>
        /// <param name="InstructionString">[string] The text of the instruction.</param>
        /// <param name="Argument">[ArgumentType] The specific argument whose text this
        /// method should return.</param>
        /// <returns>[string] The text of the instruction argument.</returns>
        public string GetArgumentString(string InstructionString, ArgumentType Argument)
        {
            MatchCollection mc = _regex.Matches(InstructionString);
            GroupCollection gc = mc[0].Groups;
            Group g = null;
            switch (Argument)
            {
                case ArgumentType.Ability:
                    g = gc["ability"];
                    break;
                case ArgumentType.Action:
                    g = gc["action"];
                    break;
                case ArgumentType.Other:
                    g = gc["other"];
                    break;
                case ArgumentType.Caller:
                    g = gc["caller"];
                    break;
                case ArgumentType.Container:
                    g = gc["container"];
                    break;
                case ArgumentType.EndLine:
                    g = gc["endline"];
                    break;
                case ArgumentType.From:
                    g = gc["from"];
                    break;
                case ArgumentType.Keyword:
                    g = gc["keyword"];
                    break;
                case ArgumentType.Location:
                    g = gc["location"];
                    break;
                case ArgumentType.Message:
                    g = gc["message"];
                    break;
                case ArgumentType.Property:
                    g = gc["property"];
                    break;
                case ArgumentType.Room:
                    g = gc["room"];
                    break;
                case ArgumentType.Target:
                    g = gc["target"];
                    break;
                case ArgumentType.To:
                    g = gc["to"];
                    break;
                case ArgumentType.Value:
                    g = gc["value"];
                    break;
                case ArgumentType.AppendMessage:
                    g = gc["appendmsg"];
                    break;
                case ArgumentType.AppendProperty:
                    g = gc["appendprop"];
                    break;
                case ArgumentType.AppendValue:
                    g = gc["appendval"];
                    break;
                default:
                    //return "";
                    throw new OperationFailedException("The specified argument type " +
                        Argument.ToString() + " does not exist within the syntax expression '" +
                        _syntax + "'.");
                    break;
            }

            if (g.Success)
                return g.Value;
            else
                throw new OperationFailedException("The specified argument type " +
                        Argument.ToString() + " does not exist within the syntax expression '" +
                        _syntax + "'.");
        }

        /// <summary>
        /// InitializeRegex
        /// Initializes the syntax Regex object using standard settings.
        /// </summary>
        private void InitializeRegex()
        {
            _regex = new Regex(_syntax, RegexOptions.ExplicitCapture);
        }

        /// <summary>
        /// IsSyntaxMatch
        /// Indicates whether the specified instruction string is valid per the local
        /// Regex object.
        /// </summary>
        /// <param name="InstructionString">[string] The instruction string to validate.</param>
        /// <returns>[bool] True if the instruction string is valid, otherwise false.</returns>
        public bool IsSyntaxMatch(string InstructionString)
        {
            if (_regex != null)
            {
                return _regex.IsMatch(InstructionString);
            }
            throw new NullReferenceException("The Regex object required to handle " +
                "syntax validation of expression '" + InstructionString + "' was not " +
                "initialized.");
        }

        /// <summary>
        /// LoadDataRow
        /// Hydrates this object from the data stored in the specified DataRow.
        /// </summary>
        /// <param name="Row">[DataRow] The data record used to hydrate this object.</param>
        //public override void LoadDataRow(DataRow Row)
        //{
        //    try
        //    {
        //        PopulatePropertyFromDataRow<int>(Row, Fields.ID, out this._id);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.SYNTAX, out this._syntax);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.NOTE, out this._note);

        //        // Now is a good time to instantiate the regex object with the stored
        //        // syntax pattern.
        //        InitializeRegex();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Save
        /// Saves chagnes to this instruction syntax to the database or creates a new record in the
        /// database if this is a new syntax.
        /// </summary>
        /// <returns>[bool] True if the save was successful, otherwise false.</returns>
        //public override bool Save()
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool Save(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
