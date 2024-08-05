namespace Utils.IntegrationEvents.Events
{
    public class LookupTableUpdatedEvent
    {
        public string TableName { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public LookupTableUpdatedEvent(int Id, string TableName, string Name, string Description)
        {
            this.Id = Id;
            this.TableName = TableName;
            this.Name = Name;
            this.Description = Description;
        }
            
    }
}
