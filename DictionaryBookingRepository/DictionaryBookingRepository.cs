
using System;
using System.Collections.Generic;

namespace DictionaryBookingStore
{
    extern alias BookingServiceData;

    using BookingServiceData.BookingService.Data;

    public class DictionaryBookingRepository : IBookingRepository
    {
        private readonly Dictionary<string, IBookingEntity> bookings = new Dictionary<string, IBookingEntity>();
        public IBookingEntity Add(IBookingEntity entity)
        {
            var entityWithId = new BookingEntity(entity) { BookingId = Guid.NewGuid().ToString() };
            bookings.Add(entityWithId.BookingId, entityWithId);
            return entityWithId;
        }

        public IBookingEntity GetById(string id)
        {
            return bookings[id];
        }

        public void Set(IBookingEntity entity)
        {
            bookings[entity.BookingId] = entity;
        }
    }
}
