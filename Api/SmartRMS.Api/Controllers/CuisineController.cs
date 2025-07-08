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
using System.Runtime.InteropServices.ComTypes;

namespace SmartRMS.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CuisineController : ApiController
    {
        [HttpPost]
        [Route("SaveCuisine")]
        public bool SaveCuisine(CuisineDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var editData = context.Cuisines.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {
                        editData.Name = dataDto.Name;
                        editData.NoOfPeople = dataDto.NoOfPeople;
                        editData.RestaurantId = dataDto.RestaurantId;
                        context.Entry(editData).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }

                else
                {
                    Cuisine addData = new Cuisine();
                    addData.Name = dataDto.Name;
                    addData.IsActive = true;
                    addData.NoOfPeople = dataDto.NoOfPeople;
                    addData.RestaurantId = dataDto.RestaurantId;
                    context.Cuisines.Add(addData);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        [HttpGet]
        [Route("GetCuisinesByRestId/{id}")]
        public List<CuisineDto> GetCuisinesByRestId(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Cuisines
                                      .Where(x => x.IsActive == true && x.RestaurantId == id)
                                      .Select(x => new CuisineDto
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
                                          IsActive = x.IsActive,
                                            NoOfPeople = x.NoOfPeople,
                                          RestaurantId = x.RestaurantId,
                                      })
                                      .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("DeleteCuisine/{id}")]
        public bool DeleteCuisine(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Cuisines.FirstOrDefault(x => x.Id == id);
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
