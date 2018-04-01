using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.BaseDbInterfaces
{
    public interface IMessageTransaction : IBaseEntity
    {
        string EMAILADDRESS { get; set; }

        long? MESSAGEID { get; set; }

        bool? MESSAGERECEIVED { get; set; }

        DateTime? MESSAGERECEIVEDTIME { get; set; }
    }
}
