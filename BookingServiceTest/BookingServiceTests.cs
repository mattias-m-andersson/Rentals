
using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookingServiceTest
{
    using BookingService.CostCalculation;

    [TestClass]
    public class BookingServiceTests
    {
        [TestMethod]
        public void NewBooking_returns_booking_instance_with_non_blank_booking_number()
        {
            var bookingRepo = new DictionaryBookingStore.DictionaryBookingRepository();

            var bookingService = new BookingService.BookingService(bookingRepo, CreateCostCalculatorResolverForTest());
            var booking = bookingService.NewBooking("1234567", "smaBil", DateTime.Now, 123456);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(booking.BookingId));
        }

        [TestMethod]
        public void Given_a_new_booking_cost_has_no_value()
        {
            var bookingRepo = new DictionaryBookingStore.DictionaryBookingRepository();
            var bookingService = new BookingService.BookingService(bookingRepo, CreateCostCalculatorResolverForTest());
            var startTime = DateTime.Now;
            var kmDistance = 130u;
            var odometerStart = 135000u;
            var odometerEnd = odometerStart + kmDistance;

            var booking = bookingService.NewBooking("1234567", "Småbil", startTime, odometerStart);
            Assert.AreEqual(0m, booking.Cost);
        }

        [TestMethod]
        public void Given_a_new_booking_cost_has_value_after_closing_booking()
        {
            var bookingRepo = new DictionaryBookingStore.DictionaryBookingRepository();
            var bookingService = new BookingService.BookingService(bookingRepo, CreateCostCalculatorResolverForTest());
            var startTime = DateTime.Now;
            var kmDistance = 130u;
            var odometerStart = 135000u;
            var odometerEnd = odometerStart + kmDistance;

            var booking = bookingService.NewBooking("1234567", "Småbil", startTime, odometerStart);
            booking.Close(startTime.AddDays(2), odometerEnd);
            Assert.IsNotNull(booking.Cost);
        }

        private static IRentalCostCalculatorResolver CreateCostCalculatorResolverForTest()
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
