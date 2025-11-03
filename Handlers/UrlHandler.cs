using Microsoft.Extensions.Logging;

namespace KkxWebServer.Handlers;

internal sealed class UrlHandler : KkxRequestHandler
{
    public override async Task HandleAsync()
    {
        if (Context.Request.Url is null)
        {
            Logger.LogWarning("UrlHandler::HandleAsync: Invalid URL");

            Terminate();

            return;
        }


        Path = Context.Request.Url.AbsolutePath;

        Logger.LogInformation("UrlHandler::HandleAsync: Incoming request: {0}", Path);

        await GoAsync();
    }
}