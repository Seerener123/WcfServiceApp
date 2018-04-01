using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MessageDbLib.MessagingEntities
{
    [Table("MessageTransactionTable", Schema = "messagedbo")]
    public class MessageTransactionTable : IBaseEntity
    {
        public long ID { get; set; }

        [StringLength(500)]
        public string EMAILADDRESS { get; set; }

        public long? MESSAGEID { get; set; }

        public bool? MESSAGERECEIVED { get; set; }

        public DateTime? MESSAGERECEIVEDTIME { get; set; }

        public virtual IMessage Message { get; set; }

        //public virtual MessageTable Message { get; set; }
    }
}
