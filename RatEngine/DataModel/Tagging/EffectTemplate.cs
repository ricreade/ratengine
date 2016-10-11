using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Tagging
{
    public class EffectTemplate
    {
        private List<FlagContext> _flags;

        public IReadOnlyList<FlagContext> Flags
        {
            get { return _flags.AsReadOnly(); }
        }

        public bool IsConforming(Effect Element)
        {
            throw new NotImplementedException();
        }
    }
}
