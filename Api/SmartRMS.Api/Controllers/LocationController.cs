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
    public class LocationController : ApiController
    {

        [HttpPost]
        [Route("SaveLocation")]
        public bool SaveLocation(LocationDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var editData = context.Locations.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {

                        editData.Name = dataDto.Name;
                        editData.StateId = dataDto.StateId;
                        editData.DistrictId = dataDto.DistrictId;
                        context.Entry(editData).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Location addData = new Location();
                    addData.Name = dataDto.Name;
                    addData.IsActive = true;
                    addData.StateId = dataDto.StateId;
                    addData.DistrictId = dataDto.DistrictId;
                    context.Locations.Add(addData);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }


        [HttpGet]
        [Route("GetLocations")]
        public List<LocationDto> GetLocations()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Locations
                                      .Where(x => x.IsActive == true)
                                      .Select(x => new LocationDto
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
                                          IsActive = x.IsActive,
                                          StateId = x.StateId,
                                          State = new StateDto
                                          {
                                              Id = x.State.Id,
                                              Name = x.State.Name,
                                          },
                                          DistrictId = x.DistrictId,
                                          District = new DistrictDto
                                          {
                                              Id= x.District.Id,
                                              Name= x.District.Name,
                                          }
                                      })
                                      .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("GetLocationsByDistId/{id}")]
        public List<LocationDto> GetLocationsByDistId(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Locations
                                      .Where(x => x.IsActive == true && x.DistrictId == id)
                                      .Select(x => new LocationDto
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
                                          IsActive = x.IsActive,
                                          StateId = x.StateId,
                                          State = new StateDto
                                          {
                                              Id = x.State.Id,
                                              Name = x.State.Name,
                                          },
                                          DistrictId = x.DistrictId,
                                          District = new DistrictDto
                                          {
                                              Id= x.District.Id,
                                              Name= x.District.Name,
                                          }
                                      })
                                      .ToList();
                return dataList;
            }
        }
        [HttpGet]
        [Route("DeleteLocation/{id}")]
        public bool DeleteLocation(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Locations.FirstOrDefault(x => x.Id == id);
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
