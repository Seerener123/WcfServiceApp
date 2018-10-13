using MessageMqLib.MqInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace MessageMqLib.MqProducerClasses
{
    public class RabbitMqProdcuerClass : IMessageQueueProducer
    {
        public void ExecuteMessageQueueing<TMessage>(TMessage message)
        {
            ConnectionFactory connectionFactory = CreateConnectionFactory();
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                ExecuteMessageSending(connection, message);
            }
        }

        private ConnectionFactory CreateConnectionFactory()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = "";
            return connectionFactory;
        }

        private void ExecuteMessageSending<TMessage>(IConnection connection, TMessage message)
        {
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                // send message part here
            }
        }
    }
}
