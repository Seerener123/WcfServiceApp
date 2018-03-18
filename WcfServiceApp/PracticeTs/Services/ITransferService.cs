using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfServiceApp.PracticeTs.Contracts;

namespace WcfServiceApp.PracticeTs.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITransferService" in both code and config file together.
    [ServiceContract]
    public interface ITransferService
    {
        [OperationContract]
        void Send(PracticeContract practiceContract);
        [OperationContract]
        int Addition(PracticeContract practiceContract);
    }
}
