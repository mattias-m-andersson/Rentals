using System;

using BookingService.Data;

namespace BookingService
{
    extern alias BookingServiceData;

    using BookingServiceData.BookingService.Data;

    using CostCalculation;

    public class BookingService
    {
        private IBookingRepository bookingRepo;
        private IRentalCostCalculatorResolver calculatorResolver;

        public BookingService(IBookingRepository bookingRepo, IRentalCostCalculatorResolver calculatorResolver)
        {
            this.bookingRepo = bookingRepo;
            this.calculatorResolver = calculatorResolver;
        }

        public Booking NewBooking(string customerId, string carCategory, DateTime startTime, uint odometerOut)
        {
            var bookingData = new BookingData()
            {
                CustomerId = customerId,
                CarCategory = carCategory,
                OdometerOut = odometerOut,
                StartTime = startTime,
            };

            var costCalculator = calculatorResolver.Resolve(bookingData);
            var bookingEntity = bookingRepo.Add(bookingData);

            return new Booking(bookingEntity, costCalculator, bookingRepo);
        }

        public Booking GetBooking(string bookingId)
        {
            var bookingEntity = bookingRepo.GetById(bookingId);
            var costCalculator = calculatorResolver.Resolve(bookingEntity);

            return new Booking(bookingEntity, costCalculator, bookingRepo);
        }
    }
}
