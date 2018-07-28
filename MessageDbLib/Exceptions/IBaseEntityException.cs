using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.Exceptions
{
    public interface IBaseEntityException<TEntity> where TEntity : IBaseEntity
    {
        bool IsEntityNull { get; }
        TEntity Entity { get; }
    }
}