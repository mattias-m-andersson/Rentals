using System;


namespace DictionaryBookingStore
{
    extern alias BookingServiceData;

    using BookingServiceData.BookingService.Data;
    internal class BookingEntity : IBookingEntity
    {
        public BookingEntity() { }

        public BookingEntity(IBookingEntity other)
        {
            BookingId = other.BookingId;
            CustomerId = other.CustomerId;
            CarCategory = other.CarCategory;
            StartTime = other.StartTime;
            EndTime = other.EndTime;
            OdometerIn = other.OdometerIn;
            OdometerOut = other.OdometerOut;
            Cost = other.Cost;
        }

        public string BookingId { get; set; }
        public string CustomerId { get; set; }
        public string CarCategory { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public uint OdometerOut { get; set; }
        public uint OdometerIn { get; set; }
        public decimal Cost { get; set; }
    }
}