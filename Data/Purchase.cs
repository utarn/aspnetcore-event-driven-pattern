using System.Collections.Generic;

namespace aspnetcore_event_driven.Data
{
    public class Purchase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public int Quantity { get; set; }
    }
}