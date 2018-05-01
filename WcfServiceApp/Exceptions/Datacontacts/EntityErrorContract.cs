using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfServiceApp.Exceptions.Datacontacts
{
    [DataContract]
    public class EntityErrorContract : IErrorsContract
    {
        [DataMember]
        public string Message { get; set; }
    }
}