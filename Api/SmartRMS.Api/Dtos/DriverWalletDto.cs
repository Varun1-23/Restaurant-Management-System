using SmartRMS.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRMS.Api.Dtos
{
    public class DriverWalletDto
    {
        public long Id { get; set; }
        public float Amount { get; set; }
        public long BookingId { get; set; }
        public virtual BookingDto Booking { get; set; }
        public long DriverId { get; set; }
        public virtual DriverDto Driver { get; set; }
    }
}