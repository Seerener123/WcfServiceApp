using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceApp.Messaging.DataContracts
{
    public interface IMessageContract
    {
        string UserName { get; set; }
        string Message { get; set; }
        IList<string> EmailAccounts { get; set; }
        DateTime MessageCreated { get; set; }
    }
}
