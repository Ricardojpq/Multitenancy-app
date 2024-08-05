using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.LookupTables
{
    public class LUTTASearchCriteria
    {
        public string TableName { get; set; }
        public string Service { get; set; }
        public string SearchInFields { get; set; }
        public string SearchValue { get; set; }
        public bool IncludeInactive { get; set; } = false;
        //public List<FilterOption> Filter { get; set; }        
        public Dictionary<string, string> Filter { get; set; }
    }
    public class FilterOption
    {
        public string Property { get; set; }
        public string Value { get; set; }
    }
}
