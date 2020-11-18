using System.Management.Automation;

namespace Rentals.PS
{
    public class RentalsState
    {
        public BookingService.BookingService BookingServices { get; set; }

        internal static RentalsState GetState(SessionState sessionState)
        {
            if (!(sessionState.PSVariable.GetValue(nameof(RentalsState), default(RentalsState)) is RentalsState state))
            {
                sessionState.PSVariable.Set(nameof(RentalsState), state = new RentalsState());
            }

            return state;
        }
    }
}
