using KkxWebServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

{
    var serviceCollection = new ServiceCollection();

    serviceCollection.AddSingleton<IDirectoryScanner, DirectoryScanner>();
    serviceCollection.AddSingleton<KkxServer>();
    serviceCollection.AddLogging(x => { x.AddConsole(); });

    KkxGlobals.Services = serviceCollection.BuildServiceProvider();
}

MimeTypes.FallbackMimeType = "text/plain";

using var server = KkxGlobals.Services.GetRequiredService<KkxServer>();

await server.Listen(); // Runs infinitely