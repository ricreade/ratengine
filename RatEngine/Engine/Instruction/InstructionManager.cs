using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel;
using RatEngine.DataModel.Effects;
using RatEngine.DataModel.Inventory;
using RatEngine.DataModel.Mob;
using RatEngine.DataModel.World;
using RatEngine.Engine.Command;
using RatEngine.Engine.Instruction;

namespace RatEngine.Engine.Instruction
{
    public class InstructionManager
    {

        //private const string NEW_LINE = "&lt;br&nbsp;/&gt;";
        private const string NEW_LINE = "&lt;br&gt;";

        /// <summary>
        /// ExecuteInstructions
        /// Iterates through all instructions applicable to the keyword syntax and executes
        /// them in turn.  An InstructionResultSet object acts as a collection of registers
        /// to record values between instruction calls.
        /// </summary>
        /// <param name="CommandString">[string] The original input command string from the
        /// user.</param>
        /// <param name="Keyword">[Keyword] The keyword object representing the input
        /// command string.</param>
        /// <param name="Syntax">[KeywordSyntax] The syntax object representing the specific
        /// keyword syntax used in the input command string.</param>
        /// <param name="AssociatedFlags">[IEnumerable<Flag>] The collection of flags associated
        /// with this keyword syntax that must be overcome to execute certain instructions.</param>
        /// <param name="Caller">[Combatant] A reference to the PlayerCharacter or NonPlayerCharacter
        /// that initiated this action.</param>
        /// <returns>[Task<Response>] A task containing the total set of response messages to
        /// send back to the user(s).</returns>
        public async Task<Response> ExecuteInstructions(string CommandString, Keyword Keyword,
            KeywordSyntax Syntax, IEnumerable<Flag> AssociatedFlags, Combatant Caller)
        {
            InstructionResultSet result = new InstructionResultSet()
            {
                NextInstruction = 1,
                LoopCounter = 0,
                AggregateString = "",
                StandardString = "",
                ArgumentName = "",
                GameObj = null,
                Response = new Response(),
                Settings = new InstructionResultSet.ResponseSettings()
            };     // An instruction result

            // Execute instructions until the instruction counter is set to -1.
            // This signals that the instruction execution is complete.  The next instruction value
            // should never point to an instruction beyond the last SystemInstruction.  This indicates
            // a database error in the syntax instruction set.
            do
            {
                if (result.NextInstruction <= Syntax.Instructions.Count)
                    result = (await ExecuteInstruction(Syntax.Instructions.Find(x => x.Sequence == result.NextInstruction), /*[result.NextInstruction], */
                                result, CommandString, Keyword, Syntax, AssociatedFlags, Caller));
                else
                    throw new OperationFailedException("A SystemInstruction attempted to direct " +
                        "application flow beyond the last instruction.  CommandString: " +
                        CommandString + ".  Syntax: " + Syntax.Syntax + ".");

            } while (result.NextInstruction > 0);

            //ResolveResponseMessages(Caller, result, Syntax);

            return result.Response;
        }

