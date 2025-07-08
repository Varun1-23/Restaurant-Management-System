using SmartRMS.api;
using SmartRMS.Api.Dtos;
using SmartRMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SmartRMS.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DriverController : ApiController
    {
        [HttpPost]
        [Route("SaveDriver")]
        public bool SaveDriver(DriverDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var editData = context.Drivers.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {

                        editData.Name = dataDto.Name;
                        editData.MobileNo = dataDto.MobileNo;
                        editData.Email = dataDto.Email;
                        editData.Designation = dataDto.Designation;
                        editData.RestaurantId = dataDto.RestaurantId;
                        editData.LocationId = dataDto.LocationId;
                        editData.Address = dataDto.Address;
                        editData.IsActive = dataDto.IsActive;
                        editData.IdExpiry = dataDto.IdExpiry;

                        if (dataDto.Photo != null && dataDto.Photo != "" && !dataDto.Photo.Contains("UploadedFiles"))
                        {
                            Guid id = Guid.NewGuid();
                            var imgData = dataDto.Photo.Substring(dataDto.Photo.IndexOf(",") + 1);
                            byte[] bytes = Convert.FromBase64String(imgData);
                            Image image;
                            using (MemoryStream ms = new MemoryStream(bytes))
                            {
                                image = Image.FromStream(ms);
                            }
                            Bitmap b = new Bitmap(image);
                            string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                            b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            editData.Photo = string.Concat("UploadedFiles\\" + id + ".jpg");
                        }
                        if (dataDto.IdPhoto != null && dataDto.IdPhoto != "" && !dataDto.IdPhoto.Contains("UploadedFiles"))
                        {
                            Guid id = Guid.NewGuid();
                            var imgData = dataDto.IdPhoto.Substring(dataDto.IdPhoto.IndexOf(",") + 1);
                            byte[] bytes = Convert.FromBase64String(imgData);
                            Image image;
                            using (MemoryStream ms = new MemoryStream(bytes))
                            {
                                image = Image.FromStream(ms);
                            }
                            Bitmap b = new Bitmap(image);
                            string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                            b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            editData.IdPhoto = string.Concat("UploadedFiles\\" + id + ".jpg");
                        }

                        context.Entry(editData).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Driver addData = new Driver();
                    addData.Name = dataDto.Name;
                    addData.MobileNo = dataDto.MobileNo;
                    addData.Email = dataDto.Email;
                    addData.Designation = dataDto.Designation;
                    addData.RestaurantId = dataDto.RestaurantId;
                    addData.LocationId = dataDto.LocationId;
                    addData.Address = dataDto.Address;
                    addData.IdExpiry = dataDto.IdExpiry;
                    addData.IsActive = true;

                    if (dataDto.Photo != null && dataDto.Photo != "")
                    {
                        Guid id = Guid.NewGuid();
                        var imgData = dataDto.Photo.Substring(dataDto.Photo.IndexOf(",") + 1);
                        byte[] bytes = Convert.FromBase64String(imgData);
                        Image image;
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            image = Image.FromStream(ms);
                        }
                        Bitmap b = new Bitmap(image);
                        string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                        b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        addData.Photo = string.Concat("UploadedFiles\\" + id + ".jpg");
                    }
                    if (dataDto.IdPhoto != null && dataDto.IdPhoto != "")
                    {
                        Guid id = Guid.NewGuid();
                        var imgData = dataDto.IdPhoto.Substring(dataDto.IdPhoto.IndexOf(",") + 1);
                        byte[] bytes = Convert.FromBase64String(imgData);
                        Image image;
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            image = Image.FromStream(ms);
                        }
                        Bitmap b = new Bitmap(image);
                        string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                        b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        addData.IdPhoto = string.Concat("UploadedFiles\\" + id + ".jpg");
                    }

                    context.Drivers.Add(addData);
                    context.SaveChanges();

                    User user = new User();

                    user.UserName = dataDto.Email;
                    user.DriverId = addData.Id;
                    user.Role = "Driver";
                    user.IsActive = true;

                    var passwordSalt = AuthenticationBL.CreatePasswordSalt(Encoding.ASCII.GetBytes(dataDto.Password));
                    user.PasswordSalt = Convert.ToBase64String(passwordSalt);
                    var password = AuthenticationBL.CreateSaltedPassword(passwordSalt, Encoding.ASCII.GetBytes(dataDto.Password));
                    user.Password = Convert.ToBase64String(password);

                    context.Users.Add(user);
                    context.SaveChanges();

                    return true;
                }
                return false;
            }
        }


        [HttpGet]
        [Route("GetDrivers")]
        public List<DriverDto> GetDrivers()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Drivers
                                      .Where(x => x.IsActive == true)
                                      .Select(x => new DriverDto
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
                                          MobileNo = x.MobileNo,
                                          Email = x.Email,
                                          Designation = x.Designation,
                                          RestaurantId = x.RestaurantId,
                                          Restaurant = new RestaurantDto
                                          {
                                              Id = x.Restaurant.Id,
                                              Name = x.Restaurant.Name,
                                          },
                                          LocationId = x.LocationId,
                                          Location = new LocationDto
                                          {
                                              Id = x.Location.Id,
                                              Name = x.Location.Name,
                                          },
                                          Address = x.Address,
                                          Photo = x.Photo,
                                          IsActive = x.IsActive,
                                          IdExpiry = x.IdExpiry,
                                          IdPhoto = x.IdPhoto,
                                          IsApproved = x.IsApproved,

                                      })
                                      .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("GetDriverById/{id}")]
        public DriverDto GetDriverById(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Drivers
                                      .Where(x => x.Id == id)
                                      .Select(x => new DriverDto
                                      {
                                          Id = x.Id,
                                          Name = x.Name,

                                          Photo = x.Photo,
                                          MobileNo = x.MobileNo,
                                          Email = x.Email,
                                          Designation =x.Designation,
                                          LocationId = x.LocationId,
                                          Location = new LocationDto
                                          {
                                              Id = x.Location.Id,
                                              Name = x.Location.Name,
                                          },
                                          Address = x.Address,
                                          RestaurantId = x.RestaurantId,
                                          Restaurant = new RestaurantDto
                                          {
                                              Id = x.RestaurantId,
                                              Name = x.Restaurant.Name
                                          },
                                         IdExpiry = x.IdExpiry,
                                         IdPhoto = x.IdPhoto,
                                          IsActive = x.IsActive,
                                          IsApproved = x.IsApproved,

                                      })
                                      .ToList().FirstOrDefault();
                return dataList;
            }
        }

        [HttpGet]
        [Route("GetDriversbyRestId/{id}")]
        public List<DriverDto> GetDriversbyRestId(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Drivers
                                      .Where(x => x.IsActive == true && x.RestaurantId == id)
                                      .Select(x => new DriverDto
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
                                          MobileNo = x.MobileNo,
                                          Email = x.Email,
                                          Designation = x.Designation,
                                          RestaurantId = x.RestaurantId,
                                          Restaurant = new RestaurantDto
                                          {
                                              Id = x.Restaurant.Id,
                                              Name = x.Restaurant.Name,
                                          },
                                          LocationId = x.LocationId,
                                          Location = new LocationDto
                                          {
                                              Id = x.Location.Id,
                                              Name = x.Location.Name,
                                          },
                                          Address = x.Address,
                                          Photo = x.Photo,
                                          IsActive = x.IsActive,
                                          IdExpiry = x.IdExpiry,
                                          IdPhoto = x.IdPhoto,
                                          IsApproved = x.IsApproved,
                                      })
                                      .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("DeleteDriver/{id}")]
        public bool DeleteDriver(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var deleteData = context.Drivers.FirstOrDefault(x => x.Id == id);
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
        [Route("ApproveDriver/{id}")]
        public bool ApproveDriver(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var deleteData = context.Drivers.FirstOrDefault(x => x.Id == id);
                if (deleteData != null)
                {
                    deleteData.IsApproved = true;
                    context.Entry(deleteData).Property(x => x.IsActive).IsModified = true;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
          
        [HttpGet]
        [Route("GetDriverWallet/{id}")]
        public List<DriverWalletDto> GetDriverWallet(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.DriverWallets
                        .Where(x => x.DriverId == id)
                        .Select(x => new DriverWalletDto
                        {
                            Id = x.Id,
                            Amount = x.Amount,
                            BookingId = x.BookingId,
                            Booking = new BookingDto
                            {
                                Id = x.Booking.Id,
                                DeliveryDate = x.Booking.DeliveryDate,
                                Date = x.Booking.Date,
                            },

                        })
                        .ToList();
                return dataList;
            }
        }


    }
}
