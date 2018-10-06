using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceApp.Messaging.DataContracts.MessageContracts
{
    public interface IMessageTransactionInfoContract
    {
        string SenderName { get; set; }
        string ReceiverName { get; set; }
        string MessageContent { get; set; }
        DateTime MessageCreated { get; set; }
        bool MessageReceived { get; set; }
    }
}
