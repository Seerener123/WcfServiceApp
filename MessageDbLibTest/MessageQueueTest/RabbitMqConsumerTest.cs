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
            RabbitMqConsumerClass<MessageTable> rabbitMqConsumer = new RabbitMqConsumerClass<MessageTable>(QueueTypeConstant.MongoDbPersistentQueue);
            //MessageTable message = rabbitMqConsumer.ExecuteRetrievalOfMultipleMessages();
            MessageTable message = rabbitMqConsumer.ExecuteRetrievalOfSingleMessage();
            MessageTable messageData = message as MessageTable;
            if (messageData != null)
            {
                Console.WriteLine("Message recieved");
            }
            Assert.IsInstanceOfType(messageData, typeof(MessageTable));
        }
    }
}
