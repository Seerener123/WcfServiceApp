using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageMqLib.MqInterfaces
{
    public interface IMessageQueueConsumer<TMessage> where TMessage : class
    {
        List<TMessage> Messages { get; }
        TMessage ExecuteRetrievalOfMultipleMessages(); //<TMessage>() where TMessage : class;
        TMessage ExecuteRetrievalOfSingleMessage(); //<TMessage>() where TMessage : class;
    }
}
