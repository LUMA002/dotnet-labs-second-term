namespace Labs.Domain.Entities
{
    public sealed class Wagon
    {
        public Guid WagonId { get; init; }
        public Guid TrainId { get; set; }
        public string WagonNumber { get; set; } = null!;
        public Guid WagonTypeId { get; set; }

        public Train? Train { get; set; }
        public WagonType? WagonType { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}