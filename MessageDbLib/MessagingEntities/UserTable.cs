using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace MessageDbLib.MessagingEntities
{
    [Table("UserTable")]
    [DataContract(Name = "UserTable")]
    public class UserTable : IUser
    {
        [DataMember(Name = "ID")]
        public long ID { get; set; }

        [Required]
        [StringLength(500)]
        [DataMember(Name = "USERNAME")]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(500)]
        [DataMember(Name = "PASSWORD")]
        public string PASSWORD { get; set; }

        [Required]
        [DataMember(Name = "DOB")]
        public DateTime? DOB { get; set; }

        //[DataMember(Name = "Messages")]
        //public ICollection<MessageTable> Messages { get; set; }

        public UserTable()
        {
            //Messages = new HashSet<MessageTable>();
        }
    }
}