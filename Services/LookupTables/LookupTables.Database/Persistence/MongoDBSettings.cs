using LookupTables.Database.Persistence.Interfaces;

namespace LookupTables.Database.Persistence
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}
