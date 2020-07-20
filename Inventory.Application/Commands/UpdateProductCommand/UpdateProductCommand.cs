using MediatR;

namespace Inventory.Application.Commands
{
    public class UpdateProductCommand : IRequest<UpdateProductCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NoOfUnit { get; set; }
        public int ReOrderLevel { get; set; }
        public double UnitPrice { get; set; }
    }
}
