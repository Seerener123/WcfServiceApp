using MessageBaseDbLib.BasePocoInterfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MessageDbLib.MessagingEntities
{
    //[Table("MessageTransactionTable", Schema = "messagedbo")]
    [DataContract(Name = "MessageTransactionTable")]
    public class MessageTransactionTable : IMessageTransaction
    {
        [DataMember(Name = "Id")]
        public long Id { get; set; }

        [StringLength(500)]
        [DataMember(Name = "EmailAddress")]
        public string EmailAddress { get; set; }

        [DataMember(Name = "MessageId")]
        public long? MessageId { get; set; }

        [DataMember(Name = "MessageReceived")]
        public bool? MessageReceived { get; set; }

        [DataMember(Name = "MessageReceivedTime")]
        public DateTime? MessageReceivedTime { get; set; }

        //[DataMember(Name = "Message")]
        //public virtual IMessage Message { get; set; }

        [DataMember(Name = "Message")]
        public virtual MessageTable Message { get; set; }
    }
}
