namespace BookingService.CostCalculation
{
    extern alias BookingServiceData;

    using BookingServiceData.BookingService.Data;

    public interface IRentalCostCalculatorResolver
    {
        IRentalCostCalculator Resolve(IBookingEntity booking);
    }
}
