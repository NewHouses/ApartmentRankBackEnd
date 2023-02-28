using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRank.Domain.ValueObjects
{
    public class ParkingSpace
    {
        public bool hasParkingSpace { get; set; }
        public bool isParkingSpaceIncludedInPrice { get; set; }
    }
}
