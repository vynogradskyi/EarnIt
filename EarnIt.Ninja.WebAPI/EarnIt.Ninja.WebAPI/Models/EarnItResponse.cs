using Nancy;

namespace EarnIt.Ninja.WebAPI.Models
{
    public class EarnItResponse
    {
        public HttpStatusCode HttpStatus { get; set; }
        public EarnItStatusCode Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}