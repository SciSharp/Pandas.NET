using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PandasNet.Utils;

public class Web
{
    public static bool Download(string url, string destDir, string destFileName)
    {
        if (destFileName == null)
            destFileName = url.Split(Path.DirectorySeparatorChar).Last();

        Directory.CreateDirectory(destDir);

        string relativeFilePath = Path.Combine(destDir, destFileName);

        if (File.Exists(relativeFilePath))
        {
            Console.WriteLine($"{relativeFilePath} already exists.");
            return false;
        }

        var wc = new WebClient();
        Console.WriteLine($"Downloading from {url}");
        var download = Task.Run(() => wc.DownloadFile(url, relativeFilePath));
        while (!download.IsCompleted)
        {
            Thread.Sleep(1000);
            Console.WriteLine(".");
        }
        Console.WriteLine("");
        Console.WriteLine($"Downloaded to {relativeFilePath}");

        return true;
    }
}
