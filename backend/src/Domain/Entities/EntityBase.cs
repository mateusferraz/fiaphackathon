using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreateDateAt { get; set; }
        public DateTime UpdateDateAt { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
