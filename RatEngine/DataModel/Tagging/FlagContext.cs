using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Tagging
{
    public class FlagContext
    {
        private readonly FlagTemplate _controlledtemplate;
        private bool _isrequired;
        private int _multiplicity;
        private string _settings;

        public FlagContext(FlagTemplate ControlledTemplate)
        {
            _controlledtemplate = ControlledTemplate;
        }

        public bool IsRequired
        {
            get { return _isrequired; }
        }

        public int Multiplicity
        {
            get { return _multiplicity; }
        }

        public string Settings
        {
            get { return _settings; }
        }
    }
}
