namespace MusicSystemConsoleClient.Data
{
    public abstract class BaseResourse : IResourse
    {
        protected IHttpRequester requester;

        protected BaseResourse()
        {
            this.requester = new HttpRequester("http://localhost:8591/api");
        }

        public string Resourse { get; set; }

        public IRequestContentType All
        {
            get { return requester.Get(Resourse); }
        }

        public IRequestContentType Find(int id)
        {
            return requester.Get(Resourse, id);
        }

        public IRequestContentType Add(object data)
        {
            return requester.Post(Resourse + "/add", null, data);
        }

        public IRequestContentType Edit(int id, object data)
        {
            return requester.Put(Resourse + "update", id, null, data);
        }

        public IRequestContentType Delete(int id)
        {
            return requester.Delete(Resourse + "delete", id);
        }

    }
}
