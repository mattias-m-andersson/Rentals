using System;

namespace BookingService.Data
{
    extern alias BookingServiceData;

    using BookingServiceData.BookingService.Data;

    internal class BookingData : IBookingEntity
    {
        public string BookingId { get; }
        public string CustomerId { get; set; }
        public string CarCategory { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public uint OdometerOut { get; set; }
        public uint OdometerIn { get; set; }
        public decimal Cost { get; set; }
    }
}