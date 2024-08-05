using System.Text;
using Microsoft.AspNetCore.Http;

namespace Utils.I18n;


public class ResponseUtil : IResponseUtil
{
    public async Task WriteAsync(HttpContext context, StringBuilder stringBuilder)
    {
        await context.Response.WriteAsync(stringBuilder.ToString());
    }
}