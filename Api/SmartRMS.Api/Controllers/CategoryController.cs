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

namespace SmartRMS.api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {

        [HttpPost]
        [Route("SaveCategory")]
        public bool SaveCategory(CategoryDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var editData = context.Categories.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {

                        editData.Name = dataDto.Name; 
                        editData.RestaurantId = dataDto.RestaurantId;
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
                    Category addData = new Category();
                    addData.Name = dataDto.Name; 
                    addData.RestaurantId = dataDto.RestaurantId;
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

                    context.Categories.Add(addData);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        [HttpGet]
        [Route("GetCategories")]
        public List<CategoryDto> GetCategories()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Categories.Where(x => x.IsActive == true)
                        .Select(x => new CategoryDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Photo = x.Photo,
                            RestaurantId = x.RestaurantId,
                            Restaurant = new RestaurantDto
                            {
                                Id = x.Restaurant.Id,
                                Name = x.Restaurant.Name,
                            },
                            IsActive = x.IsActive,

                        })
                        .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("GetCategoriesByRestaurant/{id}")]
        public List<CategoryDto> GetCategoriesByRestaurant(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Categories.Where(x => x.IsActive == true && x.RestaurantId == id)
                        .Select(x => new CategoryDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Photo = x.Photo,
                            RestaurantId = x.RestaurantId,
                            Restaurant = new RestaurantDto
                            {
                                Id = x.Restaurant.Id,
                                Name = x.Restaurant.Name,
                            },
                            IsActive = x.IsActive,

                        })
                        .ToList();
                return dataList;
            }
        }


        [HttpGet]
        [Route("DeleteCategory/{id}")]
        public bool DeleteCategory(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Categories.FirstOrDefault(x => x.Id == id);
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
