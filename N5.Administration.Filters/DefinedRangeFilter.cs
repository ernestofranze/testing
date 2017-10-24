using System;
using System.Collections.Generic;
using System.Text;

namespace N5.Administration.Filters
{
    public class DefinedRangeFilter : Filter
    {
        public IEnumerable<RangeDefinition> RangeDefinitions { get; set; }
    }
}
