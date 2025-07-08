using SmartRMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using SmartRMS.Api.Dtos;
using System.Text;
using System.Net.Mail;

namespace SmartRMS.api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RestaurantController : ApiController
    {

        [HttpPost]
        [Route("checkEmailRest")]
        public long checkEmailRest(RestaurantDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var old = context.Restaurants.FirstOrDefault(x => x.IsActive && x.Email == dataDto.Email);
                if (old != null)
                {
                    return 2;
                }

                Random random = new Random();
                var otp = random.Next(100000, 999999);

                var fromAddress = new MailAddress("easybookuae552@gmail.com", "No Reply Resto Nest");
                var toAddress = new MailAddress(dataDto.Email, dataDto.Email);
                const string fromPassword = "lpyfpuxxvrcindec";
                const string subject = "Resto Nest Register OTP";
                string body = otp + " : is your one time password for registering to Resto Nest.";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                })
                {
                    message.Headers.Add("Content-type", "text/html");
                    message.Headers.Add("charset", "ISO-8859-1");
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }

                return otp;

            }
        }


        [HttpPost]
        [Route("SaveRestaurant")]
        public bool SaveRestaurant(RestaurantDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var editData = context.Restaurants.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {
                        editData.Name = dataDto.Name; 
                        editData.MobileNo = dataDto.MobileNo;
                        editData.Email = dataDto.Email;
                        editData.Description = dataDto.Description;
                        editData.StateId = dataDto.StateId;
                        editData.DistrictId = dataDto.DistrictId;
                        editData.LocationId = dataDto.LocationId;                        
                        editData.IsActive = dataDto.IsActive;
                        editData.Cost = dataDto.Cost;
                        editData.Type = dataDto.Type;

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
                        if (dataDto.License != null && dataDto.License != "" && !dataDto.License.Contains("UploadedFiles"))
                        {
                            Guid id = Guid.NewGuid();
                            var imgData = dataDto.License.Substring(dataDto.License.IndexOf(",") + 1);
                            byte[] bytes = Convert.FromBase64String(imgData);
                            Image image;
                            using (MemoryStream ms = new MemoryStream(bytes))
                            {
                                image = Image.FromStream(ms);
                            }
                            Bitmap b = new Bitmap(image);
                            string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                            b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            editData.License = string.Concat("UploadedFiles\\" + id + ".jpg");
                        }

                        context.Entry(editData).State = EntityState.Modified;
                        context.SaveChanges();

                        if (dataDto.Password != null && dataDto.Password != "")
                        {
                            var user = context.Users.FirstOrDefault(x => x.RestaurantId == dataDto.Id);

                            if (user != null)
                            {
                                var passwordSalt = AuthenticationBL.CreatePasswordSalt(Encoding.ASCII.GetBytes(dataDto.Password));
                                user.PasswordSalt = Convert.ToBase64String(passwordSalt);
                                var password = AuthenticationBL.CreateSaltedPassword(passwordSalt, Encoding.ASCII.GetBytes(dataDto.Password));
                                user.Password = Convert.ToBase64String(password);

                                context.Entry(user).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            else
                            {
                                User user2 = new User();

                                user2.UserName = dataDto.Email;
                                user2.RestaurantId = dataDto.Id;
                                user2.Role = "Restaurant";
                                user2.IsActive = true;

                                var passwordSalt = AuthenticationBL.CreatePasswordSalt(Encoding.ASCII.GetBytes(dataDto.Password));
                                user2.PasswordSalt = Convert.ToBase64String(passwordSalt);
                                var password = AuthenticationBL.CreateSaltedPassword(passwordSalt, Encoding.ASCII.GetBytes(dataDto.Password));
                                user2.Password = Convert.ToBase64String(password);

                                context.Users.Add(user2);
                                context.SaveChanges();
                            }
                        }
                        

                        return true;
                    }
                }
                else
                {

                    Restaurant addData = new Restaurant();
                    addData.Name = dataDto.Name; 
                    addData.MobileNo = dataDto.MobileNo;
                    addData.Email = dataDto.Email;
                    addData.Description = dataDto.Description;
                    addData.StateId = dataDto.StateId;
                    addData.DistrictId = dataDto.DistrictId;
                    addData.LocationId = dataDto.LocationId;                   
                    addData.IsActive = true;
                    addData.Cost = dataDto.Cost;
                    addData.Type = dataDto.Type;

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
                    
                    if (dataDto.License != null && dataDto.License != "")
                    {
                        Guid id = Guid.NewGuid();
                        var imgData = dataDto.License.Substring(dataDto.License.IndexOf(",") + 1);
                        byte[] bytes = Convert.FromBase64String(imgData);
                        Image image;
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            image = Image.FromStream(ms);
                        }
                        Bitmap b = new Bitmap(image);
                        string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "UploadedFiles\\" + id + ".jpg";
                        b.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        addData.License = string.Concat("UploadedFiles\\" + id + ".jpg");
                    }

                    context.Restaurants.Add(addData);
                    context.SaveChanges();

                    User user = new User();

                    user.UserName = dataDto.Email;
                    user.RestaurantId = addData.Id;
                    user.Role = "Restaurant";
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
        [Route("GetRestaurants")]
        public List<RestaurantDto> GetRestaurants()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Restaurants
                    .Where(x => x.IsActive == true)
                    .Select(x => new RestaurantDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Photo = x.Photo,
                        MobileNo = x.MobileNo,
                        Email = x.Email,
                        Description = x.Description,
                        StateId = x.StateId,
                        State = new StateDto
                        {
                            Id = x.State.Id,
                            Name = x.State.Name,
                        },
                        DistrictId = x.DistrictId,
                        District = new DistrictDto
                        {
                            Id = x.District.Id,
                            Name = x.District.Name,
                        },
                        LocationId = x.LocationId,
                        Location = new LocationDto
                        {
                            Id = x.Location.Id,
                            Name = x.Location.Name
                        },
                        
                        
                        IsActive = x.IsActive,
                        License = x.License,
                        IsApproved = x.IsApproved,
                        Cost = x.Cost,
                        Type = x.Type,

                    })
                    .ToList();
                return dataList;
            }
        }
        

        [HttpGet]
        [Route("GetRestaurantsByCatId/{id}")]
        public List<RestaurantDto> GetRestaurants(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Restaurants
                    .Where(x => x.IsActive == true && context.Categories.Where(y=>y.IsActive && y.Id == id 
                    && y.RestaurantId == x.Id).Count() >0)
                    .Select(x => new RestaurantDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Photo = x.Photo,
                        MobileNo = x.MobileNo,
                        Email = x.Email,
                        Description = x.Description,
                        StateId = x.StateId,
                        License = x.License,
                        IsApproved = x.IsApproved,
                        State = new StateDto
                        {
                            Id = x.State.Id,
                            Name = x.State.Name,
                        },
                        DistrictId = x.DistrictId,
                        District = new DistrictDto
                        {
                            Id = x.District.Id,
                            Name = x.District.Name,
                        },
                        LocationId = x.LocationId,
                        Location = new LocationDto
                        {
                            Id = x.Location.Id,
                            Name = x.Location.Name
                        },
                        
                        
                        IsActive = x.IsActive,
                        Cost = x.Cost,
                        Type = x.Type,

                    })
                    .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("GetRestaurantsByDistrictId/{id}")]
        public List<RestaurantDto> GetRestaurantsByDistrict(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Restaurants
                    .Where(x => x.IsActive == true && x.DistrictId == id)
                    .Select(x => new RestaurantDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Photo = x.Photo,
                        MobileNo = x.MobileNo,
                        Email = x.Email,
                        Description = x.Description,
                        StateId = x.StateId,
                        License = x.License,
                        IsApproved = x.IsApproved,
                        State = new StateDto
                        {
                            Id = x.State.Id,
                            Name = x.State.Name,
                        },
                        DistrictId = x.DistrictId,
                        District = new DistrictDto
                        {
                            Id = x.District.Id,
                            Name = x.District.Name,
                        },
                        LocationId = x.LocationId,
                        Location = new LocationDto
                        {
                            Id = x.Location.Id,
                            Name = x.Location.Name
                        },
                        IsActive = x.IsActive,
                        Cost = x.Cost,
                        Type = x.Type,
                    })
                    .ToList();

                return dataList;
            }
        }
        
        [HttpGet]
        [Route("GetRestaurantsByKeyword/{id}")]
        public List<RestaurantDto> GetRestaurantsByKeyword(string id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Restaurants
                    .Where(x => x.IsActive == true && (x.Name.Contains(id) || 
                    context.Products.Where(y=> y.IsActive && y.Name.Contains(id) && y.RestaurantId == x.Id).Count() >0 )  ) 
                    .Select(x => new RestaurantDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Photo = x.Photo,
                        MobileNo = x.MobileNo,
                        Email = x.Email,
                        License = x.License,
                        IsApproved = x.IsApproved,
                        Description = x.Description,
                        StateId = x.StateId,
                        State = new StateDto
                        {
                            Id = x.State.Id,
                            Name = x.State.Name,
                        },
                        DistrictId = x.DistrictId,
                        District = new DistrictDto
                        {
                            Id = x.District.Id,
                            Name = x.District.Name,
                        },
                        LocationId = x.LocationId,
                        Location = new LocationDto
                        {
                            Id = x.Location.Id,
                            Name = x.Location.Name
                        },
                        IsActive = x.IsActive,
                        Cost = x.Cost,
                        Type = x.Type,
                    })
                    .ToList();

                return dataList;
            }
        }

        [HttpGet]
        [Route("GetRestaurantsByLocationId/{id}")]
        public List<RestaurantDto> GetRestaurantsByLocation(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Restaurants
                    .Where(x => x.IsActive == true && x.LocationId == id)
                    .Select(x => new RestaurantDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Photo = x.Photo,
                        MobileNo = x.MobileNo,
                        License = x.License,
                        IsApproved = x.IsApproved,
                        Email = x.Email,
                        Description = x.Description,
                        StateId = x.StateId,
                        State = new StateDto
                        {
                            Id = x.State.Id,
                            Name = x.State.Name,
                        },
                        DistrictId = x.DistrictId,
                        District = new DistrictDto
                        {
                            Id = x.District.Id,
                            Name = x.District.Name,
                        },
                        LocationId = x.LocationId,
                        Location = new LocationDto
                        {
                            Id = x.Location.Id,
                            Name = x.Location.Name
                        },
                        IsActive = x.IsActive,
                        Cost = x.Cost,
                        Type = x.Type,
                    })
                    .ToList();

                return dataList;
            }
        }

        [HttpGet]
        [Route("DeleteRestaurant/{id}")]
        public bool DeleteRestaurant(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Restaurants.FirstOrDefault(x => x.Id == id);
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
        [HttpPost]
        [Route("RejectRestaurant/{id}")]
        public bool RejectRestaurant(long id, string reason)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var restaurant = context.Restaurants.FirstOrDefault(x => x.Id == id);
                if (restaurant != null)
                {
                    restaurant.IsApproved = false;
                    restaurant.RejectionReason = reason;  // Set rejection reason

                    context.Entry(restaurant).Property(x => x.IsApproved).IsModified = true;
                    context.Entry(restaurant).Property(x => x.RejectionReason).IsModified = true;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        [HttpGet]
        [Route("ApproveRestaurant/{id}")]
        public bool ApproveRestaurant(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var restaurant = context.Restaurants.FirstOrDefault(x => x.Id == id);
                if (restaurant != null)
                {
                    restaurant.IsApproved = true;
                    restaurant.RejectionReason = null;  // Clear rejection reason if it's approved

                    context.Entry(restaurant).Property(x => x.IsApproved).IsModified = true;
                    context.Entry(restaurant).Property(x => x.RejectionReason).IsModified = true;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

    }
}
