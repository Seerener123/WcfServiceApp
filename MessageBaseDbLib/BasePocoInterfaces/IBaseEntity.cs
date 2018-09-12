using System;
using System.ComponentModel.DataAnnotations;

namespace MessageBaseDbLib.BasePocoInterfaces
{
    public interface IBaseEntity
    {
        [Key]
        long Id { get; set; }
    }
}