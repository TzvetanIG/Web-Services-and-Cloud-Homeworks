namespace MusicSystemConsoleClient.Data
{
    public interface IResourse
    {
        IRequestContentType All { get; }

        IRequestContentType Find(int id);

        IRequestContentType Add(object data);

        IRequestContentType Edit(int id, object data);

        IRequestContentType Delete(int id);
    }
}
