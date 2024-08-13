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
        private readonly LookupTableDbContext _context;

        public LookupTableController(LookupTableDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var test = HttpContext.GetTenantId();
            return Ok("TODO BIEN");
        }
    }
}
