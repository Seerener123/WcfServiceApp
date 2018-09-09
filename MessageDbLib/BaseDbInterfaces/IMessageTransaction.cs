using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.BaseDbInterfaces
{
    public interface IMessageTransaction : IBaseEntity
    {
        string EmailAddress { get; set; }

        long? MessageId { get; set; }

        bool? MessageReceived { get; set; }

        DateTime? MessageReceivedTime { get; set; }
    }
}
