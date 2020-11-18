using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JsonFileBookingStore
{
    extern alias BookingServiceData;

    using BookingServiceData.BookingService.Data;

    public class JsonFileBookingRepository : IBookingRepository
    {
        private readonly Dictionary<string, BookingEntity> bookings = new Dictionary<string, BookingEntity>();
        private string bookingsFileName;

        public JsonFileBookingRepository(string fileName)
        {
            this.bookingsFileName = fileName;
            bookings = LoadEntitiesFromFile(bookingsFileName);
        }

        public IBookingEntity Add(IBookingEntity entity)
        {
            var entityWithId = new BookingEntity(entity) { BookingId = Guid.NewGuid().ToString() };
            bookings.Add(entityWithId.BookingId, entityWithId);
            SaveEntitiesToFile(bookings, bookingsFileName);
            return entityWithId;
        }

        public IBookingEntity GetById(string id)
        {
            return bookings[id];
        }

        public void Set(IBookingEntity entity)
        {
            bookings[entity.BookingId] = (BookingEntity)entity;
            SaveEntitiesToFile(bookings, bookingsFileName);
        }


        private static Dictionary<string, BookingEntity> LoadEntitiesFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return new Dictionary<string, BookingEntity>();
            }

            var jsonData = new System.Buffers.ReadOnlySequence<byte>(File.ReadAllBytes(path));
            var reader = new Utf8JsonReader(jsonData);
            return JsonSerializer.Deserialize<Dictionary<string, BookingEntity>>(ref reader);
        }

        private static void SaveEntitiesToFile(Dictionary<string, BookingEntity> entities, string path)
        {
            using (var fs = File.OpenWrite(path))
            {
                var writer = new Utf8JsonWriter(fs, new JsonWriterOptions() { Indented = true });
                JsonSerializer.Serialize(writer, entities, new JsonSerializerOptions() { WriteIndented = true });
            }
        }
    }
}
