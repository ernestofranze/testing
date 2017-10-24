using System;
using System.Collections.Generic;
using System.Text;

namespace N5.DataAccess.Interfaces
{
    public interface IAdministrationDataAccess
    {
        string[] GetFilters();
        void CreateBooleanFilter(string name, long customerField);
    }
}
