using System;

namespace BookingService.Data
{
    public interface IBookingEntity
    {
        string BookingId { get; }
        string CarCategory { get; set; }
        decimal Cost { get; set; }
        string CustomerId { get; set; }
        DateTime EndTime { get; set; }
        uint OdometerIn { get; set; }
        uint OdometerOut { get; set; }
        DateTime StartTime { get; set; }
    }
}