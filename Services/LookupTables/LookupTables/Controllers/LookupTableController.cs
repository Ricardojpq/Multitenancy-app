using Asp.Versioning;
using LookupTables.Database.Persistence;
using LookupTables.Domain;
using Microsoft.AspNetCore.Mvc;
using Utils.Extensions;

namespace LookupTables.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LookupTableController : Controller
    {
        private readonly LookupTableDbContext _Context;
        public LookupTableController(LookupTableDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var gender = new Gender
            //{
            //    Name = "Femenino",
            //    Description = "Femenino",
            //    CreatedBy = "TEST"
            //};

            //await _Context.Gender.AddAsync(gender);

            var currentTenant = HttpContext.GetTenantId();
            var product = new Product
            {
                Name = "Producto 1",
                Description = "Producto 1",
                CreatedBy = "TEST",
                TenantId = Guid.Parse("df31e4b6-1cc8-44a4-99bc-d10d15cdc145")
            };
            await _Context.Product.AddAsync(product);
            await _Context.SaveChangesAsync();
            return Ok("TODO BIEN");
        }
    }
}
