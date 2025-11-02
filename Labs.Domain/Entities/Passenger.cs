
namespace Labs.Domain.Entities
{
    public sealed class Passenger
    {
        public Guid PassengerId { get; init; }
        public string FirstName {get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}
