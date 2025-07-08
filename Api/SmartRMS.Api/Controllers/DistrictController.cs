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
    public class DistrictController : ApiController
    {

        [HttpPost]
        [Route("SaveDistrict")]
        public bool SaveDistrict(DistrictDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {

                    var editData = context.Districts.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {

                        editData.Name = dataDto.Name;
                        editData.StateId = dataDto.StateId;
                        context.Entry(editData).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }

                else
                {

                    District addData = new District();
                    addData.Name = dataDto.Name;
                    addData.IsActive = true;
                    addData.StateId = dataDto.StateId;
                    context.Districts.Add(addData);
                    context.SaveChanges();
                    return true;
                }
                return false;

            }
        }


        [HttpGet]
        [Route("GetDistricts")]
        public List<DistrictDto> GetDistricts()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Districts
                                      .Where(x => x.IsActive == true)
                                      .Select(x => new DistrictDto
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
                                          IsActive = x.IsActive,
                                          StateId = x.StateId,
                                          State = new StateDto
                                          {
                                              Id = x.State.Id,
                                              Name = x.State.Name,
                                          }
                                      })
                                      .ToList();
                return dataList;
            }
        }

        [HttpGet]
        [Route("GetDistrictsByState/{id}")]
        public List<DistrictDto> GetDistrictsByState(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var dataList = context.Districts.Where(x => x.IsActive == true && x.StateId == id)
                        .Select(x => new DistrictDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            StateId = x.StateId,
                            State = new StateDto
                            {
                                Id = x.State.Id,
                                Name = x.State.Name,
                            },
                            IsActive = x.IsActive,

                        })
                        .ToList();
                return dataList;
            }
        }


        [HttpGet]
        [Route("DeleteDistrict/{id}")]
        public bool DeleteDistrict(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {

                var deleteData = context.Districts.FirstOrDefault(x => x.Id == id);
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
