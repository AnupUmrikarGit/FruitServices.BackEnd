using System.Net;

namespace FruitServices.Models
{
    public class ApiResponse
    {
        public HttpStatusCode ResponseCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public object Data { get; set; }

        public ApiResponse()
        {
            
        }

        public ApiResponse(HttpStatusCode responseCode, string message, List<string> errors, object data)
        {
            this.ResponseCode = responseCode;
            this.Message = message;
            this.Errors = errors;
            this.Data = data;
        }

        public ApiResponse(HttpStatusCode responseCode, string message)
        {
            this.ResponseCode = responseCode;
            this.Message = message;
            this.Errors = null;
            //this.Data = null;
        }
    }

}