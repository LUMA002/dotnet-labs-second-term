namespace Labs.Domain.Entities
{
    public class WagonType
    {
        public Guid WagonTypeId { get; set; }
        public string Name { get; set; } = string.Empty; // загальний, плацкартний, купе, люкс
        public decimal Surcharge { get; set; } 
        public ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();

    }
}
