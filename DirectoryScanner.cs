using Microsoft.Extensions.Logging;

namespace KkxWebServer;

internal sealed class DirectoryScanner(ILogger<DirectoryScanner> logger) : IDirectoryScanner
{
    public IReadOnlyCollection<KkxFileInfo> ScanDirectory(string directory, params IEnumerable<string> extensions)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var scannedDirectories = new List<KkxFileInfo>();

        ScanDirectory(directory, directory, extensions.ToArray(), scannedDirectories);

        return scannedDirectories;
    }

    private void ScanDirectory(string initialDirectory, string directory, string[] extensions,
        List<KkxFileInfo> scannedDirectories)
    {
        try
        {
            var files = Directory.GetFiles(directory);
            var extensions2 = NormalizeExtensions(extensions);

            foreach (var file in files)
            {
                try
                {
                    var ext = Path.GetExtension(file);

                    if (extensions2.Contains(ext))
                    {
                        scannedDirectories.Add(new KkxFileInfo
                        {
                            InitialDirectory = initialDirectory,
                            Extension = ext,
                            Route = '/' + Path.GetRelativePath(initialDirectory, file).Replace('\\', '/')
                        });
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }

            var subDirectories = Directory.GetDirectories(directory);

            foreach (var subDirectory in subDirectories)
            {
                ScanDirectory(initialDirectory, subDirectory, extensions, scannedDirectories);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
        }
    }

    private ICollection<string> NormalizeExtensions(IEnumerable<string> extensions)
    {
        var result = new List<string>();

        foreach (var extension in extensions)
        {
            if (!extension.StartsWith('.'))
            {
                result.Add($".{extension}");

                continue;
            }

            result.Add(extension);
        }

        return result;
    }
}