        /// <summary>
        /// ExecuteInstruction
        /// Executes a specific instruction.
        /// </summary>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers to track between
        /// instruction executions.</param>
        /// <param name="CommandString">[string] The original input command string.</param>
        /// <param name="Keyword">[Keyword] The keyword object representing the input command string.</param>
        /// <param name="Syntax">[KeywordSyntax] The specific syntax of the input command string.</param>
        /// <param name="AssociatedFlags">[IEnumerable<Flag>] The collection of flags that must be
        /// overcome to execute certain instructions for this syntax.</param>
        /// <param name="Caller">[Combatant] The caller of this instruction.</param>
        /// <returns>[Task<InstructionResultSet>] A task containing the resulting registers from the
        /// instruction execution.</returns>
        private Task<InstructionResultSet> ExecuteInstruction(SystemInstruction Instruction, InstructionResultSet ResultSet,
            string CommandString, Keyword Keyword, KeywordSyntax Syntax, IEnumerable<Flag> AssociatedFlags, Combatant Caller)
        {
            Task<InstructionResultSet> t = null;

            try
            {
                // Create a new task to handle the instruction execution.
                t = Task.Factory.StartNew<InstructionResultSet>(() =>
                {
                    switch (Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Keyword))
                    {
                        case "getargname":
                            GetArgName(CommandString, Instruction, Syntax, ResultSet);
                            break;
                        case "get":
                            GetGameObject(Instruction, Caller, ResultSet);
                            break;
                        case "assert":
                            break;
                        case "build messages":
                            ResolveResponseMessages(Instruction, Caller, Syntax, ResultSet);
                            break;
                        case "clear":
                            Clear(Instruction, ResultSet);
                            break;
                        case "compflags":
                            break;
                        case "addobj":
                            break;
                        case "remobj":
                            break;
                        case "setcounter":
                            SetCounter(Instruction, ResultSet);
                            break;
                        case "append":
                            Append(CommandString, Instruction, Syntax, Caller, ResultSet);
                            break;
                        case "inccounter":
                            try
                            {
                                ResultSet.LoopCounter++;
                                ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
                            }
                            catch (Exception ex)
                            {
                                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                            }
                            break;
                        case "checkcounter":
                            CheckCounter(Instruction, Caller, ResultSet);
                            break;
                        case "send":
                            break;
                        case "adjstat":
                            break;
                        case "invokeaction":
                            break;
                        case "checkhit":
                            break;
                        case "applydamage":
                            break;
                        case "applyflags":
                            break;
                        case "asserthasability":
                            break;
                        case "move":
                            Move(Instruction, Caller, ResultSet);
                            break;
                        case "setresponsesettings":
                            SetResponseSettings(Instruction, ResultSet);
                            break;
                        case "executecommandstring":
                            ExecuteCommandString(Instruction, Caller, ResultSet);
                            break;
                        default:
                            throw new OperationFailedException("An unexpected keyword was found in instruction '" +
                                Instruction.Instruction + "'.");
                    }
                    return ResultSet;
                });
            }
            catch (AggregateException ex)
            {

            }

            return t;
        }

