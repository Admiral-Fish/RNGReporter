using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Ionic.Zip;

namespace Updater
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 2 || !File.Exists(args[0]) ||
                !Uri.IsWellFormedUriString(args[1], UriKind.RelativeOrAbsolute)) return;
            var client = new WebClient();
            string temp = Path.GetTempFileName();
            client.DownloadFile(args[1], temp);
            string path = Path.GetDirectoryName(args[0]);
            using (var file = new ZipFile(temp))
            {
                file.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                file.ExtractAll(path);
            }
            File.Delete(temp);
            Process.Start(args[0]);
        }
    }
}