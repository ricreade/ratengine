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
    public class EffectContext
    {
        private readonly EffectTemplate _controlledtemplate;
        private bool _isrequired;
        private int _multiplicity;
        private string _settings;

        public EffectContext(EffectTemplate ControlledTemplate)
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
