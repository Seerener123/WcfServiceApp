using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceApp.Messaging.DataContracts
{
    public interface IMessageContract
    {
        string SenderName { get; set; }
        string Message { get; set; }
    }
}
