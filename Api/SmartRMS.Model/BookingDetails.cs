using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.Model
{
    public class BookingDetails
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
        public virtual Booking Booking { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }

        public float Quantity { get; set; }
        public float Price { get; set; }
        public float OfferPrice { get; set; }
        public float Total { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
