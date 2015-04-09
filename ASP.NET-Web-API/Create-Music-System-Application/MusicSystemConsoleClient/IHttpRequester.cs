namespace MusicSystemConsoleClient
{
    using System.Collections.Generic;

    public interface IHttpRequester
    {
        IRequestContentType Get(string resource, int? id = null, Dictionary<string, string> headers = null, object data = null);

        IRequestContentType Post(string resource, Dictionary<string, string> headers = null, object data = null);

        IRequestContentType Put(string resource, int? id = null, Dictionary<string, string> headers = null,
           object data = null);

        IRequestContentType Delete(string resource, int id, Dictionary<string, string> headers = null);
    }
}
