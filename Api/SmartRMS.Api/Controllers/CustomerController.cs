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
using System.Text;
using SmartRMS.Api.Dtos;
using System.Net.Mail;

namespace SmartRMS.api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        [HttpPost]
        [Route("checkEmail")]
        public long checkEmail(CustomerDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var old = context.Customers.FirstOrDefault(x => x.IsActive && x.Email == dataDto.Email);
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
        [Route("SaveCustomer")]
        public int SaveCustomer(CustomerDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var old = context.Customers.FirstOrDefault(x => x.Id != dataDto.Id & x.Email == dataDto.Email);
                    if(old != null)
                    {
                        return 2;
                    }

                    var editData = context.Customers.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {


                        editData.Name = dataDto.Name; 
                        editData.MobileNo = dataDto.MobileNo;
                        editData.Email = dataDto.Email;
                        editData.LocationId = dataDto.LocationId;                                             
                        editData.StateId = dataDto.StateId;
                        editData.DistrictId = dataDto.DistrictId;
                        editData.Address = dataDto.Address;
                        editData.IsActive = dataDto.IsActive;

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

                        context.Entry(editData).State = EntityState.Modified;
                        context.SaveChanges();
                        return 1;
                    }
                }
                else
                {
                    var old = context.Customers.FirstOrDefault(x => x.Email == dataDto.Email);
                    if (old != null)
                    {
                        return 2;
                    }

                    Customer addData = new Customer();

                    addData.Name = dataDto.Name; 
                    addData.MobileNo = dataDto.MobileNo;
                    addData.Email = dataDto.Email;                  
                    addData.LocationId = dataDto.LocationId;
                    addData.StateId = dataDto.StateId;
                    addData.DistrictId = dataDto.DistrictId;
                    addData.Address = dataDto.Address;
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

                    context.Customers.Add(addData);
                    context.SaveChanges();

                    CustomerAddress add = new CustomerAddress();

                    add.Address1 = dataDto.Address;
                    add.StateId = addData.StateId;
                    add.DistrictId = addData.DistrictId;
                    add.LocationId = addData.LocationId;
                    add.CustomerId = addData.Id;

                    context.CustomerAddress.Add(add);
                    context.SaveChanges();

                    User user = new User();

                    user.UserName = dataDto.Email;
                    user.CustomerId = addData.Id;
                    user.Role = "Customer";
                    user.IsActive = true;

                    var passwordSalt = AuthenticationBL.CreatePasswordSalt(Encoding.ASCII.GetBytes(dataDto.Password));
                    user.PasswordSalt = Convert.ToBase64String(passwordSalt);
                    var password = AuthenticationBL.CreateSaltedPassword(passwordSalt, Encoding.ASCII.GetBytes(dataDto.Password));
                    user.Password = Convert.ToBase64String(password);

                    context.Users.Add(user);
                    context.SaveChanges();

                    return 1;
                }
                return 0;
            }
        }
        

        [HttpPost]
        [Route("SaveAddress")]
        public int SaveAddress(CustomerAddressDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var editData = context.CustomerAddress.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {


                        editData.Address1 = dataDto.Address1; 
                        editData.Address2 = dataDto.Address2; 
                        editData.Pincode = dataDto.Pincode; 
                        editData.LocationId = dataDto.LocationId; 
                        editData.StateId = dataDto.StateId; 
                        editData.DistrictId = dataDto.DistrictId; 
                      
                        context.Entry(editData).State = EntityState.Modified;
                        context.SaveChanges();
                        return 1;
                    }
                }
                else
                {
                    CustomerAddress addData = new CustomerAddress();

                    addData.Address1 = dataDto.Address1;
                    addData.Address2 = dataDto.Address2;
                    addData.Pincode = dataDto.Pincode;
                    addData.LocationId = dataDto.LocationId;
                    addData.StateId = dataDto.StateId;
                    addData.DistrictId = dataDto.DistrictId;
                    addData.CustomerId = dataDto.CustomerId;
                     
                    context.CustomerAddress.Add(addData);
                    context.SaveChanges();

                    return 1;
                }
                return 0;
            }
        }

        [HttpGet]
        [Route("GetCustomers")]
        public List<CustomerDto> GetCustomers()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Customers
                                      .Where(x => x.IsActive == true)
                                      .Select(x => new CustomerDto
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
                                          
                                          Photo = x.Photo,
                                          MobileNo = x.MobileNo,
                                          Email = x.Email,
                                          LocationId = x.LocationId,
                                          Location = new LocationDto
                                          {
                                              Id = x.Location.Id,
                                              Name = x.Location.Name,
                                          },
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
                                          
                                          Address = x.Address,
                                          IsActive = x.IsActive,

                                      })
                                      .ToList();
                return dataList;
            }
        }
        
        [HttpGet]
        [Route("GetCustomerById/{id}")]
        public CustomerDto GetCustomerById(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Customers
                                      .Where(x => x.Id == id)
                                      .Select(x => new CustomerDto
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
                                          
                                          Photo = x.Photo,
                                          MobileNo = x.MobileNo,
                                          Email = x.Email,
                                          LocationId = x.LocationId,
                                          Location = new LocationDto
                                          {
                                              Id = x.Location.Id,
                                              Name = x.Location.Name,
                                          },
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
                                          
                                          Address = x.Address,
                                          IsActive = x.IsActive,

                                      })
                                      .ToList().FirstOrDefault();
                return dataList;
            }
        }
        
        [HttpGet]
        [Route("GetCustomersAddress/{id}")]
        public List<CustomerAddressDto> GetCustomersAddress(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.CustomerAddress
                                      .Where(x => x.CustomerId == id)
                                      .Select(x => new CustomerAddressDto
                                      {
                                          Id = x.Id,
                                          Address1 = x.Address1,
                                          Address2 = x.Address2,
                                          Pincode = x.Pincode,
                                          LocationId = x.LocationId,
                                          Location = new LocationDto
                                          {
                                              Id = x.Location.Id,
                                              Name = x.Location.Name,
                                          },
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

                                      })
                                      .ToList();
                return dataList;
            }
        }


        [HttpGet]
        [Route("DeleteCustomer/{id}")]
        public bool DeleteCustomer(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Customers.FirstOrDefault(x => x.Id == id);
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
