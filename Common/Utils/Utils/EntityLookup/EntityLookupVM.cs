using Utils.Shared;

namespace Utils.EntityLookup
{
    public class EntityLookupVM
    {
        public DTParams dTParams { get; set; }
        public string entityName { get; set; }
        public bool includeInactive { get; set; } = false;
        public bool includeExpired { get; set; } = false;
        public string orderBy { get; set; }
        public bool orderAscending { get; set; } = true;

        public string typeSearch { get; set; } = Constants.UNIQUEFILTER;
        public string spropertyFields { get; set; }
        public List<string> propertyFields { get; set; }
        public string spropertyValues { get; set; }
        public List<string> propertyValues { get; set; }
        public string spropertyTypes { get; set; }
        public List<string> propertyTypes { get; set; }
        public string searchWord { get; set; }
        public List<string> entityRelations { get; set; }
        public DateTime? effectiveAsOf { get; set; }
        public List<string> propertyDatatableFields { get; set; }
        //added two new properties for ajax data filtering
        public List<string> ajaxFields { get; set; }
        public List<string> ajaxValues { get; set; }

        public bool isActive { get; set; } = true;


        public string Domain { get; set; }

        public DateTime? dateFilterValue { get; set; }
        public string dateFilterField { get; set; }

        public EntityLookupVM()
        {
            dTParams = new DTParams();
            propertyFields = new List<string>();
            propertyDatatableFields = new List<string>();
            propertyValues = new List<string>();
            propertyTypes = new List<string>();
            entityRelations = new List<string>();
            ajaxFields = new List<string>();
            ajaxValues = new List<string>();
        }
    }
}
