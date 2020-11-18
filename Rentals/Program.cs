using System;
using System.Collections.Generic;
using System.IO;

namespace Rentals
{
    using BookingService;
    using BookingService.CostCalculation;


    class Program
    {
        static void Main(string[] args)
        {
            var bookingService = CreateBookingService();

            var startTime = DateTime.Now;
            var kmDistance = 130u;
            var odometerStart = 135000u;
            var odometerEnd = odometerStart + kmDistance;

            var booking = bookingService.NewBooking("1234567", "Småbil", startTime, odometerStart);
            booking.Close(startTime.AddDays(2), odometerEnd);
        }

        private static BookingService CreateBookingService()
        {
            var dataStoreFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Rentals.json");
            var bookingRepo = new JsonFileBookingStore.JsonFileBookingRepository(dataStoreFileName);
            return new BookingService(bookingRepo, CreateCostCalculatorResolver());
        }

        private static IRentalCostCalculatorResolver CreateCostCalculatorResolver()
        {
            const decimal dayFee = 275;
            const decimal kmFee = 12;

            var mappings = new Dictionary<string, IRentalCostCalculator>()
            {
                {"Småbil", new CarCategoryCostCalculator(dayFee)},
                {"Van", new CarCategoryCostCalculator(dayFee, 1.2m, kmFee, 1m )},
                {"Minibuss", new CarCategoryCostCalculator(dayFee, 1.7m, kmFee, 1.5m)},
            };

            return new RentalCostCalculatorResolver(mappings);

        }
    }
}
