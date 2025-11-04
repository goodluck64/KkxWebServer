using Microsoft.Extensions.Logging;

namespace KkxWebServer.Handlers;

internal sealed class PrimaryHandler : KkxRequestHandler
{
    public override async Task HandleAsync()
    {
        if (Context.Request.Url is null)
        {
            Logger.LogWarning("UrlHandler::HandleAsync: Invalid URL");

            Terminate();

            return;
        }

        if (Context.Request.HttpMethod != HttpMethod.Get.Method)
        {
            Logger.LogWarning("UrlHandler::HandleAsync: Only GET methods are supported");

            Terminate();

            return;
        }


        Path = Context.Request.Url.AbsolutePath;

        Logger.LogInformation("UrlHandler::HandleAsync: Incoming request: {0}", Path);

        await GoAsync();
    }
}