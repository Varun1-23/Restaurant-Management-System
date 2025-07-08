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

namespace SmartRMS.api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {

        [HttpPost]
        [Route("SaveEmployee")]
        public bool SaveEmployee(EmployeeDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var editData = context.Employees.FirstOrDefault(x => x.Id == dataDto.Id);
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
                        return true;
                    }
                }
                else
                {
                    Employee addData = new Employee();
                    addData.Name = dataDto.Name;
                    addData.MobileNo = dataDto.MobileNo;
                    addData.Email = dataDto.Email;
                    addData.Designation = dataDto.Designation;
                    addData.RestaurantId = dataDto.RestaurantId;
                    addData.LocationId = dataDto.LocationId;
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

                    context.Employees.Add(addData);
                    context.SaveChanges();

                    User user = new User();

                    user.UserName = dataDto.Email;
                    user.DriverId = addData.Id;
                    user.Role = "Employee";
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
        [Route("GetEmployees")]
        public List<EmployeeDto> GetEmployees()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Employees
                                      .Where(x => x.IsActive == true)
                                      .Select(x => new EmployeeDto
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

                                      })
                                      .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("GetEmployeesbyRestId/{id}")]
        public List<EmployeeDto> GetEmployeesbyRestId(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Employees
                                      .Where(x => x.IsActive == true && x.RestaurantId == id)
                                      .Select(x => new EmployeeDto
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

                                      })
                                      .ToList();
                return dataList;
            }
        }


        [HttpGet]
        [Route("DeleteEmployee/{id}")]
        public bool DeleteEmployee(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var deleteData = context.Employees.FirstOrDefault(x => x.Id == id);
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
