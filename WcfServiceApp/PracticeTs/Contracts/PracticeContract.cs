using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfServiceApp.PracticeTs.Contracts
{
    [DataContract]
    public class PracticeContract : IPracticeContract
    {
        public int Number { get; set; }
        public string Message { get; set; }
    }
}