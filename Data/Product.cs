using System.Collections.Generic;

namespace aspnetcore_event_driven.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int RemainingStock { get; set; }
        public virtual ICollection<Purchase> Purchases { get; }

        public Product()
        {
            Purchases = new List<Purchase>();
        }
    }
}