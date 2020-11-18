using System.Collections.Generic;

namespace BookingService.CostCalculation
{
    extern alias BookingServiceData;

    using BookingServiceData.BookingService.Data;

    public class RentalCostCalculatorResolver : IRentalCostCalculatorResolver
    {
        public RentalCostCalculatorResolver()
        {
            CalculatorMappings = new Dictionary<string, IRentalCostCalculator>();
        }

        public RentalCostCalculatorResolver(Dictionary<string, IRentalCostCalculator> calculatorMappings)
        {
            CalculatorMappings = calculatorMappings;
        }

        public Dictionary<string, IRentalCostCalculator> CalculatorMappings { get; }

        public IRentalCostCalculator Resolve(IBookingEntity booking)
        {
            return CalculatorMappings[booking.CarCategory];
        }
    }
}
