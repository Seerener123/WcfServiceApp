using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace MessageDbLib.MessagingEntities
{
    //[Table("MessageTable", Schema = "messagedbo")]
    [DataContract(Name = "MessageTable")]
    public class MessageTable : IMessage
    {
        [DataMember(Name = "Id")]
        public long Id { get; set; }

        [DataMember(Name = "MessageText")]
        public string MessageText { get; set; }

        [DataMember(Name = "SenderId")]
        public long? SenderId { get; set; }

        [DataMember(Name = "MessageCreated")]
        public DateTime? MessageCreated { get; set; }

        //[ForeignKey("SenderId")]
        [DataMember(Name = "User")]
        public virtual UserTable User { get; set; }

        [DataMember(Name = "MessageTransactions")]
        public ICollection<MessageTransactionTable> MessageTransactions { get; set; }

        public MessageTable()
        {
            MessageTransactions = new HashSet<MessageTransactionTable>();
        }
    }
}
