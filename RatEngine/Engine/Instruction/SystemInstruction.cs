using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel;
using RatEngine.DataSource;
using RatEngine.Engine.Command;

namespace RatEngine.Engine.Instruction
{
    /// <summary>
    /// This class defines a specific instruction the application must follow to carry out
    /// execution of a KeywordSyntax.  This instruction is intended to serve a similar purpose
    /// as a line of machine code that tells the application what methods to call and what
    /// decisions to make.  Objects of this class are hydrated at service start up and are 
    /// read only thereafter.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class SystemInstruction : GameElement
    {
        // Database field names.
        public struct Fields
        {
            public const string ID = "ID";
            public const string INSTRUCTION = "instruction";
            public const string SYNTAX_ID = "fkSyntax";
            public const string SYSINSTR_ID = "fkSysInstr";
            public const string SEQUENCE = "sequence";
            public const string NEXT_ON_SUCCESS = "nextOnSuccess";
            public const string NEXT_ON_FAIL = "nextOnFail";
            public const string NOTE = "note";

            //public const string NAME = "Name";
            //public const string DESCRIPTION = "Description";
        }

        // Database stored procedures.
        public struct StoredProcedures
        {
            public const string SELECT = "";
            public const string SELECTALL = "mspGetInstructionsBySyntax";
            public const string DELETE = "";
            public const string INSERT = "";
            public const string UPDATE = "";
        }

        public struct SPArguments
        {
            public const string ID = "Id";
        }

        /// <summary>
        /// Constructor
        /// Initializes this SystemInstruction using the data from the specified DataRow with a
        /// reference back to the KeywordSyntax containing this instruction.  This method throws
        /// a new NullReferenceException if either the DataRow or KeywordSyntax is null.
        /// </summary>
        /// <param name="Row">[DataRow] The database record containing the data used to hydrate
        /// this object.</param>
        /// <param name="Syntax">[KeywordSyntax] A reference to the syntax object containing
        /// this instruction.</param>
        public SystemInstruction(RatDataModelAdapter Adapter, KeywordSyntax Syntax) : base(Adapter)
        {
            if (Syntax != null)
                _kwrdsyntax = Syntax;
            else
                throw new NullReferenceException("The keyword syntax for an instruction cannot be null.");

            //if (Row != null)
            //    LoadDataRow(Row);
            //else
            //    throw new NullReferenceException("The DataRow record for a SystemInstruction was null.  " +
            //        "Cannot initialize the SystemInstruction.");
        }

        // The position of this instruction in sequence.  This can also be considered the line number
        // of this instruction in the instruction set.
        private int _seq;

        // The next instruction in the sequence to execute if this instruction is executed successfully.
        private int _nextonsuccess;

        // The next instruction in the sequence to execute if this instruction execution fails.
        private int _nextonfail;

        // The instruction string.
        private string _instr;

        // The instruction expression containing the regular expression against which this instruction
        // must validate.
        private InstructionSyntax _instrexpr;

        // The specific keyword syntax for which this instruction is part of the implementation.
        private KeywordSyntax _kwrdsyntax;

        // An optional note that may be stored with this syntax instruction for documentation
        // purposes.
        private string _note;

        // Indicates whether failure to follow the specified instruction should cause the keyword
        // to fail.
        //private bool _isstoponfail;

        // An integer value associated with this instruction.
        //private int _modval;

        public int Sequence
        {
            get { return _seq; }
        }

        public int NextSeqOnSuccess
        {
            get { return _nextonsuccess; }
        }

        public int NextSeqOnFail
        {
            get { return _nextonfail; }
        }

        public string Instruction
        {
            get { return _instr; }
        }

        public InstructionSyntax ExpressionSyntax
        {
            get { return _instrexpr; }
        }

        public KeywordSyntax KeywordSyntax
        {
            get { return _kwrdsyntax; }
        }

        public string Note
        {
            get { return _note; }
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
        /// Returns the argument string as specified by the specified argument type.
        /// </summary>
        /// <param name="Argument">[InstructionSyntax.ArgumentType] The type of argument
        /// whose string should be returned.</param>
        /// <returns>[string] The portion of the instruction representing the specified
        /// argument.</returns>
        public string GetArgumentString(InstructionSyntax.ArgumentType Argument)
        {
            return ExpressionSyntax.GetArgumentString(Instruction, Argument);
        }

        /// <summary>
        /// HasArgumentString
        /// Indicates whether the instruction contains the specified argument.
        /// </summary>
        /// <param name="Argument">[InstructionSyntax.ArgumentType] The type of
        /// argument to look for.</param>
        /// <returns>[bool] True if the specified argument is present, otherwise false.</returns>
        public bool HasArgumentString(InstructionSyntax.ArgumentType Argument)
        {
            try
            {
                ExpressionSyntax.GetArgumentString(Instruction, Argument);
            }
            catch (OperationFailedException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }

        /// <summary>
        /// LoadDataRow
        /// Hydrates this keyword syntax using the data stored in the specified DataRow.
        /// </summary>
        /// <param name="Row">[DataRow] The database record containing the data used to hydrate
        /// this system instruction object.</param>
        //public override void LoadDataRow(DataRow Row)
        //{
        //    int tmp = 0;
        //    try
        //    {
        //        PopulatePropertyFromDataRow<int>(Row, Fields.ID, out this._id);
        //        //PopulatePropertyFromDataRow<string>(Row, Fields.NAME, out this._name);
        //        //PopulatePropertyFromDataRow<string>(Row, Fields.DESCRIPTION, out this._descr);
        //        PopulatePropertyFromDataRow<int>(Row, Fields.NEXT_ON_FAIL, out this._nextonfail);
        //        PopulatePropertyFromDataRow<int>(Row, Fields.NEXT_ON_SUCCESS, out this._nextonsuccess);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.INSTRUCTION, out this._instr);
        //        PopulatePropertyFromDataRow<string>(Row, Fields.NOTE, out this._note);
        //        PopulatePropertyFromDataRow<int>(Row, Fields.SEQUENCE, out this._seq);

        //        // Verify that the keywordsyntax object passed to this object's constructor matches
        //        // what the database record predicted.
        //        PopulatePropertyFromDataRow<int>(Row, Fields.SYNTAX_ID, out tmp);
        //        if (tmp != _kwrdsyntax.ID)
        //            throw new OperationFailedException("The KeywordSyntax referenced when creating this " +
        //                "SystemInstruction does not match the value stored in the database.");

        //        // Instantiate the InstructionSyntax from this row.
        //        _instrexpr = new InstructionSyntax(null, Row);

        //        // The instruction must validate against this instruction syntax.
        //        if (!_instrexpr.IsSyntaxMatch(_instr))
        //            throw new OperationFailedException("The instruction '" + _instr +
        //                "' failed to validate against the regular expression '" +
        //                _instrexpr.RegularExpression.ToString() + "'.");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

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
