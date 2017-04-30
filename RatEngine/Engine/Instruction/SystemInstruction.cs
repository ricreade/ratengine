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
    public class SystemInstruction
    {
        

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
        public SystemInstruction(KeywordSyntax Syntax) 
        {
            
        }

        public virtual string Instruction { get; protected set; }

        public virtual InstructionSyntax Syntax { get; protected set; }

        public virtual int NextSequenceOnFail { get; protected set; }

        public virtual int NextSequenceOnSuccess { get; protected set; }

        public virtual string Notes { get; protected set; }

        public virtual int Sequence { get; protected set; }

        public virtual bool IsConforming()
        {
            if (ReferenceEquals(Syntax, null))
            {
                return false;
            }
            return Syntax.IsSyntaxMatch(Instruction);
        }
    }
}
