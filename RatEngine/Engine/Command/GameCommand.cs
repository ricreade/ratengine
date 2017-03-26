using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Mob;

namespace RatEngine.Engine.Command
{
    public class GameCommand
    {
        private string _commandstring;
        private Creature _source;
        private Response _response;

        public GameCommand(Creature source, string commandString)
        {
            _source = source;
            _commandstring = CommandString;
            _response = new Response();
        }

        public string CommandString
        {
            get { return _commandstring; }
            set { }
        }

        public Response CommandResponse
        {
            get { return _response; }
            set { }
        }

        public Creature Source
        {
            get { return _source; }
            set { }
        }
    }
}
