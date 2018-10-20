using MessageMqLib.MqInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MessageMqLib.MqProducerClasses
{
    public class RabbitMqProducerClass : IMessageQueueProducer
    {
        public void ExecuteMessageQueueing<TMessage>(TMessage message)
        {
            try
            {
                ConnectionFactory connectionFactory = CreateConnectionFactory();
                using (IConnection connection = connectionFactory.CreateConnection())
                {
                    ExecuteMessageSending(connection, message);
                }
            }
            catch (Exception exception)
            {
                // logging
                throw new InvalidOperationException("RabbitMqProducerClass encountered when trying to queue a message. See inner-exception" +
                    "for more detail.", exception);
            }
        }

        private ConnectionFactory CreateConnectionFactory()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.UserName = "admin";
            connectionFactory.Password = "";
            connectionFactory.VirtualHost = "/";
            connectionFactory.Port = AmqpTcpEndpoint.UseDefaultPort;
            connectionFactory.HostName = "192.168.44.128";
            return connectionFactory;
        }

        private void ExecuteMessageSending<TMessage>(IConnection connection, TMessage message)
        {
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                // send message part here
                byte[] binaryData = ConvertToBinary(message);
                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: binaryData);
            }
        }

        private byte[] ConvertToBinary<TMessage>(TMessage item)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, item);
                return memoryStream.ToArray();
            }
            // https://stackoverflow.com/questions/1446547/how-to-convert-an-object-to-a-byte-array-in-c-sharp
        }
    }
}
