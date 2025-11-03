namespace KkxWebServer.ChainOfResponsibility;

internal interface IHandlerPack
{
    IHandler Fold();
}

internal interface IHandlerPack<out THandler>
    where THandler : IHandler
{
    THandler Fold();
}