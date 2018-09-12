using System;
using System.Runtime.Serialization;

namespace MessageDbLib.MessagingEntities
{
    [DataContract]
    public class AdvancedUser : UserTable
    {
        [DataMember(Name = "ADVANCESTARTDATETIME")]
        public DateTime? ADVANCESTARTDATETIME { get; set; }

        [DataMember(Name = "ADVANCEENDDATETIME")]
        public DateTime? ADVANCEENDDATETIME { get; set; }

        /* The property below: ISADVANCEDUSER is used as a 
         * discriminator column in tabler-per-hierachy.
         * The discriminator column just tell us what kind
         * of object was persisted to the database.
         * 
         * The Is-advanced-user property is commented out
         * because decriminator columns are should been 
         * hidden, and should be dealt by entity itself
         * */

        //[DataMember(Name = "ISADVANCEDUSER")]
        //public bool ISADVANCEDUSER { get; set; }

        public AdvancedUser()
        {
            /* This will automatically call the base
             * constructor. 
             */
        }
    }
}
