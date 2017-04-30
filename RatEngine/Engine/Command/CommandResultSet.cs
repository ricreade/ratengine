using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel;
using RatEngine.Engine.Command;

namespace RatEngine.Engine.Command
{
    public class CommandResultSet
    {
        private readonly int REGISTER_SIZE = 10;
        private int _nextinstruction;
        private int _loopcounter;
        private string[] _valueregister;
        private GameElement[] _elementregister;
        private ConcurrentBag<Response> _responses;
        //private Response _response;

        public CommandResultSet()
        {
            InitializeRegisters();
            _responses = new ConcurrentBag<Response>();
        }

        public CommandResultSet(int registerSize)
        {
            REGISTER_SIZE = registerSize;
            InitializeRegisters();
            _responses = new ConcurrentBag<Response>();
        }

        public virtual ConcurrentBag<Response> Responses
        {
            get { return _responses; }
        }

        public virtual GameElement[] ElementRegister
        {
            get { return _elementregister; }
        }

        private void InitializeRegisters()
        {
            _valueregister = new string[REGISTER_SIZE];
            _elementregister = new GameElement[REGISTER_SIZE];
        }

        public virtual int LoopCounter
        {
            get { return _loopcounter; }
            set
            {
                if (value >= 0)
                    _loopcounter = value;
            }
        }

        public virtual int NextInstruction
        {
            get { return _nextinstruction; }
            set
            {
                if (value >= 0)
                    _nextinstruction = value;
            }
        }

        /// <summary>
        /// Creates a single <see cref="Response"/> encapsulating all responses contained
        /// in this result set.
        /// </summary>
        /// <returns></returns>
        public Response ReconcileResponses()
        {
            return null;
        }
        
        public virtual string[] ValueRegister
        {
            get { return _valueregister; }
        }
        
    }
}
