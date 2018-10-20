using System;
using MessageMqLib.MqConsumerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageDbLibTest.MessageQueueTest
{
    [TestClass]
    public class RabbitMqConsumerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            RabbitMqConsumerClass rabbitMqConsumer = new RabbitMqConsumerClass();
            object message = rabbitMqConsumer.ExecuteMessageRetrieving<string>();
            string messageString = message as string;
            if (messageString != null)
            {

            }
        }
    }
}
