using N5.Administration.Filters;
using N5.DataAccess.Interfaces;
using N5.Core.Repository;
using System.Linq;

namespace N5.DataAccess.Implementations
{
    public class AdministrationDataAccess : IAdministrationDataAccess
    {
        private IRepository<Filter, EntityFrameworkFilterContext> _filtersRepository;

        public AdministrationDataAccess(IRepository<Filter, EntityFrameworkFilterContext> filtersRepository)
        {
            _filtersRepository = filtersRepository;
        }

        /// <summary>
        /// For PoC test only 
        /// </summary>
        /// <returns></returns>
        public string[] GetFilters()
        {
            return _filtersRepository.List().Select(f => f.Name).ToArray();
        }

        public void CreateBooleanFilter(string name, long customerField)
        {
            _filtersRepository.Add(new BooleanFilter() { Name = name, CustomerFieldId = customerField });
        }
    }
}
