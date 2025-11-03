using System.Net;
using KkxWebServer.ChainOfResponsibility;
using Microsoft.Extensions.Logging;

namespace KkxWebServer.Handlers;

internal abstract class KkxRequestHandler : Handler
{
    public HttpListenerContext Context
    {
        get => GetItem<HttpListenerContext>((int)Keys.Context);
        set => SetItem((int)Keys.Context, value);
    }

    public string Path
    {
        get => GetItem<string>((int)Keys.Path);
        set => SetItem((int)Keys.Path, value);
    }

    public KkxFileInfo KkxFileInfo
    {
        get => GetItem<KkxFileInfo>((int)Keys.FileInfo);
        set => SetItem((int)Keys.FileInfo, value);
    }

    public Dictionary<string, KkxFileInfo> Pages
    {
        get => GetItem<Dictionary<string, KkxFileInfo>>((int)Keys.Pages);
        set => SetItem((int)Keys.Pages, value);
    }

    public Dictionary<string, KkxFileInfo> Fragments
    {
        get => GetItem<Dictionary<string, KkxFileInfo>>((int)Keys.Fragments);
        set => SetItem((int)Keys.Fragments, value);
    }

    public ILogger<KkxServer> Logger
    {
        get => GetItem<ILogger<KkxServer>>((int)Keys.Logger);
        set => SetItem((int)Keys.Logger, value);
    }

    private bool _completed;
    private bool _terminated;

    protected void Complete(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        if (_terminated || _completed)
        {
            return;
        }

        Context.Response.StatusCode = (int)statusCode;
        Context.Response.Close();
        _completed = true;
    }

    protected void Terminate(HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        if (_terminated || _completed)
        {
            return;
        }

        Context.Response.StatusCode = (int)statusCode;
        Context.Response.Close();
        _terminated = true;
    }

    public bool Completed => _completed;
    public bool Terminated => _terminated;

    private enum Keys
    {
        Path = 42,
        FileInfo,
        Context,
        Pages,
        Fragments,
        Logger
    }
}