using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.BaseDbInterfaces
{
    public interface IMessage : IBaseEntity
    {
        string MESSAGETEXT { get; set; }
        long? SENDERID { get; set; }
        DateTime? MESSAGECREATED { get; set; }
    }
}
