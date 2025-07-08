using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.api
{
    public class BookingDetailsDto
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
        public virtual BookingDto Booking { get; set; }
        public long ProductId { get; set; }
        public virtual ProductDto Product { get; set; }

        public float Quantity { get; set; }
        public float Price { get; set; }
        public float OfferPrice { get; set; }
        public float Total { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
