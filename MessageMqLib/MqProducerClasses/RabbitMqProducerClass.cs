using MessageMqLib.MqInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
using System.Runtime.Serialization;

namespace MessageMqLib.MqProducerClasses
{
    public class RabbitMqProducerClass : IMessageQueueProducer
    {
        private readonly string _queueName;
        private readonly string _routeKey;

        public RabbitMqProducerClass(string queueName, string routeKey)
        {
            _queueName = queueName;
            _routeKey = routeKey;
        }

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
                throw new InvalidOperationException("RabbitMqProducerClass encountered an error when trying to queue a message. See inner-exception" +
                    "for more detail.", exception);
            }
        }

        private ConnectionFactory CreateConnectionFactory()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.UserName = ConfigurationManager.AppSettings["RabbitMqConnectionUserName"];
            connectionFactory.Password = ConfigurationManager.AppSettings["RabbitMqConnectionPassword"];
            connectionFactory.VirtualHost = "/";
            connectionFactory.Port = AmqpTcpEndpoint.UseDefaultPort;
            connectionFactory.HostName = ConfigurationManager.AppSettings["RabbitMqConnectionHostName"];
            return connectionFactory;
        }

        private void ExecuteMessageSending<TMessage>(IConnection connection, TMessage message)
        {
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(_queueName, false, false, false, null);

                byte[] binaryData = ConvertToBinary(message);
                channel.BasicPublish("", _routeKey, null, binaryData);
            }
        }

        private byte[] ConvertToBinary<TMessage>(TMessage item)
        {
            DataContractSerializer dataContractSerialiser = new DataContractSerializer(typeof(TMessage));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                dataContractSerialiser.WriteObject(memoryStream, item);
                return memoryStream.ToArray();
            }
            // https://stackoverflow.com/questions/1446547/how-to-convert-an-object-to-a-byte-array-in-c-sharp
        }
    }
}
