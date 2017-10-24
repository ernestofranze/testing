using N5.API.Business.DTO;
using System.Collections.Generic;

namespace N5.API.Business.Interfaces
{
    public interface ICustomersBusiness
    {
        dynamic GetCustomer(string id);
        dynamic Search(IEnumerable<FilterViewModel> param);
    }
}
