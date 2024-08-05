using System.Text;
using Microsoft.AspNetCore.Http;

namespace Utils.I18n;

public interface IResponseUtil
{
    public Task WriteAsync(HttpContext context, StringBuilder stringBuilder);
}