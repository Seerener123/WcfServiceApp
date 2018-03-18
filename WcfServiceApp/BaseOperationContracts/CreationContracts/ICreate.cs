using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceApp.BaseOperationContracts.CreationContracts
{
    [ServiceContract]
    public interface ICreate
    {
        [OperationContract]
        int Create(int number);
    }
}
