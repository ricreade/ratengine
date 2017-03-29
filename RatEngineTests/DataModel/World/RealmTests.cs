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
        public void RealmInsertAndDeleteTest()
        {
            const string newName = "My Realm";
            const string newDescription = "A new test realm.";
            int realmId = 0;
            Guid gameId = Guid.Empty;

            RatDataModelAdapter adapter = new RatDataModelAdapter();
            Realm r = new Realm();
            r.Name = newName;
            r.Description = newDescription;
            //r.Save();

            //realmId = r.ID;
            //gameId = r.GameID;

            Assert.IsTrue(realmId > 0);
            Assert.IsTrue(gameId != Guid.Empty);

            //r.Delete();

            Assert.IsTrue(adapter.ReturnValue > 0);

            adapter.Retrieve(RatDataModelType.Realm, new List<DataParameter>() { new DataParameter(RatDataModelAdapter.RealmParameters.ID, realmId) });
            r = new Realm();
        }

        [TestMethod()]
        public void RealmTest()
        {
            string expectedName = "Pandora";
            string expectedDescription = "The world from Avatar!";
            Guid expectedGameId = new Guid("02008216-8A7E-4F0F-89DE-DA0068B7ADD5");

            Realm realm = GetTestRealm();
            Assert.AreEqual(expectedName, realm.Name);
            Assert.AreEqual(expectedDescription, realm.Description);
            //Assert.AreEqual(expectedGameId, realm.GameID);
        }

        [TestMethod()]
        public void RealmUpdateTest()
        {
            int realmId = 0;
            Guid gameId = Guid.Empty;
            string oldName = null;
            string newName = "New World";

            Realm realm = GetTestRealm();
            oldName = realm.Name;
            //realmId = realm.ID;
            //gameId = realm.GameID;

            realm.Name = newName;
            //realm.Save();

            realm = GetTestRealm();
            Assert.AreEqual(newName, realm.Name);
            //Assert.AreEqual(realmId, realm.ID);
            //Assert.AreEqual(gameId, realm.GameID);
            realm.Name = oldName;
            //realm.Save();

            realm = GetTestRealm();
            Assert.AreEqual(oldName, realm.Name);
        }

        private Realm GetTestRealm()
        {
            int testId = 2;

            RatDataModelAdapter adapter = new RatDataModelAdapter();
            adapter.Retrieve(RatDataModelType.Realm, new List<DataParameter>() { new DataParameter(RatDataModelAdapter.RealmParameters.ID, testId) });

            return new Realm();
        }
    }
}