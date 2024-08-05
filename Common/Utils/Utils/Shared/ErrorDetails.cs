using Newtonsoft.Json;

namespace Utils.Shared
{
    public class ErrorDetails
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
