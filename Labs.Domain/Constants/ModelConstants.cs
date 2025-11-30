using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Domain.Constants
{
    public static class ModelConstants
    {
        // Passenger
        public const int PassengerNameMaxLength = 100;
        public const int PassengerAddressMaxLength = 255;
        public const int PassengerPhoneMaxLength = 17;

        // Train
        public const int TrainNumberMaxLength = 10;
        public const int TrainTypeNameMaxLength = 50;

        // Wagon
        public const int WagonNumberMaxLength = 10;
        public const int WagonTypeNameMaxLength = 50;

        // Destination
        public const int DestinationNameMaxLength = 100;

        // Decimal Precision
        public const int DecimalPrecision = 8;
        public const int DecimalScale = 2;
    }
}
