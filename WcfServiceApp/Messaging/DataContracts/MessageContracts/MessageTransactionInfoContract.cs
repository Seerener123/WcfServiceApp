using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfServiceApp.Messaging.DataContracts.MessageContracts
{
    [DataContract]
    public class MessageTransactionInfoContract : IMessageTransactionInfoContract
    {
        [DataMember]
        public string SenderName { get; set; }
        [DataMember]
        public string ReceiverName { get; set; }
        [DataMember]
        public DateTime MessageCreated { get; set; }
        [DataMember]
        public string MessageContent { get; set; }
        [DataMember]
        public bool MessageReceived { get; set; }
    }
}