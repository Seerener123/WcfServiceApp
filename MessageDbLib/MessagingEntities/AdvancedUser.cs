using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.MessagingEntities
{
    [DataContract]
    public class AdvancedUser : UserTable
    {
        [DataMember(Name = "ADVANCESTARTDATETIME")]
        public DateTime? ADVANCESTARTDATETIME { get; set; }

        [DataMember(Name = "ADVANCEENDDATETIME")]
        public DateTime? ADVANCEENDDATETIME { get; set; }

        //[DataMember(Name = "ISADVANCEDUSER")]
        //public bool? ISADVANCEDUSER { get; set; }

        public AdvancedUser()
        {
            /* This will automatically call the base
             * constructor. 
             */
        }
    }
}
