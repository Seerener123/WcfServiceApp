using System;

namespace MessageBaseDbLib.BasePocoInterfaces
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