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
    public class RabbitMqConsumerClass<TMessage> : IMessageQueueConsumer<TMessage> where TMessage : class
    {
        private object _object = new object();
        private object _message;
        private readonly string _queueName;
        private AutoResetEvent _autoResetEvent;

        private List<TMessage> _messages;
        public List<TMessage> Messages
        {
            get
            {
                lock (_object)
                {
                    return _messages;
                }
            }
        }

        public RabbitMqConsumerClass(string queueType)
        {
            _queueName = queueType;
            _messages = new List<TMessage>();
            _autoResetEvent = new AutoResetEvent(false);
        }

        public TMessage ExecuteRetrievalOfMultipleMessages()
        {
            try
            {
                ConnectionFactory connectionFactory = CreateConnectionFactory();
                using (IConnection connection = connectionFactory.CreateConnection())
                {
                    ExecuteMultipleMessagesRetrieval(connection);
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

        private void ExecuteMultipleMessagesRetrieval(IConnection connection)
        {
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(_queueName, false, false, false, null);
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += EventBasicConsumer_Received;
                channel.BasicConsume(_queueName, false, consumer);
                _autoResetEvent.WaitOne(5000);
            }
        }

        private void EventBasicConsumer_Received(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                byte[] bodyDataByteArray = args.Body;
                if (ByteArrayToObject(bodyDataByteArray) is TMessage message)
                {
                    lock (_object)
                    {
                        _messages.Add(message);
                    }
                }

                if (sender is EventingBasicConsumer senderObject)
                    senderObject.Model.BasicAck(args.DeliveryTag, false);

                _autoResetEvent.Set();
            }
            catch (Exception exception)
            {
                //
            }
        }

        public TMessage ExecuteRetrievalOfSingleMessage()
        {
            try
            {
                ConnectionFactory connectionFactory = CreateConnectionFactory();
                using (IConnection connection = connectionFactory.CreateConnection())
                {
                    using (IModel channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(_queueName, false, false, false, null);
                        BasicGetResult result = channel.BasicGet(_queueName, false);
                        if (result != null)
                        {
                            byte[] bodyDataByteArray = result.Body;
                            _message = ByteArrayToObject(bodyDataByteArray);
                            channel.BasicAck(result.DeliveryTag, false);
                        }
                    }
                }
                return _message as TMessage;
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        private object ByteArrayToObject(byte[] arrayBytes)
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
