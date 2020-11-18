using System;

namespace BookingService
{
    extern alias BookingServiceData;

    using BookingServiceData.BookingService.Data;

    using CostCalculation;

    public class Booking
    {
        private IBookingEntity bookingEntity;
        private IRentalCostCalculator costCalculator;
        private IBookingRepository bookingRepo;

        public Booking(IBookingEntity bookingEntity, IRentalCostCalculator costCalculator, IBookingRepository bookingRepo)
        {
            this.bookingEntity = bookingEntity;
            this.costCalculator = costCalculator;
            this.bookingRepo = bookingRepo;
        }

        public string BookingId => bookingEntity.BookingId;
        public string CustomerId => bookingEntity.CustomerId;
        public string CarCategory => bookingEntity.CarCategory;
        public DateTime StartTime => bookingEntity.StartTime;
        public DateTime EndTime => bookingEntity.EndTime;
        public uint OdometerOut => bookingEntity.OdometerOut;
        public uint OdometerIn => bookingEntity.OdometerIn;

        public void Close(DateTime endTime, uint odometerIn)
        {
            bookingEntity.EndTime = endTime;
            bookingEntity.OdometerIn = odometerIn;
            bookingEntity.Cost = costCalculator.Calculate(bookingEntity);
            bookingRepo.Set(bookingEntity);
        }

        public decimal Cost => bookingEntity.Cost;
    }
}
