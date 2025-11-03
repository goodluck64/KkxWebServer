using KkxWebServer.ChainOfResponsibility;
using KkxWebServer.Handlers;

namespace KkxWebServer;

internal sealed class KkxServerHandlerPack : IHandlerPack<KkxRequestHandler>
{
    public KkxRequestHandler Fold()
    {
        var builder = new HandlerBuilder<KkxRequestHandler>();

        builder.AddHandler(new UrlHandler());
        builder.AddHandler(new PathNormalizerHandler());
        builder.AddHandler(new RouteHandler());
        builder.AddHandler(new FinalHandler());

        return builder.Build();
    }
}