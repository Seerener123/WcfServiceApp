using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MessageDbLib.MessagingEntities
{
    [Table("MessageTable", Schema = "messagedbo")]
    public class MessageTable : IMessage
    {
        public long ID { get; set; }

        public string MESSAGETEXT { get; set; }

        public long? SENDERID { get; set; }

        public DateTime? MESSAGECREATED { get; set; }

        public virtual IUser User { get; set; }

        //public virtual UserTable User { get; set; }
    }
}
