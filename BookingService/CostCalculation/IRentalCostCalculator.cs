namespace BookingService.CostCalculation
{
    extern alias BookingServiceData;

    using BookingServiceData.BookingService.Data;

    public interface IRentalCostCalculator
    {
        decimal Calculate(IBookingEntity booking);
    }
}