        /// <summary>
        /// Append
        /// Determines which overloaded version to Append to execute for the specified instruction.
        /// This method takes all arguments required for any of the overloaded methods.
        /// </summary>
        /// <param name="CommandString">[string] The original input command string.</param>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="Syntax">[KeywordSyntax] The syntax of the input command string.</param>
        /// <param name="Caller">[Combatant] The combatant that initiated the command.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool Append(string CommandString, SystemInstruction Instruction, KeywordSyntax Syntax,
                            Combatant Caller, InstructionResultSet ResultSet)
        {
            try
            {
                if (Instruction.HasArgumentString(InstructionSyntax.ArgumentType.AppendMessage))
                {
                    // Append the command string message to the specified resultset string
                    return Append(CommandString, Instruction, Syntax, ResultSet);
                }
                else if (Instruction.HasArgumentString(InstructionSyntax.ArgumentType.AppendValue))
                {
                    // Append the value stored in the instruction to the specified resultset string
                    return Append(Instruction, ResultSet);
                }
                else if (Instruction.HasArgumentString(InstructionSyntax.ArgumentType.AppendProperty))
                {
                    // Append the specified object property to the specified resultset string
                    return Append(Instruction, Caller, ResultSet);
                }
                else
                    throw new OperationFailedException("Unknown append type: '" + Instruction.ExpressionSyntax.Syntax + "'.");
            }
            catch (Exception ex)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }
        }

        /// <summary>
        /// Append
        /// Appends the message specified in the command string to the result set register.
        /// </summary>
        /// <param name="CommandString">[string] The original input command string.</param>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="Syntax">[KeywordSyntax] The syntax of the input command string.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool Append(string CommandString, SystemInstruction Instruction, KeywordSyntax Syntax, InstructionResultSet ResultSet)
        {
            try
            {
                string trg = Syntax.GetArgumentString(CommandString, KeywordSyntax.ArgumentType.Message);
                string edl = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.EndLine);
                string mto = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.To);

                bool endline = false;
                if (!Boolean.TryParse(edl, out endline))
                    throw new OperationFailedException("Boolean conversion failed for '" + edl + "' in '" + Instruction.Instruction + "'.");

                // Determine which register property to modify.
                if (mto == "aggregate")
                    ResultSet.AggregateString += trg + (endline ? NEW_LINE : "");
                else if (mto == "standard")
                    ResultSet.StandardString += trg + (endline ? NEW_LINE : "");
                else
                    throw new OperationFailedException("Unknown <to> type in instruction '" + Instruction.Instruction + "'.");
            }
            catch (Exception ex)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }
            ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
            return true;
        }

        /// <summary>
        /// Append
        /// Appends the value specified in the instruction to the result register.
        /// </summary>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool Append(SystemInstruction Instruction, InstructionResultSet ResultSet)
        {
            try
            {
                string val = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Value);
                string edl = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.EndLine);
                string mto = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.To);

                bool endline = false;
                if (!Boolean.TryParse(edl, out endline))
                    throw new OperationFailedException("Boolean conversion failed for '" + edl + "' in '" + Instruction.Instruction + "'.");

                // Determine which register property to modify.
                if (mto == "aggregate")
                    ResultSet.AggregateString += val + (endline ? NEW_LINE : "");
                else if (mto == "standard")
                    ResultSet.StandardString += val + (endline ? NEW_LINE : "");
                else
                    throw new OperationFailedException("Unknown <to> type in instruction '" + Instruction.Instruction + "'.");

                ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
                return true;
            }
            catch (Exception ex)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }
        }

        /// <summary>
        /// Append
        /// Appends the game object property to the result register as specified in the instruction.
        /// </summary>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="Caller">[Combatant] The combatant object that initiated this action.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool Append(SystemInstruction Instruction, Combatant Caller, InstructionResultSet ResultSet)
        {
            string resp = "";
            string mto = "";
            string trg = "";
            string prp = "";
            string edl = "";
            bool endline = false;

            try
            {
                trg = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Target);
                prp = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Property);
                edl = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.EndLine);
                mto = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.To);

                if (!Boolean.TryParse(edl, out endline))
                    throw new OperationFailedException("Boolean conversion failed for '" + edl + "' in '" +
                        Instruction.Instruction + "'.");

                // Find determine which object to reference and which property to return.
                // Note: at this time, only properties required for look, move, and say are implemented.
                switch (trg)
                {
                    case "item":
                        switch (prp)
                        {
                            case "name":
                                resp += ResultSet.GameObj.Name + (endline ? NEW_LINE : "");
                                break;
                            case "description":
                                resp += ResultSet.GameObj.Description + (endline ? NEW_LINE : "");
                                break;
                        }
                        break;
                    case "combatant":
                        switch (prp)
                        {
                            case "name":
                                resp += ResultSet.GameObj.Name + (endline ? NEW_LINE : "");
                                break;
                        }
                        break;
                    case "caller":
                        switch (prp)
                        {
                            case "name":
                                resp += Caller.Name + (endline ? NEW_LINE : "");
                                break;
                        }
                        break;
                    case "pc":
                        switch (prp)
                        {
                            case "name":
                                resp += ResultSet.GameObj.Name + (endline ? NEW_LINE : "");
                                break;
                        }
                        break;
                    case "npc":
                        switch (prp)
                        {
                            case "name":
                                resp += ResultSet.GameObj.Name + (endline ? NEW_LINE : "");
                                break;
                        }
                        break;
                    case "transition":
                        switch (prp)
                        {
                            case "name":
                                resp += ResultSet.GameObj.Name + (endline ? NEW_LINE : "");
                                break;
                            case "roomfrom":
                                resp += ((Transition)ResultSet.GameObj).DescriptionFrom + (endline ? NEW_LINE : "");
                                break;
                            case "roomto":
                                resp += ((Transition)ResultSet.GameObj).DescriptionTo + (endline ? NEW_LINE : "");
                                break;
                        }
                        break;
                    case "room":
                        switch (prp)
                        {
                            case "name":
                                resp += Caller.Location.Name + (endline ? NEW_LINE : "");
                                break;
                            case "description":
                                resp += Caller.Location.Description + (endline ? NEW_LINE : "");
                                break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }

            // Determine which register property to modify.
            if (mto == "aggregate")
                ResultSet.AggregateString += resp;
            else if (mto == "standard")
                ResultSet.StandardString += resp;
            else
                throw new OperationFailedException("Unknown <to> type in instruction '" + Instruction.Instruction + "'.");

            ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
            return true;
        }

        /// <summary>
        /// CheckCounter
        /// Checks the current register counter against the object collection count as specified in the
        /// instruction.  The instruction is considered successful if the register counter is less than
        /// the collection count and a failure if hte counter is equal to or greater than the collection
        /// count.
        /// </summary>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="Caller">[Combatant] The combatant object that initiated this action.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool CheckCounter(SystemInstruction Instruction, Combatant Caller, InstructionResultSet ResultSet)
        {
            try
            {
                string trg = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Target);
                string prp = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Property);

                // Compare the loop counter to the appropriate collection count and set the next instruction
                // value based on the result.
                // Note: at this time, only room counter checks are implemented.
                switch (trg)
                {
                    case "room":
                        switch (prp)
                        {
                            case "transition":
                                ResultSet.NextInstruction = ResultSet.LoopCounter < Caller.Location.Transitions.Count() ?
                                    Instruction.NextSeqOnSuccess : Instruction.NextSeqOnFail;
                                break;
                            case "item":
                                ResultSet.NextInstruction = ResultSet.LoopCounter < Caller.Location.Inventory.Count() ?
                                    Instruction.NextSeqOnSuccess : Instruction.NextSeqOnFail;
                                break;
                            case "combatant":
                                ResultSet.NextInstruction = ResultSet.LoopCounter < Caller.Location.Combatants.Count() ?
                                    Instruction.NextSeqOnSuccess : Instruction.NextSeqOnFail;
                                break;
                            case "pc":
                                ResultSet.NextInstruction = ResultSet.LoopCounter < Caller.Location.Combatants.OfType<PlayerCharacter>().Count() ?
                                    Instruction.NextSeqOnSuccess : Instruction.NextSeqOnFail;
                                break;
                            case "npc":
                                ResultSet.NextInstruction = ResultSet.LoopCounter < Caller.Location.Combatants.OfType<NonPlayerCharacter>().Count() ?
                                    Instruction.NextSeqOnSuccess : Instruction.NextSeqOnFail;
                                break;
                            default:
                                throw new OperationFailedException("Unknown syntax in '" + Instruction.Instruction + "'.");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
            }

            return ResultSet.NextInstruction == Instruction.NextSeqOnSuccess;
        }

        /// <summary>
        /// Clear
        /// Clears the register string as specified by the instruction.
        /// </summary>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool Clear(SystemInstruction Instruction, InstructionResultSet ResultSet)
        {
            try
            {
                // Determine which register string to clear.
                string mto = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.To);
                switch (mto)
                {
                    case "aggregate":
                        ResultSet.AggregateString = "";
                        break;
                    case "standard":
                        ResultSet.StandardString = "";
                        break;
                    default:
                        throw new OperationFailedException("Unknown syntax in '" + Instruction.Instruction + "'.");
                }
                ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
                return true;
            }
            catch (Exception ex)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }
        }

        /// <summary>
        /// ExecuteCommandString
        /// Executes the string as specified in the instruction as though it were entered by the specified
        /// caller.  The resulting InstructionResultSet is combined with the InstructionResultSet in this
        /// instruction execution.
        /// </summary>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="Caller">[Combatant] The combatant object that initiated this action.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool ExecuteCommandString(SystemInstruction Instruction, Combatant Caller, InstructionResultSet ResultSet)
        {
            Task<Response> t = null;
            try
            {
                string arg = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Value);
                Keyword k = KeywordManager.GetKeyword(arg);
                t = k.ExecuteCommandString(arg, Caller);
                t.Wait();
                ResultSet.Response.AddResponseMessages(t.Result);
            }
            catch (Exception ex)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }
            ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
            return true;
        }

        /// <summary>
        /// GetArgName
        /// Gets the argument string from the original input command string as specified by the instruction.
        /// </summary>
        /// <param name="CommandString">[string] The original input command string.</param>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="Syntax">[KeywordSyntax] The syntax of the input instruction.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool GetArgName(string CommandString, SystemInstruction Instruction, KeywordSyntax Syntax, InstructionResultSet ResultSet)
        {
            try
            {
                string arg = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Target);

                switch (arg)
                {
                    case "target":
                        ResultSet.ArgumentName = Syntax.GetArgumentString(CommandString, KeywordSyntax.ArgumentType.Target);
                        break;

                    case "location":
                        ResultSet.ArgumentName = Syntax.GetArgumentString(CommandString, KeywordSyntax.ArgumentType.Location);
                        break;

                    case "container":
                        ResultSet.ArgumentName = Syntax.GetArgumentString(CommandString, KeywordSyntax.ArgumentType.Container);
                        break;

                    case "ability":
                        ResultSet.ArgumentName = Syntax.GetArgumentString(CommandString, KeywordSyntax.ArgumentType.Ability);
                        break;
                }
            }
            catch (Exception ex)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }

            if (ResultSet.ArgumentName.Length > 0)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
                return true;
            }
            else
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }
        }

        /// <summary>
        /// GetGameObject
        /// Obtains a GameElement reference as specified by the instruction and records it in
        /// the InstructionResultSet register for later instructions.
        /// </summary>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="Caller">[Combatant] The combatant object that initiated this action.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool GetGameObject(SystemInstruction Instruction, Combatant Caller, InstructionResultSet ResultSet)
        {
            try
            {
                // Get the instruction arguments that specify the location (container) of the GameElement,
                // The type of GameElement, and the property (either index or name) to reference.
                string trg = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Target);
                string cnt = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Container);
                string prp = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Property);

                // Reference the appropriate object.
                // Note: Not all possible combinations are represented at this time.  The current implementation
                // is intended to support only the look, move, and say commands.
                switch (cnt)
                {
                    case "room":
                        switch (trg)
                        {
                            case "item":
                                switch (prp)
                                {
                                    case "name":
                                        ResultSet.GameObj = Caller.Location.GetItem(ResultSet.ArgumentName);
                                        break;
                                    case "index":
                                        ResultSet.GameObj = Caller.Location.Inventory.ToList()[ResultSet.LoopCounter];
                                        break;
                                }
                                break;

                            case "pc":
                                switch (prp)
                                {
                                    case "name":
                                        ResultSet.GameObj = Caller.Location.GetCombatant(ResultSet.ArgumentName);
                                        break;
                                    case "index":
                                        ResultSet.GameObj = Caller.Location.Combatants.OfType<PlayerCharacter>().ToList()[ResultSet.LoopCounter];
                                        break;
                                }
                                break;

                            case "npc":
                                switch (prp)
                                {
                                    case "name":
                                        ResultSet.GameObj = Caller.Location.GetCombatant(ResultSet.ArgumentName);
                                        break;
                                    case "index":
                                        ResultSet.GameObj = Caller.Location.Combatants.OfType<NonPlayerCharacter>().ToList()[ResultSet.LoopCounter];
                                        break;
                                }
                                break;

                            case "combatant":
                                ResultSet.GameObj = Caller.Location.GetCombatant(ResultSet.ArgumentName);
                                break;

                            case "transition":
                                switch (prp)
                                {
                                    case "name":
                                        ResultSet.GameObj = Caller.Location.GetTransition(ResultSet.ArgumentName);
                                        break;
                                    case "index":
                                        ResultSet.GameObj = Caller.Location.Transitions.ToList()[ResultSet.LoopCounter];
                                        break;
                                }
                                break;

                        }
                        break;
                    case "inventory":
                        ResultSet.GameObj = Caller.GetItem(ResultSet.ArgumentName);
                        break;
                    case "container":
                        ResultSet.GameObj = ((Inventoried)ResultSet.GameObj).GetItem(ResultSet.ArgumentName);
                        break;
                }
            }
            catch (Exception ex)
            {
                ResultSet.GameObj = null;
            }

            if (ResultSet.GameObj != null)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
                return true;
            }
            else
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }
        }

        /// <summary>
        /// Move
        /// Causes the specified combatant to move to the transition as referenced in the
        /// InstructionResultSet register.
        /// </summary>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="Caller">[Combatant] The combatant object that initiated this action.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool Move(SystemInstruction Instruction, Combatant Caller, InstructionResultSet ResultSet)
        {
            try
            {
                Caller.Move((Transition)ResultSet.GameObj);
            }
            catch (Exception ex)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }

            ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
            return true;
        }

        /// <summary>
        /// ResolveResponseMessages
        /// Builds the messages according to the current response settings as recorded in the register.
        /// </summary>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="Caller">[Combatant] The combatant object that initiated this action.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <param name="Syntax">[KeywordSyntax] The specific keyword syntax of the input string.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool ResolveResponseMessages(SystemInstruction Instruction, Combatant Caller,
            KeywordSyntax Syntax, InstructionResultSet ResultSet)
        {
            try
            {
                // Build messages for all PlayerCharacter objects in the caller's current room.  Treat
                // message building for the caller and other PlayerCharacters differently.
                foreach (PlayerCharacter pc in Caller.Location.Combatants.OfType<PlayerCharacter>())
                {
                    if (pc.Equals(Caller))
                    {
                        ResultSet.ApplyResponse(Caller, pc, Syntax);
                    }
                    else
                    {
                        ResultSet.ApplyResponse(Caller, pc, Syntax);
                    }
                }
                ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
                return true;
            }
            catch (Exception ex)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }
        }

        /// <summary>
        /// SetCounter
        /// Initializes the InstructionResultSet register counter to the value specified in the
        /// instruction.
        /// </summary>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool SetCounter(SystemInstruction Instruction, InstructionResultSet ResultSet)
        {
            int arg = 0;
            if (Int32.TryParse(Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Value), out arg))
            {
                ResultSet.LoopCounter = arg;
                ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
                return true;
            }
            else
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }
        }

        /// <summary>
        /// SetResponseSettings
        /// Sets the rules for which messages are constructed for users.
        /// </summary>
        /// <param name="Instruction">[SystemInstruction] The instruction to execute.</param>
        /// <param name="ResultSet">[InstructionResultSet] The set of registers for
        /// instruction execution.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool SetResponseSettings(SystemInstruction Instruction, InstructionResultSet ResultSet)
        {
            try
            {
                // Get the instruction arguments for each type.
                string clr = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Caller);
                string trg = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Target);
                string rm = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Room);
                string othr = Instruction.GetArgumentString(InstructionSyntax.ArgumentType.Other);

                // Set the register settings.
                ResultSet.Settings.Caller = GetResponseType(clr);
                ResultSet.Settings.Target = GetResponseType(trg);
                ResultSet.Settings.Room = GetResponseType(rm);
                ResultSet.Settings.Other = GetResponseType(othr);
            }
            catch (Exception ex)
            {
                ResultSet.NextInstruction = Instruction.NextSeqOnFail;
                return false;
            }
            ResultSet.NextInstruction = Instruction.NextSeqOnSuccess;
            return true;
        }

        /// <summary>
        /// GetResponseType
        /// Returns the ResponseType as specified by the argument string.
        /// </summary>
        /// <param name="Arg">[string] The string specifying the type ResponseType to return.</param>
        /// <returns>[InstructionResultSet.ResponseType] The response enumeration identified
        /// by the Arg string.</returns>
        private InstructionResultSet.ResponseType GetResponseType(string Arg)
        {
            switch (Arg)
            {
                case "none":
                    return InstructionResultSet.ResponseType.None;
                    break;
                case "standard":
                    return InstructionResultSet.ResponseType.Standard;
                    break;
                case "aggregate":
                    return InstructionResultSet.ResponseType.Aggregate;
                    break;
                default:
                    throw new OperationFailedException("Unknown response type argument '" + Arg + "'.");
            }
        }

        /// <summary>
        /// CompareFlags
        /// Performs a flag comparison using the specified flags between the two specified Flaggable
        /// objects.
        /// Note: This method is not yet implemented.
        /// </summary>
        /// <param name="FlagsToCheck">[IEnumerable<Flag>] The collection of potential flags that must be
        /// overcome for this comparison to succeed.</param>
        /// <param name="From">[Flaggable] The flaggable object that is attempting to overcome the flags.</param>
        /// <param name="To">[Flaggable] The flaggable object that contains the flags that must be overcome.
        /// Only those flags specifically listed in the FlagsToCheck collection are checked.</param>
        /// <param name="Result">[string] Any failure messages resulting from the flag check.  An empty
        /// string is considered a success.</param>
        /// <returns>[bool] True if the instruction executed successfully, otherwise false.</returns>
        private bool CompareFlags(IEnumerable<Flag> FlagsToCheck, Flaggable From, Flaggable To, out string Result)
        {
            Result = "";
            return false;
        }

        /// <summary>
        /// InstructionResultSet
        /// Contains a set of registers that record values between instruction calls and that contain
        /// the response object that will be returned at the conclusion of the instruction execution.
        /// </summary>
        private class InstructionResultSet
        {
            // The next instruction to execute when the current instruction completes.  This technique
            // supports looping and decision structions by avoiding a strict sequential execution
            // sequence.
            public int NextInstruction;

            // A counter value used to support looping by tracking the collection subscript in the current
            // iteration.
            public int LoopCounter;

            // The aggregate string built between instruction calls.
            public string AggregateString;

            // A standard response string that may be built to provide an alternate response for other
            // PlayerCharacters involved.
            public string StandardString;

            // The name of the argument as retrieved by the getargname instruction.
            public string ArgumentName;

            // A reference to a GameElement object as retrieved by the get instruction.
            public GameElement GameObj;

            // The response to return once instruction execution concludes.
            public Response Response;

            // The response settings indicating what type of response to return to users depending on
            // their relationship to the caller.
            public ResponseSettings Settings;

            public struct ResponseSettings
            {
                public ResponseType Caller;
                public ResponseType Target;
                public ResponseType Room;
                public ResponseType Other;
            }

            /// <summary>
            /// AddResponseMessages
            /// Adds the messages contained in the specified Response object to the messages list
            /// stored in this Response object.
            /// </summary>
            /// <param name="Response">[Response] The Response containing the Message objects to
            /// add to the instance collection.</param>
            public void AddResponseMessages(Response Response)
            {
                this.Response.AddResponseMessages(Response);
            }

            /// <summary>
            /// ApplyResponse
            /// Uses the current ResponseSettings to build a Message object for the specified user as
            /// appropriate and append it to the Messages collection.
            /// </summary>
            /// <param name="Caller">[Combatant] The combatant that initiated this action.</param>
            /// <param name="Recipient">[Combatant] The combatant for which the message is
            /// being constructed.</param>
            /// <param name="Syntax">[KeywordSyntax] The syntax of the specified instruction.</param>
            public void ApplyResponse(Combatant Caller, Combatant Recipient, KeywordSyntax Syntax)
            {
                Response.Message msg = null;
                // If the recipient is the caller.
                if (Recipient.Equals(Caller))
                {
                    msg = CreateMessage(Recipient, RecipientType.Caller, Syntax);
                    if (msg != null)
                        Response.Messages.Add(msg);
                }

                // If the recipient is the target of the action
                else if (Recipient.Equals(GameObj))
                {
                    msg = CreateMessage(Recipient, RecipientType.Target, Syntax);
                    if (msg != null)
                        Response.Messages.Add(msg);
                }

                // If the recipient is in the same room as the caller.
                else if (Recipient.Location.Equals(Caller.Location))
                {
                    msg = CreateMessage(Recipient, RecipientType.Room, Syntax);
                    if (msg != null)
                        Response.Messages.Add(msg);
                }

                // Otherwise
                else
                {
                    msg = CreateMessage(Recipient, RecipientType.Other, Syntax);
                    if (msg != null)
                        Response.Messages.Add(msg);
                }
            }

            /// <summary>
            /// CreateMessage
            /// Constructs a Message object based on the specified RecipientType.
            /// </summary>
            /// <param name="Recipient">[Combatant] The Combatant that will receive this
            /// message.</param>
            /// <param name="RecipientType">[RecipientType] The relationship of the recipient
            /// to the caller of this action.</param>
            /// <param name="Syntax">[KeywordSyntax] The syntax of the initial command string.</param>
            /// <returns>[Response.Message] The Message object created or null if the message
            /// text is an empty string.</returns>
            private Response.Message CreateMessage(Combatant Recipient, RecipientType RecipientType,
                KeywordSyntax Syntax)
            {
                Response.Message msg = new Response.Message();
                msg.CharacterID = Recipient.ID.ToString();
                msg.Type = "main";

                switch (RecipientType)
                {
                    case InstructionResultSet.RecipientType.Caller:
                        msg.Text = GetMessageText(Settings.Caller, Syntax);
                        break;
                    case InstructionResultSet.RecipientType.Target:
                        msg.Text = GetMessageText(Settings.Target, Syntax);
                        break;
                    case InstructionResultSet.RecipientType.Room:
                        msg.Text = GetMessageText(Settings.Room, Syntax);
                        break;
                    case InstructionResultSet.RecipientType.Other:
                        msg.Text = GetMessageText(Settings.Other, Syntax);
                        break;
                    default:
                        msg.Text = "";
                        break;
                }

                return (msg.Text == "" ? null : msg);
            }

            /// <summary>
            /// GetMessageText
            /// Returns the message text as specified by the ResponseType.
            /// Note: KeywordSyntax is not currently used by this method.  It is available to
            /// support general syntax messages.
            /// </summary>
            /// <param name="ResponseType">[ResponseType] The type of response to return.</param>
            /// <param name="Syntax">[KeywordSyntax] The syntax of the initial command string.</param>
            /// <returns>[string] The text of the message.</returns>
            private string GetMessageText(ResponseType ResponseType, KeywordSyntax Syntax)
            {
                switch (ResponseType)
                {
                    case InstructionResultSet.ResponseType.None:
                        return "";

                    case InstructionResultSet.ResponseType.Standard:
                        return StandardString;

                    case InstructionResultSet.ResponseType.Aggregate:
                        return AggregateString;

                    default:
                        return "";
                }
            }

            // The type of response to return for a given user.
            public enum ResponseType { None, Standard, Aggregate };

            // The relationship of a Combatant to the caller of the action.
            private enum RecipientType { Caller, Target, Room, Other };
        }

    }
}
