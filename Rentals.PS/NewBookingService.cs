using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;

namespace Rentals.PS
{
    using BookingService;
    using BookingService.CostCalculation;

    [Cmdlet(VerbsCommon.New, "BookingService")]
    [OutputType(typeof(BookingService))]
    public class NewBookingService : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            WriteObject(CreateBookingService());
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
