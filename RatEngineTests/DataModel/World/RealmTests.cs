using Microsoft.VisualStudio.TestTools.UnitTesting;
using RatEngine.DataModel.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.World.Tests
{
    [TestClass()]
    public class RealmTests
    {
        [TestMethod()]
        public void DeleteTest()
        {
            Realm r = new Realm(new DataSource.RatDataModelAdapter());
            
        }
    }
}