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

        public LookupTableController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("TODO BIEN");
        }
    }
}
