namespace KkxWebServer;

internal static class KkxGlobals
{
    private static IServiceProvider? _services;

    public static IServiceProvider Services
    {
        get => _services ?? throw new InvalidOperationException();
        set => _services = value;
    }

    public const string FragmentsDirectoryPath = "Fragments";
    public const string PagesDirectoryPath = "Pages";

    /// <summary>
    /// List of all available types for server. Add more extensions if you need it.
    /// </summary>
    public static readonly IEnumerable<string> ExtensionsToScan =
        ["html", "htm", "js", "css", "png", "jpg", "jpeg", "webp", "mp4", "mp3"];

    /// <summary>
    /// Endpoint to attach to. Change to whatever you want.
    /// </summary>
    public const string Host = "http://localhost:6969/"; // <-- Do not remove trailing slash!

    /// <summary>
    /// Scans directories <see cref="FragmentsDirectoryPath"/> and <see cref="PagesDirectoryPath"/> before each request.
    /// Useful when you make changes frequently.
    /// </summary>
    public const bool EnableRescan = true;
}