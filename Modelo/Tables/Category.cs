using Model.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Tables
{
    public class Category
    {
        public long? CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}