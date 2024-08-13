using LookupTables.Database.Persistence.SeedData.Models;
using LookupTables.Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;


namespace LookupTables.Database.Persistence.SeedData
{
    public class DbInitializer
    {

        public static async Task Initialize(LookupTableDbContext context, TenantDbContext tenantContext, IConfiguration configuration)
        {
            DataModelJson? data = null;
            List<Tenant> tenantData = new List<Tenant>();

            var currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\LookupTables.Database");

            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            var tenantId = Guid.Parse(configuration.GetValue<string>("TenantId")!);

            bool seedGeneralData = appSettings.SeedGeneralData;
            bool seedCountries = appSettings.SeedCountries;
            bool seedTenants = appSettings.SeedTenants;

            if (seedGeneralData)
            {
                var seedFileLocation = currentDirectory + "\\Persistence\\SeedData\\Data\\Data.json";
                data = JsonConvert.DeserializeObject<DataModelJson>(await File.ReadAllTextAsync(seedFileLocation));
            }

            if (seedCountries)
            {
                var seedFileLocation = currentDirectory + "\\Persistence\\SeedData\\Data\\DataPoliticalDivision_VEN.json";
            }

            if (seedTenants)
            {
                var seedFileLocation = currentDirectory + "\\Persistence\\SeedData\\Data\\TenantData.json";
                tenantData = JsonConvert.DeserializeObject<List<Tenant>>(await File.ReadAllTextAsync(seedFileLocation))!;
            }

            if (tenantData.Count != 0)
            {
                await SeedTenants(tenantContext, tenantData);

            }

            if (data != null)
            {
            }



        }

        #region SeedData

        private static async Task SeedTenants(TenantDbContext context, List<Tenant> tenants)
        {
            try
            {
                var newEntities = tenants.Where(x => !context.Tenants.Any(y => x._Id == y._Id)).ToList();
                if (newEntities.Any())
                {
                    await context.Tenants.AddRangeAsync(newEntities);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception("An error has occurred in the tenants' seed", ex);
            }
        }

        #endregion

    }
}
