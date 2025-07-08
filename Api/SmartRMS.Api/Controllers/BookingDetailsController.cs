using SmartRMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;

namespace SmartRMS.api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookingDetailsController : ApiController
    {


        [HttpPost]
        [Route("SaveBookingDetails")]
        public bool SaveBookingDetails(BookingDetailsDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var editData = context.BookingDetails.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {

                        editData.BookingId = dataDto.BookingId;
                        editData.ProductId = dataDto.ProductId;
                        editData.Quantity = dataDto.Quantity;
                        editData.Price = dataDto.Price;
                        editData.OfferPrice = dataDto.OfferPrice;
                        editData.Total = dataDto.Total;
                        editData.Remarks = dataDto.Remarks;
                        editData.IsActive = dataDto.IsActive;
                        context.Entry(editData).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    BookingDetails addData = new BookingDetails();
                    addData.BookingId = dataDto.BookingId;
                    addData.ProductId = dataDto.ProductId;
                    addData.Quantity = dataDto.Quantity;
                    addData.Price = dataDto.Price;
                    addData.OfferPrice = dataDto.OfferPrice;
                    addData.Total = dataDto.Total;
                    addData.Remarks = dataDto.Remarks;
                    addData.IsActive = true;
                    context.BookingDetails.Add(addData);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }


        [HttpGet]
        [Route("GetBookingDetails")]
        public List<BookingDetailsDto> GetBookingDetails()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.BookingDetails
                                      .Where(x => x.IsActive == true)
                                      .Select(x => new BookingDetailsDto
                                      {
                                          Id = x.Id,
                                          
                                          BookingId = x.BookingId,
                                          Booking = new BookingDto
                                          {
                                              Id = x.Booking.Id,
                                              
                                          },
                                          ProductId = x.ProductId,
                                          Product = new ProductDto
                                          {
                                              Id = x.Product.Id,
                                              Name = x.Product.Name,
                                             
                                          },
                                          Quantity = x.Quantity,
                                          Price = x.Price,                                         
                                          OfferPrice = x.OfferPrice,
                                          Total = x.Total,
                                          Remarks = x.Remarks,
                                          IsActive = x.IsActive,

                                      })
                                      .ToList();
                return dataList;
            }
        }


        [HttpGet]
        [Route("DeleteBookingDetails/{id}")]
        public bool DeleteBookingDetails(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.BookingDetails.FirstOrDefault(x => x.Id == id);
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
