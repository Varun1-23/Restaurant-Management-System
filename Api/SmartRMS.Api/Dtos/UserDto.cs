using SmartRMS.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRMS.Api.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public string Role { get; set; }
        public bool IsBlocked { get; set; }
        public long? CustomerId { get; set; }
        public virtual CustomerDto Customer { get; set; }
        public long? RestaurantId { get; set; }
        public virtual RestaurantDto Restaurant { get; set; }
        public long? EmployeeId { get; set; }
        public virtual EmployeeDto Employee { get; set; }
    }
}