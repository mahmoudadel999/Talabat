using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Domain.Common
{
    public abstract class BaseEntity<TKey>
    {
        public required TKey Id { get; set; }
    }
}
