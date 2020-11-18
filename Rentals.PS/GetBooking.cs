using System.Management.Automation;

namespace Rentals.PS
{
    using BS = BookingService;

    [Cmdlet(VerbsCommon.Get, "Booking")]
    [OutputType(typeof(BS.Booking))]
    public class GetBooking : PSCmdlet
    {
        private RentalsState state;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string BookingId { get; set; } =     string.Empty;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public BS.BookingService BookingService { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            state = RentalsState.GetState(SessionState);

        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            BookingService = BookingService ?? state?.BookingServices ?? throw new PSArgumentNullException($"No value supplied for property {nameof(BookingService)}.");
            WriteObject(BookingService.GetBooking(BookingId));
        }
    }
}
