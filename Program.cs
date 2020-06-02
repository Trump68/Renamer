using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Renamer
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = Environment.GetCommandLineArgs();
            if (arguments.Length < 2) return;
            string appdir = Path.GetDirectoryName(arguments[0]);
            string todir = Path.GetDirectoryName(arguments[1]);
            int next = GetMaxFile(todir,"*.jpg");
            int nextPng = GetMaxFile(todir, "*.png");
            if (nextPng > next) next = nextPng;
            while (true)
            {
                Thread.Sleep(1000);
                var files = Directory.GetFiles(appdir, "*.jpg").ToList();
                if (files.Any())
                {
                    while (File.Exists(Path.Combine(todir, $"{next}.jpg")))
                        next++;
                    string f = Path.Combine(todir, $"{next}.jpg");
                    Console.WriteLine($"{files[0]}->{f}");
                    File.Move(files[0], f);
                }
                files = Directory.GetFiles(appdir, "*.png").ToList();
                if (files.Any())
                {
                    while (File.Exists(Path.Combine(todir, $"{next}.png")))
                        next++;
                    string f = Path.Combine(todir, $"{next}.png");
                    Console.WriteLine($"{files[0]}->{f}");
                    File.Move(files[0], f);
                }
            }
        }

        private static int GetMaxFile(string todir, string ext)
        {
            var files = Directory.GetFiles(todir, ext);
            int next = 0;
            foreach (var file in files)
            {
                var fn = Path.GetFileNameWithoutExtension(file);
                int rez;
                if (int.TryParse(fn, out rez))
                {
                    if (rez > next)
                        next = rez;
                }
            }
            return next;
        }
    }
}
