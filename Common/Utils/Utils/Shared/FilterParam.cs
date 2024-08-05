namespace Utils.Shared
{
    public class FilterParam
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string SearchWord { get; set; }
        public string ColumnNameToBeOrdered { get; set; }
        public bool IsAscendingOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime? EffectiveAsOf { get; set; }
        public List<SearchColumn> Filter { get; set; }
        public List<SearchColumn> ExactFilter { get; set; }
        public DateTime? DateFilterValue { get; set; }
        public string DateFilterField { get; set; }
    }

    public class SearchColumn
    {
        public string ColumnName { get; set; }
        public string SearchValue { get; set; }
        public string PropertyType { get; set; }
    }
}
