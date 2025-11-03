namespace KkxWebServer.ChainOfResponsibility;

internal sealed class HandlerBuilder : IHandlerBuilder
{
    private IHandler? _parent;
    private IHandler? _current;

    public IHandlerBuilder AddHandler(IHandler handler)
    {
        if (_parent is null)
        {
            _parent = _current = handler;

            return this;
        }

        _current!.Next = handler;
        _current = handler;

        return this;
    }

    public IHandler Build()
    {
        if (_parent is null)
        {
            // TODO: Add exception message!
            throw new HandlerBuilderException();
        }

        return _parent;
    }
}

internal sealed class HandlerBuilder<THandler> : IHandlerBuilder<THandler>
    where THandler : IHandler
{
    private THandler? _parent;
    private THandler? _current;

    public IHandlerBuilder<THandler> AddHandler(THandler handler)
    {
        if (_parent is null)
        {
            _parent = _current = handler;

            return this;
        }

        _current!.Next = handler;
        _current = handler;

        return this;
    }

    public THandler Build()
    {
        if (_parent is null)
        {
            // TODO: Add exception message!
            throw new HandlerBuilderException();
        }

        return _parent;
    }
}