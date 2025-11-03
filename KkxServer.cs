using System.Net;
using Microsoft.Extensions.Logging;

namespace KkxWebServer;

internal sealed class KkxServer
    : IDisposable
{
    private readonly IDirectoryScanner _directoryScanner;
    private readonly ILogger<KkxServer> _logger;
    private readonly HttpListener _listener = new();
    private CancellationTokenSource _cts = new();
    private TaskCompletionSource _tcs = new();

    // These dictionaries contain route as a key and path as a value 
    private readonly Dictionary<string, KkxFileInfo> _pages = [];
    private readonly Dictionary<string, KkxFileInfo> _fragments = [];

    public KkxServer(IDirectoryScanner directoryScanner, ILogger<KkxServer> logger)
    {
        _directoryScanner = directoryScanner;
        _logger = logger;

        Scan();
    }

    private void Scan()
    {
        _pages.Clear();
        _fragments.Clear();

        foreach (var kkxDirectory in _directoryScanner.ScanDirectory(KkxGlobals.PagesDirectoryPath,
                     KkxGlobals.ExtensionsToScan))
        {
            _pages.Add(kkxDirectory.Route, kkxDirectory);
        }

        foreach (var kkxDirectory in _directoryScanner.ScanDirectory(KkxGlobals.FragmentDirectoryPath,
                     KkxGlobals.ExtensionsToScan))
        {
            _fragments.Add(kkxDirectory.Route, kkxDirectory);
        }

        _logger.LogInformation("KkxServer::Scan: Scanned");
    }

    public Task Listen()
    {
        _listener.Prefixes.Add(KkxGlobals.Host);
        _listener.Start();

        _ = Task.Run(ListenHandler);

        return _tcs.Task;
    }

    private async Task ListenHandler()
    {
        try
        {
            _logger.LogInformation("KkxServer::Listen: Listening");

            while (!_cts.IsCancellationRequested)
            {
                var context = await _listener.GetContextAsync();

                _ = Task.Run(() => RequestHandler(context));
            }
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    private void Stop()
    {
        _cts.Cancel();
        ((IDisposable)_listener).Dispose();
        _cts.Dispose();
        _tcs.SetResult();
    }

    private async Task RequestHandler(HttpListenerContext context)
    {
        _logger.LogInformation("KkxServer::RequestHandler: New request");

        if (KkxGlobals.EnableRescan)
        {
            Scan();
        }

        var pack = new KkxServerHandlerPack();
        var handler = pack.Fold();

        handler.Context = context;
        handler.Pages = _pages;
        handler.Fragments = _fragments;
        handler.Logger = _logger;

        await handler.HandleAsync();
    }

    public void Dispose()
    {
        Stop();
        _cts.Dispose();

        _logger.LogInformation("KkxServer::Dispose: KkxServer Disposed");
    }
}