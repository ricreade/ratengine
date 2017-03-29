using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Tagging
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class FlagContext
    {
        private readonly FlagTemplate _controlledtemplate;
        private bool _isrequired;
        private int _multiplicity;
        private string _settings;

        public FlagContext(FlagTemplate template)
        {
            _controlledtemplate = template;
        }

        public bool IsRequired
        {
            get { return _isrequired; }
            set { }
        }

        public int Multiplicity
        {
            get { return _multiplicity; }
            set { }
        }

        public string Settings
        {
            get { return _settings; }
            set { }
        }
    }
}
