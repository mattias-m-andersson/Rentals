using System;

namespace BookingService.CostCalculation
{
    extern alias BookingServiceData;

    using BookingServiceData.BookingService.Data;

    public class CarCategoryCostCalculator : IRentalCostCalculator
    {
        private decimal dayFee;
        private decimal dayFeeScaleFactor;
        private decimal kmFee;
        private decimal kmFeeScaleFactor;

        public CarCategoryCostCalculator(decimal dayFee) : this(dayFee, 1, 0, 1)
        {
        }

        public CarCategoryCostCalculator(decimal dayFee, decimal kmFee) : this(dayFee, 1, kmFee, 1)
        {
        }

        public CarCategoryCostCalculator(decimal dayFee, decimal dayFeeScaleFactor, decimal kmFee, decimal kmFeeScaleFactor)
        {
            this.dayFee = dayFee;
            this.dayFeeScaleFactor = dayFeeScaleFactor;
            this.kmFee = kmFee;
            this.kmFeeScaleFactor = kmFeeScaleFactor;
        }

        public decimal Calculate(IBookingEntity booking)
        {
            var numberOfDays = (booking.EndTime - booking.StartTime).Days;
            numberOfDays = (numberOfDays <= 0) ? 1 : numberOfDays;

            var kmAmount = booking.OdometerIn - booking.OdometerOut;

            return Calculate(dayFee, dayFeeScaleFactor, (uint)numberOfDays, kmFee, kmFeeScaleFactor, kmAmount);

        }

        private static decimal Calculate(decimal dayFee, decimal dayFeeScaleFactor, uint numberOfDays, decimal kmFee, decimal kmFeeScaleFactor, uint kmAmount)
        {
            return Convert.ToDecimal((dayFee * numberOfDays * dayFeeScaleFactor) + (kmFee * kmAmount * kmFeeScaleFactor));
        }
    }
}
