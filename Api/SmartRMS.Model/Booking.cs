using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.Model
{
    public class Booking
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public long? CustomerAddressId { get; set; }
        public virtual CustomerAddress CustomerAddress { get; set; } 
        public long? DriverId { get; set; }
        public virtual Driver Driver { get; set; }

        public List<BookingDetails> BookingDetails { get; set; }

        public DateTime Date { get; set; }
        public float Subtotal { get; set; }
        public float TotalDiscount { get; set; }
        public float GrandTotal { get; set; }
        public DateTime DeliveryDate { get; set; }
        [StringLength(100)]
        public string DeliveryTime { get; set; }
        public long StatusId { get; set; }
        public virtual Status Status { get; set; }
        public bool IsActive { get; set; }
    }
}
