using MessageMqLib.MqInterfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MessageMqLib.MqConsumerClasses
{
    public class RabbitMqConsumerClass : IMessageQueueConsumer
    {
        private object _message;

        public TMessage ExecuteMessageRetrieving<TMessage>() where TMessage : class
        {
            try
            {
                ConnectionFactory connectionFactory = CreateConnectionFactory();
                using (IConnection connection = connectionFactory.CreateConnection())
                {
                    ExecuteMessageReceivinging<TMessage>(connection);
                }
                return _message as TMessage;
            }
            catch (Exception exception)
            {
                //
            }
            return null;
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

        private void ExecuteMessageReceivinging<TMessage>(IConnection connection)
        {
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += EventBasicConsumer_Received;
                channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
            }
        }

        private void EventBasicConsumer_Received(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                 byte[] bodyDataByteArray = args.Body;
                _message = ByteArrayToObject(bodyDataByteArray);
            }
            catch (Exception exception)
            {
                //
            }
        }

        private object ByteArrayToObject(byte[] arrayBytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                memoryStream.Write(arrayBytes, 0, arrayBytes.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}
