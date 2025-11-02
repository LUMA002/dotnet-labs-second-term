using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Domain.Entities
{
    public sealed class TrainType
    {
        public Guid TrainTypeId { get; init; }
        public string TypeName { get; set; } = string.Empty; // швидкісний, інтерсіті, пасажирський
        public  ICollection<Train> Trains { get; set; } = new List<Train>();
    }
}
