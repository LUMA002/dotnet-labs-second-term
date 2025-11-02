namespace Labs.Domain.Entities
{
    public sealed class WagonType
    {
        public Guid WagonTypeId { get; init; }
        public string WagonTypeName { get; set; } = string.Empty; // загальний, плацкартний, купе, люкс
        public decimal Surcharge { get; set; } 
        public ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();

    }
}
