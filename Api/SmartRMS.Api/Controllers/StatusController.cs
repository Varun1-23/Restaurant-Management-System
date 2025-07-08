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
    public class StatusController : ApiController
    {
        [HttpPost]
        [Route("SaveStatus")]
        public bool SaveStatus(StatusDto dataDto)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                if (dataDto.Id > 0)
                {
                    var editData = context.Statuses.FirstOrDefault(x => x.Id == dataDto.Id);
                    if (editData != null)
                    {
                        editData.Name = dataDto.Name;
                        context.Entry(editData).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    Status addData = new Status();
                    addData.Name = dataDto.Name;
                    addData.IsActive = true;
                    context.Statuses.Add(addData);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        [HttpGet]
        [Route("GetStatuses")]
        public List<StatusDto> GetStatuses()
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var dataList = context.Statuses
                                       .Where(x => x.IsActive == true)
                                       .Select(x => new StatusDto
                                       {
                                           Id = x.Id,
                                           Name = x.Name,
                                           IsActive = x.IsActive
                                       })
                                       .ToList();
                return dataList;
            }
        }
        [HttpGet]
        [Route("DeleteStatus/{id}")]
        public bool DeleteStatus(long id)
        {
            using (SMARTRMSDB context = new SMARTRMSDB())
            {
                var deleteData = context.Statuses.FirstOrDefault(x => x.Id == id);
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
