using N5.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace N5.Administration.Filters
{
    public class Filter
    {
        public int Id { get; set; }

        public long CustomerFieldId { get; set; }
        public CustomerField CustomerField { get; set; }

        public string Name { get; set; }
    }
}
