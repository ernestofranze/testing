using N5.API.Business.Interfaces;
using N5.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace N5.API.Business.Implementations
{
    public class AdministrationBusiness : IAdministrationBusiness
    {
        private IAdministrationDataAccess _administrationDataAccess;

        public AdministrationBusiness(IAdministrationDataAccess administrationDataAccess)
        {
            _administrationDataAccess = administrationDataAccess;
        }

        public string[] GetFilters()
        {
            return _administrationDataAccess.GetFilters();
        }

        public void CreateBooleanFilter(string name, long customerField)
        {
            _administrationDataAccess.CreateBooleanFilter(name, customerField);
        }
    }
}
