using System;
using System.Data.Entity;
using System.Linq;

namespace SmartRMS.Model
{
    public class SMARTRMSDB : DbContext
    {

        public SMARTRMSDB()
            : base("name=SMARTRMSDB")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<BookingDetails> BookingDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSession> UserSessions { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<Cuisine> Cuisines { get; set; }
        public virtual DbSet<CuisineBooking> CuisineBookings { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<DriverWallet> DriverWallets { get; set; }
    }


}