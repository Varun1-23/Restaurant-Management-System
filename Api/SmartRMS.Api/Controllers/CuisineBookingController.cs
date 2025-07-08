using SmartRMS.api;
using SmartRMS.Api.Dtos;
using SmartRMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SmartRMS.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CuisineBookingBookingController : ApiController
    {
        [HttpPost]
        [Route("SaveCuisineBooking")]
        public bool SaveCuisineBooking(CuisineBookingDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var editData = context.CuisineBookings.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {
                        editData.Date = dataDto.Date;
                        editData.Time = dataDto.Time;
                        editData.RestaurantId = dataDto.RestaurantId;
                        editData.CuisineId = dataDto.CuisineId;
                        editData.CustomerId = dataDto.CustomerId;
                        context.Entry(editData).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }

                else
                {
                    CuisineBooking addData = new CuisineBooking();
                    addData.Date = dataDto.Date;
                    addData.Time = dataDto.Time;
                    addData.RestaurantId = dataDto.RestaurantId;
                    addData.CuisineId = dataDto.CuisineId;
                    addData.CustomerId = dataDto.CustomerId;
                    addData.IsActive = true;
                    context.CuisineBookings.Add(addData);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        [HttpGet]
        [Route("GetCuisineBookings")]
        public List<CuisineBookingDto> GetCuisineBookings()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.CuisineBookings
                                      .Where(x => x.IsActive == true)
                                      .Select(x => new CuisineBookingDto
                                      {
                                          Id = x.Id,
                                          Date = x.Date,
                                          Time = x.Time,
                                          RestaurantId = x.RestaurantId,
                                          Restaurant = new RestaurantDto
                                          {
                                              Id = x.Restaurant.Id,
                                              Name = x.Restaurant.Name,
                                          },
                                          CuisineId = x.CuisineId,
                                          Cuisine = new CuisineDto
                                          {
                                              Id = x.Cuisine.Id,
                                              Name = x.Cuisine.Name,
                                          },
                                          CustomerId = x.CustomerId,
                                          Customer  = new CustomerDto
                                          {
                                              Id = x.Customer.Id,
                                              Name = x.Customer.Name,
                                          }
                                      })
                                      .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("GetCuisineBookingsByCustomerId/{id}")]
        public List<CuisineBookingDto> GetCuisineBookingsByCustomerId(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.CuisineBookings
                                      .Where(x => x.IsActive == true && x.CustomerId == id)
                                      .Select(x => new CuisineBookingDto
                                      {
                                          Id = x.Id,
                                          Date = x.Date,
                                          Time = x.Time,
                                          RestaurantId = x.RestaurantId,
                                          Restaurant = new RestaurantDto
                                          {
                                              Id = x.Restaurant.Id,
                                              Name = x.Restaurant.Name,
                                          },
                                          CuisineId = x.CuisineId,
                                          Cuisine = new CuisineDto
                                          {
                                              Id = x.Cuisine.Id,
                                              Name = x.Cuisine.Name,
                                          },
                                          CustomerId = x.CustomerId,
                                          Customer = new CustomerDto
                                          {
                                              Id = x.Customer.Id,
                                              Name = x.Customer.Name,
                                          }
                                      })
                                      .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("GetCuisineBookingsByRestaurantId/{id}")]
        public List<CuisineBookingDto> GetCuisineBookingsByRestaurantId(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.CuisineBookings
                                      .Where(x => x.IsActive == true && x.RestaurantId == id)
                                      .Select(x => new CuisineBookingDto
                                      {
                                          Id = x.Id,
                                          Date = x.Date,
                                          Time = x.Time,
                                          RestaurantId = x.RestaurantId,
                                          Restaurant = new RestaurantDto
                                          {
                                              Id = x.Restaurant.Id,
                                              Name = x.Restaurant.Name,
                                          },
                                          CuisineId = x.CuisineId,
                                          Cuisine = new CuisineDto
                                          {
                                              Id = x.Cuisine.Id,
                                              Name = x.Cuisine.Name,
                                          },
                                          CustomerId = x.CustomerId,
                                          Customer = new CustomerDto
                                          {
                                              Id = x.Customer.Id,
                                              Name = x.Customer.Name,
                                          }
                                      })
                                      .ToList();
                return dataList;
            }
        }


        [HttpGet]
        [Route("DeleteCuisineBooking/{id}")]
        public bool DeleteCuisineBooking(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.CuisineBookings.FirstOrDefault(x => x.Id == id);
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
    }
}
