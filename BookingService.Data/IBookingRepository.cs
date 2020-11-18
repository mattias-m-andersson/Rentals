
namespace BookingService.Data
{
    public interface IBookingRepository
    {
        IBookingEntity GetById(string id);
        IBookingEntity Add(IBookingEntity entity);
        void Set(IBookingEntity entity);
    }
}
