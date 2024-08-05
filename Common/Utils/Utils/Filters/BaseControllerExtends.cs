using Utils.BaseController;

namespace Utils.Filters
{
    public static class BaseControllerExtends
    {
        public static ResponseOperation GetResponseOperation<T>(this StandardController<T> self)
        {
            var id = self.HttpContext.Request.Headers[ResponseOperationGlobal.NameHeaderOperationId];
            var name = self.HttpContext.Request.Headers[ResponseOperationGlobal.NameHeaderOperationName];

            return new ResponseOperation(Guid.Parse(id), name);
        }
    }
}
