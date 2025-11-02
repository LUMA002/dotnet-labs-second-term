using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Domain.Entities
{
    public sealed class Train
    {
        public Guid TrainId { get; init; }
        public string TrainNumber { get; set; } = string.Empty;
        public Guid TrainTypeId { get; set; }
        public TrainType? TrainType { get; set; } 
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();
    }
}



