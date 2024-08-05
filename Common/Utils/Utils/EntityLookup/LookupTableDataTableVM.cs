namespace Utils.EntityLookup
{
    public class LookupTableDataTableVM
    {
        public DTParams DTParams { get; set; }
        public Guid LookupTableId { get; set; }
        public bool IncludeInactive { get; set; } = true;
        public LookupTableDataTableVM(Guid lookuptableid, DTParams dtParams)
        {
            LookupTableId = lookuptableid;
            DTParams = dtParams;
        }
    }
}
