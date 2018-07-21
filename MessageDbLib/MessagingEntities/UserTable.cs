using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace MessageDbLib.MessagingEntities
{
    /* Datacontract attributes only work with classes. Using these attributes
     * wcf can serialise the class, and send the data over the internet.
     * 
     * The Datacontract attribute to used to map the class that is going to be
     * serialised, were as the DataMember attribute is used to map the property
     * that are also going to be serialised.
     * */

    //[Table("UserTable")]
    [DataContract(Name = "UserTable")]
    public class UserTable : IUser
    {
        /* Please note that the name assignment to each DataMember attribute must be unique,
         * so something like:
         *      [DataMember(Name = "ID")] // same name assignment
         *      public long ID { get; set; }
         *      
         *      [DataMember(Name = "ID")] // same name assignment
         *      public string USERNAME { get; set; }
         *      
         * Beacause both share the same name, but are different properties, wcf get confused, and
         * throws an exception saying that one of the property is not accessible .i.e. not serialiseable.
         * */

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

        [StringLength(100)]
        [DataMember(Name = "FIRSTNAME")]
        public string FIRSTNAME { get; set; }

        [StringLength(100)]
        [DataMember(Name = "SURNAME")]
        public string SURNAME { get; set; }

        //[Required]
        [DataMember(Name = "DOB")]
        public DateTime? DOB { get; set; }

        [StringLength(6)]
        [DataMember(Name = "GENDER")]
        public string GENDER { get; set; }

        [DataMember(Name = "Messages")]
        public ICollection<MessageTable> Messages { get; set; }

        public UserTable()
        {
            Messages = new HashSet<MessageTable>();
        }
    }
}