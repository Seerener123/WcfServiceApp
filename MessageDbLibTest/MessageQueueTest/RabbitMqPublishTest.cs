using System;
using MessageMqLib.MqProducerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageDbLibTest.MessageQueueTest
{
    [TestClass]
    public class RabbitMqPublishTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            RabbitMqProducerClass rabbitMqProdcuer = new RabbitMqProducerClass();
            rabbitMqProdcuer.ExecuteMessageQueueing("Hello world");
        }
    }
}
