using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    public class ConnectionUtils
    {
        private static ConnectionUtils _instance;

        public ConnectionUtils()
        {

        }

        public string ConnectionString
        {
            get { return Properties.Settings.Default.ConnString; }
        }

        public static ConnectionUtils Instance()
        {
            if (_instance == null)
                _instance = new ConnectionUtils();
            return _instance;
        }
    }
}
