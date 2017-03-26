using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RatEngine.Engine.Command
{
    public class CommandListener
    {
        private Keyword _keyword;
        private string _scriptname;

        public CommandListener(Keyword keyword, string scriptName)
        {
            _keyword = keyword;
            _scriptname = scriptName;
        }

        public bool IsPatternMatch(string commandString)
        {
            return _keyword.IsMatch(commandString);
        }

        public void ProcessCommand(GameCommand command)
        {
            if (IsPatternMatch(command.CommandString))
                _keyword.ExecuteCommand(command);
        }
    }
}
