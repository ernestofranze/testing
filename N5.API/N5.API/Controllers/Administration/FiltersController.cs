using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N5.API.Business.Interfaces;
using N5.API.DataContracts;
using System.Collections.Generic;

namespace N5.API.Controllers.Administration
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/Administration/Filters")]
    public class FiltersController : Controller
    {
        private IAdministrationBusiness _administrationBusiness;

        public FiltersController(IAdministrationBusiness administrationBusiness)
        {
            _administrationBusiness = administrationBusiness;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _administrationBusiness.GetFilters();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] FilterDataContract data)
        {
            _administrationBusiness.CreateBooleanFilter(data.Name, data.CustomerFieldId);
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}