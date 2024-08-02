using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T> Data { get; private set; } 

        public PaginationResponse(IEnumerable<T> items)
        {
            Data = items;
        }
    }
}
