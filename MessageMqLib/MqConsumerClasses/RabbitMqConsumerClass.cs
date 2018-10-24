using MessageMqLib.MqInterfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageMqLib.MqConsumerClasses
{
    public class RabbitMqConsumerClass : IMessageQueueConsumer
    {
        private object _message;
        private readonly string _queueType;
        private AutoResetEvent _autoResetEvent;

        public RabbitMqConsumerClass(string queueType)
        {
            _queueType = queueType;
            _autoResetEvent = new AutoResetEvent(false);
        }

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
            connectionFactory.UserName = ConfigurationManager.AppSettings["RabbitMqConnectionUserName"];
            connectionFactory.Password = ConfigurationManager.AppSettings["RabbitMqConnectionPassword"];
            connectionFactory.VirtualHost = "/";
            connectionFactory.Port = AmqpTcpEndpoint.UseDefaultPort;
            connectionFactory.HostName = ConfigurationManager.AppSettings["RabbitMqConnectionHostName"];
            return connectionFactory;
        }

        private void ExecuteMessageReceivinging<TMessage>(IConnection connection)
        {
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueType, durable: false, exclusive: false, autoDelete: false, arguments: null);
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += EventBasicConsumer_Received<TMessage>;
                channel.BasicConsume(queue: _queueType, autoAck: true, consumer: consumer);
                _autoResetEvent.WaitOne(5000);
            }
        }

        private void EventBasicConsumer_Received<TMessage>(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                 byte[] bodyDataByteArray = args.Body;
                _message = ByteArrayToObject<TMessage>(bodyDataByteArray);
                _autoResetEvent.Set();
            }
            catch (Exception exception)
            {
                //
            }
        }

        private object ByteArrayToObject<TMessage>(byte[] arrayBytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                DataContractSerializer binaryFormatter = new DataContractSerializer(typeof(TMessage));
                memoryStream.Write(arrayBytes, 0, arrayBytes.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return binaryFormatter.ReadObject(memoryStream);
            }
        }
    }
}
