using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N5.API.Business.Interfaces;

namespace N5.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private ICustomersBusiness _customersBusiness;

        public CustomersController(ICustomersBusiness customersBusiness)
        {
            _customersBusiness = customersBusiness;
        }

        [HttpGet("{id}")]
        public dynamic Get(string id)
        {
            return _customersBusiness.GetCustomer(id);
        }

        [HttpPost]
        public dynamic Search()
        {
            return _customersBusiness.Search(null);
        }
    }
}