namespace Rentals.PS
{
    using System.Management.Automation;

    using BookingService;

    [Cmdlet(VerbsCommon.Set, "Default")]
    public class SetDefault : PSCmdlet
    {
        private RentalsState state;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [ValidateNotNull]
        public BookingService BookingService { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            state = RentalsState.GetState(SessionState);
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            state.BookingServices = BookingService ?? state.BookingServices;
        }
    }
}
