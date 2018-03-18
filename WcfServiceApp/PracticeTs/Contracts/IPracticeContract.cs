using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceApp.PracticeTs.Contracts
{
    public interface IPracticeContract
    {
        int Number { get; set; }
        string Message { get; set; }
    }
}
