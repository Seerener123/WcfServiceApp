using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfServiceApp.Messaging.DataContracts
{
    [DataContract]
    public class MessageContract : IMessageContract
    {
        [DataMember]
        public string SenderName { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}