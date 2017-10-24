using System;
using System.Collections.Generic;
using System.Text;

namespace N5.API.Business.Interfaces
{
    public interface IAdministrationBusiness
    {
        string[] GetFilters();
        void CreateBooleanFilter(string name, long customerField);
    }
}
