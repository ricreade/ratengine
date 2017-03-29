using Microsoft.VisualStudio.TestTools.UnitTesting;
using RatEngine.DataModel.Tagging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Tagging.Tests
{
    [TestClass()]
    public class FlagTests
    {
        [TestMethod()]
        public void FlagEqualsTest()
        {
            Flag flag1 = new Flag(Guid.NewGuid());
            Flag flag2 = new Flag(Guid.NewGuid());

            Assert.AreNotEqual(flag1, flag2);
        }

        [TestMethod()]
        public void FlagNotEqualsTest()
        {
            Guid id = Guid.NewGuid();
            Flag flag1 = new Flag(id);
            Flag flag2 = new Flag(id);

            Assert.AreEqual(flag1, flag2);
        }

        [TestMethod()]
        public void FlagLessThanTest()
        {
            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();

            Flag flag1 = new Flag(id1);
            Flag flag2 = new Flag(id2);

            if (id1.CompareTo(id2) < 0)
            {
                Assert.IsTrue(flag1 < flag2);
                Assert.IsTrue(flag2 > flag1);
            }
            else
            {
                Assert.IsTrue(flag1 > flag2);
                Assert.IsTrue(flag2 < flag1);
            }
        }
    }
}