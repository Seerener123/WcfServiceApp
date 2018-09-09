using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.BaseDbInterfaces
{
    public interface IUser : IBaseEntity
    {
        string UserName { get; set; }

        string Password { get; set; }

        string FirstName { get; set; }

        string Surname { get; set; }

        DateTime? Dob { get; set; }

        string Gender { get; set; }
    }
}