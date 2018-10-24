using System;
using MessageDbLib.MessagingEntities;
using MessageMqLib.MqConsumerClasses;
using MessageMqLib.QueueConstants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageDbLibTest.MessageQueueTest
{
    [TestClass]
    public class RabbitMqConsumerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            RabbitMqConsumerClass rabbitMqConsumer = new RabbitMqConsumerClass(QueueTypeConstant.MongoDbPersisentQueue);
            MessageTable message = rabbitMqConsumer.ExecuteMessageRetrieving<MessageTable>();
            MessageTable messageData = message as MessageTable;
            if (messageData != null)
            {
                Console.WriteLine("Message recieved");
            }
            Assert.IsInstanceOfType(messageData, typeof(MessageTable));
        }
    }
}
