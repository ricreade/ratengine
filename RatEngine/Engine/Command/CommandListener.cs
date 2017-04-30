using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using RatEngine.Engine.Instruction;
using RatEngine.Engine.Scripting;

namespace RatEngine.Engine.Command
{
    /// <summary>
    /// Entity to respond to a user <see cref="GameCommand"/>.
    /// </summary>
    /// <remarks>This class provides a means to respond to a <see cref="GameCommand"/> 
    /// issued by a player.  The listener determines whether the command complies with 
    /// the expected expression.  If it does, the listener executes a pre-defined series
    /// of instructions against the command.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class CommandListener
    {
        
        private Regex _regex;

        public CommandListener() { }

        public CommandListener(string expression)
        {
            RegularExpressionString = expression;
        }

        [DataMember]
        public virtual string RegularExpressionString { get; protected set; }

        [DataMember]
        public virtual List<SystemInstruction> Instructions { get; protected set; }

        public virtual void AddInstruction(SystemInstruction instruction)
        {
            if (instruction != null)
            {
                if (Instructions == null)
                    Instructions = new List<SystemInstruction>();
                Instructions.Add(instruction);
            }
        }

        public virtual CommandListener Clone()
        {
            CommandListener listener = new CommandListener(RegularExpressionString);
            foreach (SystemInstruction instruction in Instructions)
            {
                listener.AddInstruction(instruction);
            }
            return listener;
        }

        public virtual bool HasInstruction(SystemInstruction instruction)
        {
            return false;
        }

        private void InvokeInstructions(GameCommand command)
        {
            if (IsPatternMatch(command.CommandString))
            {
                foreach (SystemInstruction instruction in Instructions)
                {
                    IScriptRequest request = ScriptUtil.CreateRequest(command, instruction);
                    IScriptResult result = ScriptUtil.ExecuteRequest(request);
                }
            }
        }

        public bool IsPatternMatch(string commandString)
        {
            if (_regex == null)
                _regex = new Regex(RegularExpressionString);
            return _regex.IsMatch(commandString);
        }

        public Task ProcessCommand(GameCommand command)
        {
            return Task.Run(() => InvokeInstructions(command));
        }

        public virtual bool RemoveInstruction(SystemInstruction instruction)
        {
            if (Instructions == null)
                Instructions = new List<SystemInstruction>();
            return Instructions.Remove(instruction);
        }
    }
}
