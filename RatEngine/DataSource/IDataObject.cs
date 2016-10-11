using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    public interface IDataObject
    {
        RatDataModelAdapter DataAdapter { get; set; }

        bool Delete();

        bool Delete(RatDataModelAdapter Adapter);

        void LoadFromAdapter(RatDataModelAdapter Adapter);

        bool Save();

        bool Save(RatDataModelAdapter Adapter);
    }
}
