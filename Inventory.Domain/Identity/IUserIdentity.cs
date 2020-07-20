namespace Inventory.Domain.Identity
{
    public interface IUserIdentity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
