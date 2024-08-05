using System.Collections.Generic;
using System.Linq;

namespace ViewModels.LookupTables
{
    public class LookupTablePanelVM
    {
        public string ActiveCategoryName { get; set; }
        public int ActiveTableId { get; set; }
        public IEnumerable<IGrouping<string, LookupTablePanelElementVM>> GroupedLookupTablePanelElements { get; set; }
        public LookupTablePanelVM(List<LookupTablePanelElementVM> lookupTablePanelElements, int id)
        {
            GroupedLookupTablePanelElements = lookupTablePanelElements.GroupBy(x => x.CategoryName);
            ActiveTableId = id;
            ActiveCategoryName = GetActiveCategoryId(id, GroupedLookupTablePanelElements);

            string GetActiveCategoryId(int ActiveTableId, IEnumerable<IGrouping<string, LookupTablePanelElementVM>> groupedPanelElements)
            {
                string ActiveCategoryName = string.Empty;
                
                foreach (var group in groupedPanelElements)
                {
                    foreach (var element in group)
                    {
                        if (element.Id == ActiveTableId)
                            ActiveCategoryName = element.CategoryName;                                                    
                    }
                    if (ActiveCategoryName != string.Empty)
                        break;
                }
                return ActiveCategoryName;
            }

        }
    }
    public class LookupTablePanelElementVM
    {
        public int CategoryId { get; set; }
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public int TotalRecords { get; set; }
    }
}
