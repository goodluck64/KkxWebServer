using Microsoft.Extensions.Logging;

namespace KkxWebServer.Handlers;

internal sealed class RouteHandler : KkxRequestHandler
{
    public override async Task HandleAsync()
    {
        if (Pages.TryGetValue(Path, out var kkxFileInfo) || Fragments.TryGetValue(Path, out kkxFileInfo))
        {
            KkxFileInfo = kkxFileInfo;

            await GoAsync();

            return;
        }
        
        Logger.LogWarning("RouteHandler::HandleAsync: Specified route does not exist");

        Terminate();
    }
}