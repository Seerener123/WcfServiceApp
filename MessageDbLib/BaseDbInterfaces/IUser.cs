using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.BaseDbInterfaces
{
    public interface IUser : IBaseEntity
    {
        string USERNAME { get; set; }

        string PASSWORD { get; set; }

        DateTime? DOB { get; set; }
    }
}