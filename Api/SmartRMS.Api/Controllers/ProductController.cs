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
    public class ProductController : ApiController
    {

        [HttpPost]
        [Route("SaveProduct")]
        public bool SaveProduct(ProductDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var editData = context.Products.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {

                        editData.Name = dataDto.Name; 
                        editData.RestaurantId = dataDto.RestaurantId;
                        editData.CategoryId = dataDto.CategoryId;
                        editData.Description = dataDto.Description;
                        editData.Price = dataDto.Price;
                        editData.OfferPrice = dataDto.OfferPrice;
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

                    Product addData = new Product();

                    addData.Name = dataDto.Name; 
                    addData.RestaurantId = dataDto.RestaurantId;
                    addData.CategoryId = dataDto.CategoryId;
                    addData.Description = dataDto.Description;
                    addData.Price = dataDto.Price;
                    addData.OfferPrice = dataDto.OfferPrice;
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

                    context.Products.Add(addData);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }


        [HttpGet]
        [Route("GetProducts")]
        public List<ProductDto> GetProducts()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Products
                                      .Where(x => x.IsActive == true)
                                      .Select(x => new ProductDto
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
                                          CategoryId = x.CategoryId,
                                          Category = new CategoryDto
                                          {
                                              Id = x.Category.Id,
                                              Name = x.Category.Name,
                                          },
                                          Description = x.Description,
                                          Price = x.Price,
                                          OfferPrice = x.OfferPrice,
                                          IsActive = x.IsActive,

                                      })
                                      .ToList();
                return dataList;
            }
        }
        

        [HttpGet]
        [Route("GetProductsByCategory/{id}")]
        public List<ProductDto> GetProductsByCategory(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Products.Where(x => x.IsActive == true && x.CategoryId == id)
                    .Select(x => new ProductDto
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
                        CategoryId = x.CategoryId,
                        Category = new CategoryDto
                        {
                            Id = x.Category.Id,
                            Name = x.Category.Name,
                        },
                        Description = x.Description,
                        Price = x.Price,
                        OfferPrice = x.OfferPrice,
                        IsActive = x.IsActive,

                    })
                    .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("GetProductsByRestaurant/{id}")]
        public List<ProductDto> GetProductsByRestaurant(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Products.Where(x => x.IsActive == true && x.RestaurantId == id)
                        .Select(x => new ProductDto
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
                            CategoryId = x.CategoryId,
                            Category = new CategoryDto
                            {
                                Id = x.Category.Id,
                                Name = x.Category.Name,
                            },
                            Description = x.Description,
                            Price = x.Price,
                            OfferPrice = x.OfferPrice,
                            IsActive = x.IsActive,

                        })
                        .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("DeleteProduct/{id}")]
        public bool DeleteProduct(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Products.FirstOrDefault(x => x.Id == id);
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
