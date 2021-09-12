
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw.Dto.Dto
{
    public class FiledOrderAttribute : Attribute
    {
        public int Order { get; set; }
    }
}