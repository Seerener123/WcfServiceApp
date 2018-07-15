using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageDbLib.BaseDbInterfaces;
using MessageDbLib.MessagingEntities;
using MessageDbLib.Constants;
using MessageDbLib.DbPersistances;

namespace MessageDbLibTest.PersistTest
{
    /// <summary>
    /// Summary description for CreateMessageTest
    /// </summary>
    [TestClass]
    public class CreateMessageTest
    {
        public CreateMessageTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        //[TestMethod]
        public void CreateMessageMsSql()
        {
            Random randomNumber = new Random();
            string messagetext = "Unit test message. Hello for the " + randomNumber.Next(1000000) + " time.";

            MessageTable message = new MessageTable()
            {
                MESSAGETEXT = messagetext,
                MESSAGECREATED = DateTime.Now,
                SENDERID = 1
            };

            MessagePersistant createMessage = new MessagePersistant(null, MessageDbContextConstant.MsSqlMessageDbContext);
            createMessage.AddToPending(message);
            createMessage.SaveChange();
        }

        [TestMethod]
        public void CreateMessageMYSql()
        {
            Random randomNumber = new Random();
            string messagetext = "Unit test message. Hello for the " + randomNumber.Next(1000000) + " time.";

            MessageTable message = new MessageTable()
            {
                MESSAGETEXT = messagetext,
                MESSAGECREATED = DateTime.Now,
                SENDERID = 1
            };

            MessagePersistant createMessage = new MessagePersistant(null, MessageDbContextConstant.MySqlMessageDbContext);
            createMessage.AddToPending(message);
            createMessage.SaveChange();
        }
    }
}
