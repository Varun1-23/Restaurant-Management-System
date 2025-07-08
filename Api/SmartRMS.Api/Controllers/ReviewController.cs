using SmartRMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;
using SmartRMS.api;

namespace SmartRMS.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReviewController : ApiController
    {
        [HttpPost]
        [Route("SaveReview")]
        public bool SaveReview(ReviewDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                Review addData = new Review();

                addData.Rating = dataDto.Rating;
                addData.Description = dataDto.Description;
                addData.RestaurantId = dataDto.RestaurantId;
                addData.CustomerId = dataDto.CustomerId;
                addData.BookingId = dataDto.BookingId;
                addData.IsActive = true;

                context.Reviews.Add(addData);
                context.SaveChanges();

                return true;
            }
        }

        [HttpGet]
        [Route("GetReviewsByCusId/{id}")]
        public List<ReviewDto> GetReviewsByCusId(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Reviews
                                      .Where(x => x.IsActive == true && x.CustomerId == id)
                                      .Select(x => new ReviewDto
                                      {
                                          Id = x.Id,
                                          Rating = x.Rating,
                                          Description = x.Description,
                                          CustomerId = x.CustomerId,
                                          Customer =  new CustomerDto
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
                                          BookingId = x.BookingId,
                                          Booking = new BookingDto
                                          {
                                              Id = x.Booking.Id,
                                              Date = x.Booking.Date,
                                          },
                                          IsActive = x.IsActive
                                      })
                                      .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("GetReviewsByRestId/{id}")]
        public List<ReviewDto> GetReviewsByRestId(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Reviews
                                      .Where(x => x.IsActive == true && x.RestaurantId == id)
                                      .Select(x => new ReviewDto
                                      {
                                          Id = x.Id,
                                          Rating = x.Rating,
                                          Description = x.Description,
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
                                          BookingId = x.BookingId,
                                          Booking = new BookingDto
                                          {
                                              Id = x.Booking.Id,
                                              Date = x.Booking.Date,
                                          },
                                          IsActive = x.IsActive
                                      })
                                      .ToList();
                return dataList;
            }
        }


        [HttpGet]
        [Route("DeleteReview/{id}")]
        public bool DeleteReview(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Reviews.FirstOrDefault(x => x.Id == id);
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
