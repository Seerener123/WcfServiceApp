using MessageBaseDbLib.BasePocoInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
         *      [DataMember(Name = "Id")] // same name assignment
         *      public long Id { get; set; }
         *      
         *      [DataMember(Name = "Id")] // same name assignment
         *      public string UserName { get; set; }
         *      
         * Beacause both share the same name, but are different properties, wcf get confused, and
         * throws an exception saying that one of the property is not accessible .i.e. not serialiseable.
         * */

        [DataMember(Name = "Id")]
        public long Id { get; set; }

        [Required]
        [StringLength(500)]
        [DataMember(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(500)]
        [DataMember(Name = "Password")]
        public string Password { get; set; }

        [StringLength(100)]
        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [DataMember(Name = "Surname")]
        public string Surname { get; set; }

        //[Required]
        [DataMember(Name = "Dob")]
        public DateTime? Dob { get; set; }

        [StringLength(6)]
        [DataMember(Name = "Gender")]
        public string Gender { get; set; }

        [DataMember(Name = "Messages")]
        public ICollection<MessageTable> Messages { get; set; }

        public UserTable()
        {
            Messages = new HashSet<MessageTable>();
        }
    }
}