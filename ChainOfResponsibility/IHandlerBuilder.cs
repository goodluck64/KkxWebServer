namespace KkxWebServer.ChainOfResponsibility;

internal interface IHandlerBuilder
{
    IHandlerBuilder AddHandler(IHandler handler);
    IHandler Build();
}

internal interface IHandlerBuilder<THandler>
    where THandler : IHandler
{
    IHandlerBuilder<THandler> AddHandler(THandler handler);
    THandler Build();
}