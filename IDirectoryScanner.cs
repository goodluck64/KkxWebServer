namespace KkxWebServer;

internal interface IDirectoryScanner
{
    IReadOnlyCollection<KkxFileInfo> ScanDirectory(string directory, params IEnumerable<string> extensions);
}