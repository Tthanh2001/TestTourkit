using System;
using System.Collections.Generic;

namespace WebTestTourkit.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime EntryDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductType> ProductTypes { get; set; }

    }
}
