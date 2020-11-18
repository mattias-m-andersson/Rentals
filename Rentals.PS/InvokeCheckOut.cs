using System;
using System.Management.Automation;

namespace Rentals.PS
{
    using BookingService;


    [OutputType(typeof(Booking))]
    [Cmdlet(VerbsLifecycle.Invoke, "CheckOut")]
    public class InvokeCheckOut : PSCmdlet
    {
        private BookingService bookingService;
        private RentalsState state;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string PersonalId { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string CarCategory { get; set; }

        [Parameter(Mandatory = false)]
        public DateTime Start { get; set; } = DateTime.Now;

        [Parameter(Mandatory = true)]
        public uint Odometer { get; set; }

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [ValidateNotNull]
        public BookingService BookingService { get; set; }

        protected override void BeginProcessing()
        {
            state = RentalsState.GetState(SessionState);
            bookingService = bookingService ?? state.BookingServices ?? throw new PSArgumentNullException($"No value supplied for property {nameof(BookingService)}.");
        }

        protected override void ProcessRecord()
        {
            WriteObject(bookingService.NewBooking(PersonalId, CarCategory, Start, Odometer));
        }


    }
}
