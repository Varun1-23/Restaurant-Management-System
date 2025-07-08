using SmartRMS.Api.Dtos;
using SmartRMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.api
{
    public class BookingDto
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public virtual CustomerDto Customer { get; set; }
        public long RestaurantId { get; set; }
        public virtual RestaurantDto Restaurant { get; set; }
        public long? CustomerAddressId { get; set; }
        public virtual CustomerAddressDto CustomerAddress { get; set; }
        public long? DriverId { get; set; }
        public virtual DriverDto Driver { get; set; }

        public List<BookingDetailsDto> BookingDetails { get; set; }

        public DateTime Date { get; set; }
        public float Subtotal { get; set; }
        public float TotalDiscount { get; set; }
        public float GrandTotal { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryTime { get; set; }
        public long StatusId { get; set; }
        public virtual StatusDto Status { get; set; }
        public bool IsActive { get; set; }

        public bool IsWebBooking { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMob { get; set; }
        public string CustomerEmail { get; set; }
    }
}
