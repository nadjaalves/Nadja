using Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Registers

{
    public class Product
    {
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public long? CategoryId { get; set; }
        public long? SupplierId { get; set; }
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}