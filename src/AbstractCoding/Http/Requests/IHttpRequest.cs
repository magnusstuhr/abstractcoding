using System.Net.Http;
using System.Threading.Tasks;

namespace AbstractCoding.Http.Requests
{
    public interface IHttpRequest
    {
        public Task<HttpResponseMessage> Execute();
    }
}
