using System;
using System.Collections.Generic;

namespace WebTestTourkit.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductTypes = new HashSet<ProductType>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime EntryDate { get; set; }

        public virtual ICollection<ProductType> ProductTypes { get; set; }
    }
}
