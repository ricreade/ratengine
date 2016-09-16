using Microsoft.VisualStudio.TestTools.UnitTesting;
using RatEngine.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource.Tests
{
    [TestClass()]
    public class SqlDataConnectionTests
    {
        [TestMethod()]
        public void SqlDataConnectionTest()
        {
            SqlDataConnection conn = new SqlDataConnection(ConnectionUtils.Instance().ConnectionString);
            conn.OpenConnection();
            conn.CloseConnection();
            conn.OpenConnection();
            conn.CloseConnection();
        }
    }
}