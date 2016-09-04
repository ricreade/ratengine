using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.World;

namespace RatEngine.DataSource
{
    /// <summary>
    /// Utility class to support data handling between the data source and the
    /// data models.
    /// </summary>
    public class RatDataModelAdapter
    {
        public static List<Realm> GetRealms()
        {
            List<Realm> realms = new List<Realm>();

            SqlDataConnection conn = new SqlDataConnection(Properties.Settings.Default.ConnString);
            IDataResultSet data = conn.SendReadRequest("", null);


            return realms;
        }

        public static void SaveRealm(Realm Item)
        {
            if (Item != null)
            {
                SqlDataConnection conn = new SqlDataConnection(Properties.Settings.Default.ConnString);
                
            }
        }
    }
}
