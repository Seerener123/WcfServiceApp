using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace MessageDbLib.MessagingEntities
{
    [Table("MessageTable", Schema = "messagedbo")]
    [DataContract(Name = "MessageTable")]
    public class MessageTable : IMessage
    {
        [DataMember(Name = "ID")]
        public long ID { get; set; }

        [DataMember(Name = "MESSAGETEXT")]
        public string MESSAGETEXT { get; set; }

        [DataMember(Name = "SENDERID")]
        public long? SENDERID { get; set; }

        [DataMember(Name = "MESSAGECREATED")]
        public DateTime? MESSAGECREATED { get; set; }

        [DataMember(Name = "User")]
        [ForeignKey("SENDERID")]
        public virtual UserTable User { get; set; }

        //[DataMember]
        //public virtual UserTable User { get; set; }
    }
}
