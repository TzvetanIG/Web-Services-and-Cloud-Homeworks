namespace MusicSystemConsoleClient
{
    using System.Collections.Generic;
    using RestSharp;

    public class HttpRequester : IHttpRequester
    {
        private RestClient client;

        public HttpRequester(string baseUrl)
        {
            this.client = new RestClient(baseUrl);
        }

        public IRequestContentType MakeRequest(Method method, string resource, Dictionary<string, string> headers = null, object data = null)
        {
            var request = new Request(method, resource, this.client);
            if (headers != null)
            {
                request.Headers = headers;
            }

            if (data != null)
            {
                request.Data = data;
            }

            return request;
        }

        public IRequestContentType MakeRequest(Method method, string resource, int id,
            Dictionary<string, string> headers = null, object data = null)
        {
            var resourceWithId = resource + "/{id}";
            var request = (Request)MakeRequest(method, resource, headers, data);
            var urlSegments = new Dictionary<string, object> {{"id", id.ToString()}};
            request.UrlSegments = urlSegments;
            return request;
        }

        public IRequestContentType Get(string resource, int? id = null, Dictionary<string, string> headers = null, object data = null)
        {
            if (id != null)
            {
                return (Request) MakeRequest(Method.GET, resource, (int) id, headers, data);
            }

            return (Request)MakeRequest(Method.GET, resource, headers, data);
        }

        public IRequestContentType Post(string resource, Dictionary<string, string> headers = null, object data = null)
        {
            var request = (Request)MakeRequest(Method.POST, resource, headers, data);
            return request;
        }

        public IRequestContentType Put(string resource, int? id = null, Dictionary<string, string> headers = null, object data = null)
        {
            if (id != null)
            {
                return (Request)MakeRequest(Method.PUT, resource, (int)id, headers, data);
            }

            return (Request)MakeRequest(Method.GET, resource, headers, data);
        }

        public IRequestContentType Delete(string resource, int id, Dictionary<string, string> headers = null)
        {
            return (Request)MakeRequest(Method.DELETE, resource, id, headers);
        }
    }
}
