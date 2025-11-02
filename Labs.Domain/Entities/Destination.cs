using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Domain.Entities
{
    public sealed class Destination
    {
        public Guid DestinationId { get; init; }
        public string DestinationName { get; set; } = string.Empty;
        public int Distance { get; set; } 
        public decimal BasePrice { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}



