using System;

namespace Inventory.Application.Queries
{
    public class GetProductByIdQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NoOfUnit { get; set; }
        public int ReOrderLevel { get; set; }
        public double UnitPrice { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedDateTime { get; set; }
    }
}
