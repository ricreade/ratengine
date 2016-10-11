using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel
{
    public interface ITemplated<T> where T : IGameElementTemplate
    {
        T Template { get; }
    }
}
