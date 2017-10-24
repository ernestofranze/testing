using N5.DataAccess.DTO;
using System.Collections.Generic;

namespace N5.DataAccess.Interfaces
{
    public interface ICustomersDataAccess
    {
        T Get<T>(string id) where T : class;
        IEnumerable<T> Search<T>(Query filter) where T : class;
        void Put<T>(T document) where T : class; 
    }
}
