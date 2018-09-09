using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.BaseDbInterfaces
{
    public interface IBaseEntity
    {
        [Key]
        long Id { get; set; }
    }
}