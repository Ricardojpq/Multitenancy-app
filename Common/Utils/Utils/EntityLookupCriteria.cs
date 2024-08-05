namespace Utils
{
    public class EntityLookupCriteria
    {
        public string TableName { get; set; }
        public int RecordId { get; set; }
        public IEnumerable<string> EntityDetailsFields { get; set; }
        public IEnumerable<string> EntityLookupRelations { get; set; }
    }
}
