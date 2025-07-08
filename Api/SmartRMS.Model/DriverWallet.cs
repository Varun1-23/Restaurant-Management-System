using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.Model
{
    public class DriverWallet
    {
        public long Id { get; set; }
        public float Amount { get; set; }
        public long BookingId { get; set; }
        public virtual Booking Booking { get; set; }
        public long DriverId { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
