using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    public enum RatDataModelType
    {
        Keyword,
        KeywordSyntax,
        Realm,
        Region,
        Room,
        SystemInstruction,
        Transition
    }

    public class RatDataModelAdapterFactory
    {
        public IRatDataModelAdapter GetAdapter()
        {
            return null;
        }


        private class RatDataModelAdapterImpl : IRatDataModelAdapter
        {
            public IDataResultSet ResultSet
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public int ReturnValue
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public void Delete()
            {
                throw new NotImplementedException();
            }

            public void Retrieve()
            {
                throw new NotImplementedException();
            }

            public void Save()
            {
                throw new NotImplementedException();
            }
        }
    }
}
