using System.Net;
using Microsoft.Extensions.Logging;

namespace KkxWebServer.Handlers;

internal sealed class FinalHandler : KkxRequestHandler
{
    public override async Task HandleAsync()
    {
        if (Completed || Terminated)
        {
            return;
        }

        try
        {
            Context.Response.ContentType = MimeTypes.GetMimeType(KkxFileInfo.Route);
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            await using var binaryWriter = new BinaryWriter(Context.Response.OutputStream);
            var bytes = await File.ReadAllBytesAsync(KkxFileInfo.LocalPath);

            binaryWriter.Write(bytes);
            binaryWriter.Flush();

            Complete();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
        }
    }
}