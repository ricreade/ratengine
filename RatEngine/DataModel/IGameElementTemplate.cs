using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel
{
    public interface IGameElementTemplate
    {
        bool IsConforming(ITemplatedBase Element);
    }
}
