using System.Diagnostics.CodeAnalysis;

namespace KkxWebServer.ChainOfResponsibility;

internal abstract class Handler : IHandler
{
    private IDictionary<int, object>? _items;
    private IHandler? _next;

    public IHandler? Next
    {
        get => _next;
        set
        {
            _next = value;

            if (_next is not null)
            {
                _next.Items = Items;
            }
        }
    }

    public IDictionary<int, object> Items
    {
        get => _items ??= new Dictionary<int, object>();
        set => _items = value;
    }

    public abstract Task HandleAsync();

    protected Task GoAsync()
    {
        Next?.HandleAsync();

        return Task.CompletedTask;
    }

    public void SetItem<T>(int key, T value) where T : notnull => Items[key] = value;

    public bool TryGetItem<T>(int key, [NotNullWhen(true)] out T? item)
        where T : notnull
    {
        item = default;

        if (Items.TryGetValue(key, out var outItem))
        {
            item = (T)outItem;

            return true;
        }

        return false;
    }

    public T GetItem<T>(int key)
        where T : notnull
    {
        if (Items.TryGetValue(key, out var outItem))
        {
            return (T)outItem;
        }

        throw new KeyNotFoundException();
    }

    public bool HasItem(int key) => Items.ContainsKey(key);
}