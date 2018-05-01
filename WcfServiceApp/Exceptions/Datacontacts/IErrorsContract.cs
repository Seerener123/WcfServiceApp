using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceApp.Exceptions.Datacontacts
{
    public interface IErrorsContract
    {
        string Message { get; set; }
    }
}
