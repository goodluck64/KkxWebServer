using Microsoft.Extensions.Logging;

namespace KkxWebServer.Handlers;

internal sealed class PathNormalizerHandler : KkxRequestHandler
{
    public override async Task HandleAsync()
    {
        if (Completed || Terminated)
        {
            return;
        }

        if (!System.IO.Path.HasExtension(Path))
        {
            string? resultPath = null;

            bool resultSet = HasWithExtension(Path, ".html", out resultPath);

            if (!resultSet)
            {
                resultSet = HasWithExtension(Path, ".htm", out resultPath);
            }

            if (!resultSet)
            {
                Logger.LogWarning("PathNormalizerHandler::HandleAsync: Path not found");
                
                Terminate();

                return;
            }

            Path = resultPath;
        }

        await GoAsync();
    }

    private bool HasWithExtension(string path, string extension, out string changedPath)
    {
        changedPath = System.IO.Path.ChangeExtension(path, extension);

        return Pages.ContainsKey(changedPath) || Fragments.ContainsKey(changedPath);
    }
}