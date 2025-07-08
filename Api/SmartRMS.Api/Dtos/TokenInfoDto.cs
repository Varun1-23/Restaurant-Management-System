using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRMS.Api.Dtos
{
    public class TokenInfoDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public long UserId { get; set; }
        public long? CustomerId { get; set; }
        public long? RestaurantId { get; set; }
        public long? EmployeeId { get; set; }
        public long? DriverId { get; set; }
        public bool IsApproved { get; set; }
        public string RejectionReason { get; set; }
    }
}