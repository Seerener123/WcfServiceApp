using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageMqLib.MqInterfaces
{
    public interface IMessageQueueProducer
    {
        void ExecuteMessageQueueing<TMessage>(TMessage message);
    }
}
