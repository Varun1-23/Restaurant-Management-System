using SmartRMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;
using SmartRMS.Api.Dtos;
using System.Runtime.InteropServices.ComTypes;

namespace SmartRMS.api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookingController : ApiController
    {
        [HttpPost]
        [Route("SaveBooking")]
        public bool SaveBooking(BookingDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {
                    var editData = context.Bookings.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {
                          
                        editData.Date = dataDto.Date;
                        editData.Subtotal = dataDto.Subtotal;
                        editData.TotalDiscount = dataDto.TotalDiscount;
                        editData.GrandTotal = dataDto.GrandTotal;
                        editData.DeliveryDate = dataDto.DeliveryDate;
                        editData.DeliveryTime = dataDto.DeliveryTime;
                        editData.StatusId = dataDto.StatusId;
                        editData.IsActive = dataDto.IsActive;
                        editData.CustomerAddressId = dataDto.CustomerAddressId;
                        context.Entry(editData).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Booking addData = new Booking();
                    var rest = context.Restaurants.FirstOrDefault(x => x.Id == dataDto.RestaurantId);

                    if (dataDto.IsWebBooking)
                    {
                        var status = context.Statuses.FirstOrDefault(x => x.Name == "Booked");

                        addData.CustomerId = dataDto.CustomerId;
                        addData.StatusId = status.Id;
                    }
                    else
                    {
                        
                        var status = context.Statuses.FirstOrDefault(x => x.Name == "POS Bill");
                        addData.StatusId = status.Id;

                        if (dataDto.CustomerMob != null && dataDto.CustomerMob != "")
                        {
                            var cus = context.Customers.FirstOrDefault(x => x.MobileNo == dataDto.CustomerMob);
                            if (cus != null)
                            {
                                addData.CustomerId = cus.Id;
                            }
                            else
                            {
                                Customer cusData = new Customer();

                                cusData.Name = dataDto.CustomerName;
                                cusData.MobileNo = dataDto.CustomerMob;
                                cusData.Email = dataDto.CustomerEmail;
                                cusData.LocationId = rest.LocationId;
                                cusData.StateId = rest.StateId;
                                cusData.DistrictId = rest.DistrictId;
                                cusData.IsActive = true;

                                context.Customers.Add(cusData);
                                context.SaveChanges();

                                addData.CustomerId = cusData.Id;
                            }
                        }
                        else
                        {
                            var cus = context.Customers.FirstOrDefault(x => x.Name == "CASH CUSTOMER");
                            addData.CustomerId = cus.Id;
                        }
                    }

                 
                    addData.RestaurantId = dataDto.RestaurantId;
                    addData.Date = dataDto.Date;
                    addData.Subtotal = dataDto.Subtotal;
                    addData.TotalDiscount = dataDto.TotalDiscount;
                    addData.GrandTotal = dataDto.GrandTotal;
                    addData.DeliveryDate = dataDto.DeliveryDate;
                    addData.DeliveryTime = dataDto.DeliveryTime;
                    addData.CustomerAddressId = dataDto.CustomerAddressId;

                    addData.IsActive = true;
                    context.Bookings.Add(addData);
                    context.SaveChanges();

                    foreach (var item in dataDto.BookingDetails)
                    {

                        BookingDetails detail = new BookingDetails();

                        detail.BookingId = addData.Id;
                        detail.ProductId = item.ProductId;
                        detail.Quantity = item.Quantity;
                        detail.Price = item.Price;
                        detail.OfferPrice = item.OfferPrice;
                        detail.Total = item.Total;
                        detail.Remarks = item.Remarks;
                        detail.IsActive = true;
                        context.BookingDetails.Add(detail);
                        context.SaveChanges();
                    }

                    return true;
                }
                return false;
            }
        }


        [HttpGet]
        [Route("GetBookings")]
        public List<BookingDto> GetBookings()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Bookings
                                      .Where(x => x.IsActive == true)
                                      .Select(x => new BookingDto
                                      {
                                          Id = x.Id,
                                          CustomerId = x.CustomerId,
                                          Customer = new CustomerDto
                                          {
                                              Id = x.Customer.Id,
                                              Name = x.Customer.Name,
                                          },
                                          RestaurantId = x.RestaurantId,
                                          Restaurant = new RestaurantDto
                                          {
                                              Id = x.Restaurant.Id,
                                              Name = x.Restaurant.Name,
                                          },
                                          Date = x.Date,
                                          Subtotal = x.Subtotal,
                                          TotalDiscount = x.TotalDiscount,
                                          GrandTotal = x.GrandTotal,
                                          DeliveryDate = x.DeliveryDate,
                                          DeliveryTime = x.DeliveryTime,
                                          StatusId = x.StatusId,
                                          Status = new StatusDto
                                          {
                                              Id = x.Status.Id,
                                              Name = x.Status.Name,
                                          },
                                          DriverId = x.DriverId,
                                          Driver = new DriverDto
                                          {
                                              Id = x.Driver != null ? x.Driver.Id: 0,
                                              Name = x.Driver != null ? x.Driver.Name : "",
                                          },
                                          IsActive = x.IsActive,

                                      }).OrderByDescending(x=>x.Date)
                                      .ToList();
                return dataList;
            }
        }
        

        [HttpPost]
        [Route("GetBookings2")]
        public List<BookingDto> GetBookings2(FilterDto filterDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Bookings.Where(x => x.IsActive == true
                       && x.Date >= filterDto.FromDate
                       && x.Date <= filterDto.ToDate)
                                      .Where(x => x.IsActive == true)
                                      .Select(x => new BookingDto
                                      {
                                          Id = x.Id,
                                          CustomerId = x.CustomerId,
                                          Customer = new CustomerDto
                                          {
                                              Id = x.Customer.Id,
                                              Name = x.Customer.Name,
                                          },
                                          RestaurantId = x.RestaurantId,
                                          Restaurant = new RestaurantDto
                                          {
                                              Id = x.Restaurant.Id,
                                              Name = x.Restaurant.Name,
                                          },
                                          Date = x.Date,
                                          Subtotal = x.Subtotal,
                                          TotalDiscount = x.TotalDiscount,
                                          GrandTotal = x.GrandTotal,
                                          DeliveryDate = x.DeliveryDate,
                                          DeliveryTime = x.DeliveryTime,
                                          StatusId = x.StatusId,
                                          Status = new StatusDto
                                          {
                                              Id = x.Status.Id,
                                              Name = x.Status.Name,
                                          },
                                          DriverId = x.DriverId,
                                          Driver = new DriverDto
                                          {
                                              Id = x.Driver != null ? x.Driver.Id: 0,
                                              Name = x.Driver != null ? x.Driver.Name : "",
                                          },
                                          IsActive = x.IsActive,

                                      }).OrderByDescending(x=>x.Date)
                                      .ToList();
                return dataList;
            }
        }


        [HttpGet]
        [Route("GetBookingsByCustomer/{id}")]
        public List<BookingDto> GetBookingsByCustomer(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Bookings.Where(x => x.IsActive == true && x.CustomerId == id)
                    .Select(x => new BookingDto
                    {
                        Id = x.Id,

                        RestaurantId = x.RestaurantId,
                        Restaurant = new RestaurantDto
                        {
                            Id = x.Restaurant.Id,
                            Name = x.Restaurant.Name,
                        },
                        CustomerId = x.CustomerId,
                        Customer = new CustomerDto
                        {
                            Id = x.Customer.Id,
                            Name = x.Customer.Name,
                        },
                        Date = x.Date,
                        Subtotal = x.Subtotal,
                        TotalDiscount = x.TotalDiscount,
                        GrandTotal = x.GrandTotal,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryTime = x.DeliveryTime,
                        StatusId = x.StatusId,
                        Status = new StatusDto
                        {
                            Id = x.Status.Id,
                            Name = x.Status.Name,
                        },
                        DriverId = x.DriverId,
                        Driver = new DriverDto
                        {
                            Id = x.Driver != null ? x.Driver.Id : 0,
                            Name = x.Driver != null ? x.Driver.Name : "",
                        },
                        IsActive = x.IsActive,

                    }).OrderByDescending(x => x.Date)
                    .ToList();
                return dataList;
            }
        }

        
        [HttpPost]
        [Route("GetBookingsByCustomer2")]
        public List<BookingDto> GetBookingsByCustomer2(FilterDto filterDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Bookings.Where(x => x.IsActive == true 
                        && x.CustomerId == filterDto.CustomerId && x.Date >= filterDto.FromDate
                        && x.Date <= filterDto.ToDate)
                    .Select(x => new BookingDto
                    {
                        Id = x.Id,

                        RestaurantId = x.RestaurantId,
                        Restaurant = new RestaurantDto
                        {
                            Id = x.Restaurant.Id,
                            Name = x.Restaurant.Name,
                        },
                        CustomerId = x.CustomerId,
                        Customer = new CustomerDto
                        {
                            Id = x.Customer.Id,
                            Name = x.Customer.Name,
                        },
                        Date = x.Date,
                        Subtotal = x.Subtotal,
                        TotalDiscount = x.TotalDiscount,
                        GrandTotal = x.GrandTotal,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryTime = x.DeliveryTime,
                        StatusId = x.StatusId,
                        Status = new StatusDto
                        {
                            Id = x.Status.Id,
                            Name = x.Status.Name,
                        },
                        DriverId = x.DriverId,
                        Driver = new DriverDto
                        {
                            Id = x.Driver != null ? x.Driver.Id : 0,
                            Name = x.Driver != null ? x.Driver.Name : "",
                        },
                        IsActive = x.IsActive,

                    }).OrderByDescending(x => x.Date)
                    .ToList();
                return dataList;
            }
        }

        
        [HttpGet]
        [Route("GetBookingsDetail/{id}")]
        public BookingDto GetBookingsDetail(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Bookings.Where(x => x.IsActive == true && x.Id == id)
                    .Select(x => new BookingDto
                    {
                        Id = x.Id,

                        RestaurantId = x.RestaurantId,
                        Restaurant = new RestaurantDto
                        {
                            Id = x.Restaurant.Id,
                            Name = x.Restaurant.Name,
                        },
                        CustomerId = x.CustomerId,
                        Customer = new CustomerDto
                        {
                            Id = x.Customer.Id,
                            Name = x.Customer.Name,
                            MobileNo = x.Customer.MobileNo,
                        },
                        CustomerAddressId = x.CustomerAddressId,
                        CustomerAddress = new CustomerAddressDto
                        {
                            Id = x.CustomerAddress != null ? x.CustomerAddress.Id: 0,
                            Address1 = x.CustomerAddress != null ? x.CustomerAddress.Address1 : "",
                            Address2 = x.CustomerAddress != null ? x.CustomerAddress.Address2 : "",
                            Pincode = x.CustomerAddress != null ? x.CustomerAddress.Pincode : "",
                            LocationName = x.CustomerAddress != null ? x.CustomerAddress.Location.Name : "",
                        },
                        Date = x.Date,
                        Subtotal = x.Subtotal,
                        TotalDiscount = x.TotalDiscount,
                        GrandTotal = x.GrandTotal,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryTime = x.DeliveryTime,
                        StatusId = x.StatusId,
                        Status = new StatusDto
                        {
                            Id = x.Status.Id,
                            Name = x.Status.Name,
                        },
                        DriverId = x.DriverId,
                        Driver = new DriverDto
                        {
                            Id = x.Driver != null ? x.Driver.Id : 0,
                            Name = x.Driver != null ? x.Driver.Name : "",
                        },
                        IsActive = x.IsActive,
                        BookingDetails = x.BookingDetails.Where(y=>y.IsActive)
                        .Select(y=> new BookingDetailsDto
                        {
                            Id = y.Id,
                            ProductId = y.ProductId,
                            Product = new ProductDto
                            {
                                Id = y.Product.Id,
                                Name = y.Product.Name,
                                Photo = y.Product.Photo,
                            },
                            Quantity= y.Quantity,
                            Price = y.Price,
                            OfferPrice = y.OfferPrice,
                            Total = y.Total,
                            Remarks = y.Remarks,
                        }).ToList()

                    })
                    .ToList().FirstOrDefault();
                return dataList;
            }
        }


        [HttpGet]
        [Route("GetBookingsByRestaurant/{id}")]
        public List<BookingDto> GetBookingsByRestaurant(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Bookings.Where(x => x.IsActive == true && x.RestaurantId == id
                && x.Status.Name != "POS Bill")
                    .Select(x => new BookingDto
                    {
                        Id = x.Id,
                        
                        RestaurantId = x.RestaurantId,
                        Restaurant = new RestaurantDto
                        {
                            Id = x.Restaurant.Id,
                            Name = x.Restaurant.Name,
                        },
                        CustomerId = x.CustomerId,
                        Customer = new CustomerDto
                        {
                            Id = x.Customer.Id,
                            Name = x.Customer.Name,
                        },
                        Date = x.Date,
                        Subtotal = x.Subtotal,
                        TotalDiscount = x.TotalDiscount,
                        GrandTotal = x.GrandTotal,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryTime = x.DeliveryTime,
                        StatusId = x.StatusId,
                        Status = new StatusDto
                        {
                            Id = x.Status.Id,
                            Name = x.Status.Name,
                        },
                        DriverId = x.DriverId,
                        Driver = new DriverDto
                        {
                            Id = x.Driver != null ? x.Driver.Id : 0,
                            Name = x.Driver != null ? x.Driver.Name : "",
                        },
                        IsActive = x.IsActive,

                    }).OrderByDescending(x => x.Date)
                    .ToList();
                return dataList;
            }
        }
        
        [HttpPost]
        [Route("GetBookingsByRestaurant2")]
        public List<BookingDto> GetBookingsByRestaurant2(FilterDto filterDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Bookings.Where(x => x.IsActive == true
                       && x.RestaurantId == filterDto.RestaurantId && x.Date >= filterDto.FromDate
                       && x.Date <= filterDto.ToDate && x.Status.Name != "POS Bill")
                    .Select(x => new BookingDto
                    {
                        Id = x.Id,
                        
                        RestaurantId = x.RestaurantId,
                        Restaurant = new RestaurantDto
                        {
                            Id = x.Restaurant.Id,
                            Name = x.Restaurant.Name,
                        },
                        CustomerId = x.CustomerId,
                        Customer = new CustomerDto
                        {
                            Id = x.Customer.Id,
                            Name = x.Customer.Name,
                        },
                        Date = x.Date,
                        Subtotal = x.Subtotal,
                        TotalDiscount = x.TotalDiscount,
                        GrandTotal = x.GrandTotal,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryTime = x.DeliveryTime,
                        StatusId = x.StatusId,
                        Status = new StatusDto
                        {
                            Id = x.Status.Id,
                            Name = x.Status.Name,
                        },
                        DriverId = x.DriverId,
                        Driver = new DriverDto
                        {
                            Id = x.Driver != null ? x.Driver.Id : 0,
                            Name = x.Driver != null ? x.Driver.Name : "",
                        },
                        IsActive = x.IsActive,

                    }).OrderByDescending(x => x.Date)
                    .ToList();
                return dataList;
            }
        }
        
        [HttpPost]
        [Route("GetBillsByRestaurant2")]
        public List<BookingDto> GetBillsByRestaurant2(FilterDto filterDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Bookings.Where(x => x.IsActive == true
                     && x.RestaurantId == filterDto.RestaurantId && x.Date >= filterDto.FromDate
                     && x.Date <= filterDto.ToDate && x.Status.Name == "POS Bill")
                    .Select(x => new BookingDto
                    {
                        Id = x.Id,
                        
                        RestaurantId = x.RestaurantId,
                        Restaurant = new RestaurantDto
                        {
                            Id = x.Restaurant.Id,
                            Name = x.Restaurant.Name,
                        },
                        CustomerId = x.CustomerId,
                        Customer = new CustomerDto
                        {
                            Id = x.Customer.Id,
                            Name = x.Customer.Name,
                        },
                        Date = x.Date,
                        Subtotal = x.Subtotal,
                        TotalDiscount = x.TotalDiscount,
                        GrandTotal = x.GrandTotal,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryTime = x.DeliveryTime,
                        StatusId = x.StatusId,
                        Status = new StatusDto
                        {
                            Id = x.Status.Id,
                            Name = x.Status.Name,
                        },
                        DriverId = x.DriverId,
                        Driver = new DriverDto
                        {
                            Id = x.Driver != null ? x.Driver.Id : 0,
                            Name = x.Driver != null ? x.Driver.Name : "",
                        },
                        IsActive = x.IsActive,

                    }).OrderByDescending(x => x.Date)
                    .ToList();
                return dataList;
            }
        }
        
        [HttpGet]
        [Route("GetBillsByRestaurant/{id}")]
        public List<BookingDto> GetBillsByRestaurant(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Bookings.Where(x => x.IsActive == true && x.RestaurantId == id
                && x.Status.Name == "POS Bill")
                    .Select(x => new BookingDto
                    {
                        Id = x.Id,
                        
                        RestaurantId = x.RestaurantId,
                        Restaurant = new RestaurantDto
                        {
                            Id = x.Restaurant.Id,
                            Name = x.Restaurant.Name,
                        },
                        CustomerId = x.CustomerId,
                        Customer = new CustomerDto
                        {
                            Id = x.Customer.Id,
                            Name = x.Customer.Name,
                        },
                        Date = x.Date,
                        Subtotal = x.Subtotal,
                        TotalDiscount = x.TotalDiscount,
                        GrandTotal = x.GrandTotal,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryTime = x.DeliveryTime,
                        StatusId = x.StatusId,
                        Status = new StatusDto
                        {
                            Id = x.Status.Id,
                            Name = x.Status.Name,
                        },
                        DriverId = x.DriverId,
                        Driver = new DriverDto
                        {
                            Id = x.Driver != null ? x.Driver.Id : 0,
                            Name = x.Driver != null ? x.Driver.Name : "",
                        },
                        IsActive = x.IsActive,

                    }).OrderByDescending(x => x.Date)
                    .ToList();
                return dataList;
            }
        }
        
        [HttpGet]
        [Route("GetBillsByDriver/{id}")]
        public List<BookingDto> GetBillsBydriver(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Bookings.Where(x => x.IsActive == true && x.DriverId == id
                && x.Status.Name == "On The Way")
                    .Select(x => new BookingDto
                    {
                        Id = x.Id,
                        
                        RestaurantId = x.RestaurantId,
                        Restaurant = new RestaurantDto
                        {
                            Id = x.Restaurant.Id,
                            Name = x.Restaurant.Name,
                        },
                        CustomerId = x.CustomerId,
                        Customer = new CustomerDto
                        {
                            Id = x.Customer.Id,
                            Name = x.Customer.Name,
                        },
                        Date = x.Date,
                        Subtotal = x.Subtotal,
                        TotalDiscount = x.TotalDiscount,
                        GrandTotal = x.GrandTotal,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryTime = x.DeliveryTime,
                        StatusId = x.StatusId,
                        Status = new StatusDto
                        {
                            Id = x.Status.Id,
                            Name = x.Status.Name,
                        },
                        DriverId = x.DriverId,
                        Driver = new DriverDto
                        {
                            Id = x.Driver != null ? x.Driver.Id : 0,
                            Name = x.Driver != null ? x.Driver.Name : "",
                        },
                        IsActive = x.IsActive,

                    }).OrderByDescending(x => x.Date)
                    .ToList();
                return dataList;
            }
        }
        
        [HttpGet]
        [Route("GetAllBillsBydriver/{id}")]
        public List<BookingDto> GetAllBillsBydriver(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Bookings.Where(x => x.IsActive == true && x.DriverId == id)
                    .Select(x => new BookingDto
                    {
                        Id = x.Id,
                        
                        RestaurantId = x.RestaurantId,
                        Restaurant = new RestaurantDto
                        {
                            Id = x.Restaurant.Id,
                            Name = x.Restaurant.Name,
                        },
                        CustomerId = x.CustomerId,
                        Customer = new CustomerDto
                        {
                            Id = x.Customer.Id,
                            Name = x.Customer.Name,
                        },
                        Date = x.Date,
                        Subtotal = x.Subtotal,
                        TotalDiscount = x.TotalDiscount,
                        GrandTotal = x.GrandTotal,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryTime = x.DeliveryTime,
                        StatusId = x.StatusId,
                        Status = new StatusDto
                        {
                            Id = x.Status.Id,
                            Name = x.Status.Name,
                        },
                        DriverId = x.DriverId,
                        Driver = new DriverDto
                        {
                            Id = x.Driver != null ? x.Driver.Id : 0,
                            Name = x.Driver != null ? x.Driver.Name : "",
                        },
                        IsActive = x.IsActive,

                    }).OrderByDescending(x => x.Date)
                    .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("DeleteBooking/{id}")]
        public bool DeleteBooking(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Bookings.FirstOrDefault(x => x.Id == id);
                if (deleteData != null)
                {
                    deleteData.IsActive = false;
                    context.Entry(deleteData).Property(x => x.IsActive).IsModified = true;
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
        }
   
        [HttpGet]
        [Route("CancelBooking/{id}")]
        public int CancelBooking(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Bookings.FirstOrDefault(x => x.Id == id);
                if (deleteData != null)
                {
                    if (deleteData.Status.Name != "Booked")
                    {
                        return 2;
                    }

                    var status = context.Statuses.FirstOrDefault(x => x.Name == "Cancelled");
                    long statusId = 0;
                    if (status == null)
                    {
                        Status addData = new Status();
                        addData.Name = "Cancelled";
                        addData.IsActive = true;
                        context.Statuses.Add(addData);
                        context.SaveChanges();

                        statusId = addData.Id;
                    }
                    else
                    {
                        statusId = status.Id;
                    }

                    deleteData.StatusId = statusId;
                    context.Entry(deleteData).Property(x => x.StatusId).IsModified = true;
                    context.SaveChanges();
                    return 1;
                }

                return 0;
            }
        }
   
        [HttpGet]
        [Route("UpdateStatus/{id}/{sid}")]
        public int UpdateStatus(long id, long sid)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Bookings.FirstOrDefault(x => x.Id == id);
                if (deleteData != null)
                {
                    deleteData.StatusId = sid;
                    context.Entry(deleteData).Property(x => x.StatusId).IsModified = true;
                    context.SaveChanges();
                    return 1;
                }

                return 0;
            }
        }

        [HttpGet]
        [Route("UpdateDriver/{id}/{did}")]
        public int UpdateDriver(long id, long did)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Bookings.FirstOrDefault(x => x.Id == id);
                if (deleteData != null)
                {
                    var status = context.Statuses.FirstOrDefault(x => x.IsActive && x.Name == "On The Way");
                    if (status  == null)
                    {
                        status.Name = "On The Way";
                        status.IsActive = true;
                        context.Statuses.Add(status);
                        context.SaveChanges();
                    }

                    deleteData.DriverId = did;
                    deleteData.StatusId = status.Id;
                    context.Entry(deleteData).Property(x => x.DriverId).IsModified = true;
                    context.Entry(deleteData).Property(x => x.StatusId).IsModified = true;
                    context.SaveChanges();
                    return 1;
                }

                return 0;
            }
        }

        [HttpGet]
        [Route("DeliverOrder/{id}")]
        public int DeliverOrder(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Bookings.FirstOrDefault(x => x.Id == id);
                if (deleteData != null)
                {
                    var status = context.Statuses.FirstOrDefault(x => x.IsActive && x.Name == "Delivered");
                    if (status  == null)
                    {
                        status.Name = "Delivered";
                        status.IsActive = true;
                        context.Statuses.Add(status);
                        context.SaveChanges();
                    }                  

                    deleteData.DeliveryTime = DateTime.Now.Hour + " : " + DateTime.Now.Minute;
                    deleteData.StatusId = status.Id;
                    context.Entry(deleteData).Property(x => x.DeliveryTime).IsModified = true;
                    context.Entry(deleteData).Property(x => x.StatusId).IsModified = true;
                    context.SaveChanges();

                    DriverWallet wallet = new DriverWallet();

                    wallet.DriverId = deleteData.DriverId.Value;
                    wallet.BookingId = deleteData.Id;
                    wallet.Amount = 30;

                    context.DriverWallets.Add(wallet);
                    context.SaveChanges();


                    return 1;
                }

                return 0;
            }
        }
   
    }
}
