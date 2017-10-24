namespace N5.Administration.Filters
{
    public class RangeDefinition
    {
        public int Id { get; set; }

        public int DefinedRangeFilterId { get; set; }
        public DefinedRangeFilter DefinedRangeFilter { get; set; }

        public string Name { get; set; }

        public double? Min { get; set; }
        public double? Max { get; set; }
    }
}