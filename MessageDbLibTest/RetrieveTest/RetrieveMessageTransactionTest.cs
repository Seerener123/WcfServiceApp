using System;
using MessageBaseDbLib.Constants;
using MessageDbLib.DbRetrievals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageDbLibTest.RetrieveTest
{
    [TestClass]
    public class RetrieveMessageTransactionTest
    {
        [TestMethod]
        public void TestRetrieveMessageTransactionUsingSenderIdMssql()
        {
            RetrieveMessageTransactionClass retrieveMessageTransaction = new RetrieveMessageTransactionClass(DbContextConstant.MsSqlDbContext);
            var list = retrieveMessageTransaction.GetAllEntitiesMatchingCondtionsEagerLoadAssociatedEntitiesFunc(t => t.Message, t => t.Message.SenderId == 1);
            int value = 1;
        }
    }
}
