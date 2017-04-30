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
    public class InstructionSyntax
    {
        

        /// <summary>
        /// Constructor.  Accepts a DataRow containing the data required to hydrate this
        /// object.
        /// </summary>
        /// <param name="Row">[DataRow] The record data used to hydrate this object.</param>
        public InstructionSyntax() 
        {
            
        }

        public virtual string ExpressionString { get; protected set; }

        public virtual string Note { get; protected set; }

        public virtual Regex RegularExpression { get; protected set; }

        public virtual string ScriptClassName { get; protected set; }

        public virtual string ScriptName { get; protected set; }

        

        /// <summary>
        /// InitializeRegex
        /// Initializes the syntax Regex object using standard settings.
        /// </summary>
        private void InitializeRegex()
        {
            RegularExpression = new Regex(ExpressionString, RegexOptions.ExplicitCapture);
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
            if (RegularExpression == null)
            {
                InitializeRegex();
            }
            return RegularExpression.IsMatch(InstructionString);
            
        }
        
    }
}
