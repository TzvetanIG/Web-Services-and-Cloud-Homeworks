namespace MusicSystemConsoleClient
{
    using System.Collections.Generic;
    using System.Net;
    using RestSharp;

    class Request : IRequestContentType, IRequestResult
    {
        private RestClient client;
        private Dictionary<string, string> headers;
        private object data;
        private Dictionary<string, object> urlSegments;
        private IRestRequest restRequest;
        private IRestResponse response;
 

        public Request(Method method, string resource, RestClient client)
        {
            this.client = client;
            this.restRequest = new RestRequest(resource, method);
            this.headers = new Dictionary<string, string>();
            this.data = null;
            this.urlSegments = new Dictionary<string, object>();
        }

        public Dictionary<string, string> Headers
        {
            get { return this.headers; }
            set { this.headers = value; }
        }

        public object Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public Dictionary<string, object> UrlSegments
        {
            get { return this.urlSegments; }
            set { this.urlSegments = value; }
        }

        public IRequestResult Xml
        {
            get
            {
                this.restRequest.AddHeader("Accept", "text/xml");
                this.ExecudeRequest();
                return this;
            }
        }

        public IRequestResult Json
        {
            get
            {
                this.restRequest.AddHeader("Accept", "application/json");
                this.ExecudeRequest();
                return this;
            }
        }

        public string Content 
        {
            get
            {
                 return response.Content;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return response.StatusCode;
            }
        }

        private void AttachHeaders()
        {
            foreach (var header in this.Headers)
            {
                this.restRequest.AddHeader(header.Key, header.Value);
            }
        }

        private void AttachData()
        {
            if (data != null)
            {
                this.restRequest.AddJsonBody(data);
            }
        }

        private void AttachUrlSegments()
        {
            foreach (var urlSegment in this.urlSegments)
            {
                this.restRequest.AddParameter(urlSegment.Key, urlSegment.Value);
            }
        }

        private void ExecudeRequest()
        {
            this.AttachHeaders();
            this.AttachUrlSegments();
            this.AttachData();
            this.response = this.client.Execute(this.restRequest);

        }
    }
}
