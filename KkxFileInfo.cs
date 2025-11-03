namespace KkxWebServer;

internal sealed class KkxFileInfo
{
    public required string InitialDirectory { get; init; }
    public required string Route { get; init; }
    public required string Extension { get; init; }

    public string LocalPath => InitialDirectory + Route;
}