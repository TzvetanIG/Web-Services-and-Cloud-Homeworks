namespace MusicSystemConsoleClient
{
    using System.Net;

    public interface IRequestResult
    {
        string Content { get; }
        HttpStatusCode StatusCode { get; }
    }
}
