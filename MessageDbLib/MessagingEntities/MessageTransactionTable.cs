using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace MessageDbLib.MessagingEntities
{
    //[Table("MessageTransactionTable", Schema = "messagedbo")]
    [DataContract(Name = "MessageTransactionTable")]
    public class MessageTransactionTable : IBaseEntity
    {
        [DataMember(Name = "ID")]
        public long ID { get; set; }

        [StringLength(500)]
        [DataMember(Name = "EMAILADDRESS")]
        public string EMAILADDRESS { get; set; }

        [DataMember(Name = "MESSAGEID")]
        public long? MESSAGEID { get; set; }

        [DataMember(Name = "MESSAGERECEIVED")]
        public bool? MESSAGERECEIVED { get; set; }

        [DataMember(Name = "MESSAGERECEIVEDTIME")]
        public DateTime? MESSAGERECEIVEDTIME { get; set; }

        //[DataMember(Name = "Message")]
        //public virtual IMessage Message { get; set; }

        [DataMember(Name = "EMAILADDRESS")]
        public virtual MessageTable Message { get; set; }
    }
}
