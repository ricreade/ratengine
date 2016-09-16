using Microsoft.VisualStudio.TestTools.UnitTesting;
using RatEngine.DataModel.World;
using RatEngine.DataSource;
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
            Realm r = new Realm(new RatDataModelAdapter());

        }

        [TestMethod()]
        public void RealmTest()
        {
            int testId = 2;
            string expectedName = "Pandora";
            string expectedDescription = "The world from Avatar!";
            Guid expectedGameId = new Guid("02008216-8A7E-4F0F-89DE-DA0068B7ADD5");
            RatDataModelAdapter adapter = new RatDataModelAdapter();
            adapter.Retrieve(RatDataModelType.Realm, new List<DataParameter>() { new DataParameter(RatDataModelAdapter.RealmFields.ID, testId) });

            Realm realm = new Realm(adapter);
            Assert.AreEqual(expectedName, realm.Name);
            Assert.AreEqual(expectedDescription, realm.Description);
            Assert.AreEqual(expectedGameId, realm.GameID);
        }
    }
}