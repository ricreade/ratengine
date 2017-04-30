using Microsoft.VisualStudio.TestTools.UnitTesting;
using RatEngine.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Tests
{
    [TestClass()]
    public class GameMessageTests
    {
        private readonly string messageNoArgs = @"You don't see anything here.";
        private readonly string messageThreeArgs = @"Hello {title} {name}! Welcome to the Hollowbrook apartments. You have visited the apartments {number} times!";

        [TestMethod()]
        public void GameMessage_NoArgs_Test()
        {
            string expected = @"You don't see anything here.";
            GameMessage msg = new GameMessage(messageNoArgs);
            Assert.AreEqual(expected, msg.ToString());
        }

        [TestMethod()]
        public void GameMessage_ThreeArgs_Test()
        {
            string expected = @"Hello Mrs. Davenport! Welcome to the Hollowbrook apartments. You have visited the apartments 3 times!";
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("title", "Mrs.");
            args.Add("name", "Davenport");
            args.Add("number", "3");

            GameMessage msg = new GameMessage(messageThreeArgs);
            msg.SetArguments(args);
            Assert.AreEqual(expected, msg.ToString());
        }

        [TestMethod()]
        public void GameMessage_ExtraArg_Test()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("title", "Mrs.");
            args.Add("name", "Davenport");
            args.Add("number", "3");
            args.Add("extra", "one");

            GameMessage msg = new GameMessage(messageThreeArgs);
            try
            {
                msg.SetArguments(args);
                string result = msg.ToString();
                Assert.Fail("Expected exception did not occur.");
            }
            catch (ArgumentException ex)
            {
                // Success.
            }
            catch (Exception ex)
            {
                Assert.Fail("Wrong exception type: " + ex.GetType());
            }
        }

        [TestMethod()]
        public void GameMessage_MissingArg_Test()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("title", "Mrs.");
            args.Add("name", "Davenport");
            args.Add("extra", "one");

            GameMessage msg = new GameMessage(messageThreeArgs);
            try
            {
                msg.SetArguments(args);
                string result = msg.ToString();
                Assert.Fail("Expected exception did not occur.");
            }
            catch (ArgumentException ex)
            {
                // Success.
            }
            catch (Exception ex)
            {
                Assert.Fail("Wrong exception type: " + ex.GetType());
            }
        }

        [TestMethod()]
        public void GameMessage_CanBuildMessageSuccess_Test()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("title", "Mrs.");
            args.Add("name", "Davenport");
            args.Add("number", "3");

            GameMessage msg = new GameMessage(messageThreeArgs);
            msg.SetArguments(args);
            Assert.IsTrue(msg.CanBuildMessage());
        }

        [TestMethod()]
        public void GameMessage_CanBuildMessageFail_Test()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("title", "Mrs.");
            args.Add("name", "Davenport");
            args.Add("numbe", "3");

            GameMessage msg = new GameMessage(messageThreeArgs);
            msg.SetArguments(args);
            Assert.IsFalse(msg.CanBuildMessage());
        }
    }
}