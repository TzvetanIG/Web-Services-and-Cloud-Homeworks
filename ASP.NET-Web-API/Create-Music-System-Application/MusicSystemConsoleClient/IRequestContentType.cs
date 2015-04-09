namespace MusicSystemConsoleClient
{
    using System.Net;

    public interface IRequestContentType
    {
        IRequestResult Xml { get; }
        IRequestResult Json { get; }
    }
}
