using System;

namespace MessageBaseDbLib.BasePocoInterfaces
{
    public interface IMessageTransaction : IBaseEntity
    {
        string EmailAddress { get; set; }

        long? MessageId { get; set; }

        bool? MessageReceived { get; set; }

        DateTime? MessageReceivedTime { get; set; }
    }
}
