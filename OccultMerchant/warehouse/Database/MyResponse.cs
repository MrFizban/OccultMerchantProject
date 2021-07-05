using System.Net;
using System.Net.Http;

namespace warehouse.Database
{
    public class MyResponse: HttpResponseMessage
    {
        private long id { get; set; }

        public MyResponse(HttpStatusCode statusCode, long id) : base(statusCode)
        {
            this.id = id;
        }
    }
}