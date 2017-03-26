using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    public abstract class GameDataObject
    {
        private IRatDataModelAdapter _adapter;

        public GameDataObject(IRatDataModelAdapter DataAdapter)
        {
            _adapter = DataAdapter;

        }
        
        public void Delete()
        {
            _adapter.Delete();
        }

        public void Load()
        {
            _adapter.Retrieve();
            IDataResultSet results = _adapter.ResultSet;
        }

        public void Save()
        {
            _adapter.Save();
        }
    }
}
