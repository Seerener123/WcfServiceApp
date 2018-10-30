using System;
using MessageDbLib.MessagingEntities;
using MessageMqLib.MqProducerClasses;
using MessageMqLib.QueueConstants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageDbLibTest.MessageQueueTest
{
    [TestClass]
    public class RabbitMqPublishTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            MessageTable messageTable = new MessageTable();
            messageTable.MessageCreated = DateTime.Now;
            messageTable.SenderId = 1;
            messageTable.MessageText = "Testing RabbitMq by sending an enitity over the queue.";
            RabbitMqProducerClass rabbitMqProdcuer = new RabbitMqProducerClass(QueueTypeConstant.MongoDbPersistentQueue, QueueTypeConstant.MongoDbPersistentQueue);
            rabbitMqProdcuer.ExecuteMessageQueueing(messageTable);
        }
    }
}
