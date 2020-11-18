using System;
using System.Management.Automation;

using BookingService;

namespace Rentals.PS
{
    [Cmdlet(VerbsLifecycle.Invoke, "CheckIn")]
    public class InvokeCheckIn : Cmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNull]
        public Booking Booking { get; set; }

        [Parameter(Mandatory = false)]
        public DateTime CheckInTime { get; set; } = DateTime.Now;

        [Parameter(Mandatory = true)]
        public uint Odomoeter { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            Booking.Close(CheckInTime, Odomoeter);
        }
    }
}
