using Model.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Registers
{
        public class Supplier
        {
            public long? SupplierId { get; set; }
            public string Name { get; set; }
        [NotMapped]
            public virtual ICollection<Product> Products { get; set; }
        }
    }

