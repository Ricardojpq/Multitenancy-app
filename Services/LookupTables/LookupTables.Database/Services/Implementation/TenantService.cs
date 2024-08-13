using LookupTables.Database.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupTables.Database.Services.Implementation
{
    public class TenantService : ITenantService
    {
        private readonly HttpContext? _httpContext;

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public string GetTenantId()
        {
            if (_httpContext is null)
            {
                Log.Debug("TenantId not found in token");
                throw new Exception("TenantId not found in token");
            }

            var headerAuth = _httpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(headerAuth))
            {
                Log.Error("Authorization Token Not Received");
                return "";
            }

            var token = headerAuth.First().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tenantId = jwtSecurityToken.Claims.First(claim => claim.Type == "tenantId").Value;
            if (string.IsNullOrWhiteSpace(tenantId))
            {
                Log.Debug("TenantId not found in token");
                throw new Exception("TenantId not found in token");
            }
            return tenantId;
        }
    }
}
