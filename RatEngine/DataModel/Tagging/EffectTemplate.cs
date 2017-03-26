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
    public class EffectTemplate
    {
        private List<FlagContext> _flagdefs;

        [DataMember]
        public List<FlagContext> FlagDefinitions
        {
            get { return _flagdefs; }
        }

        public bool IsConforming(Effect Element)
        {
            throw new NotImplementedException();
        }
    }
}
