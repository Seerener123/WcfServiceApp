using MessageBaseDbLib.BasePocoInterfaces;

namespace MessageDbLib.Exceptions
{
    public interface IBaseEntityException<TEntity> where TEntity : IBaseEntity
    {
        bool IsEntityNull { get; }
        TEntity Entity { get; }
    }
}