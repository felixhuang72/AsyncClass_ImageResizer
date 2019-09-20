using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            #region Sync (Original)

            imageProcess.Clean(destinationPath);

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            sw1.Stop();

            #endregion

            #region Async

            imageProcess.Clean(destinationPath);
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
            sw2.Stop();

            #endregion

            Console.WriteLine($"原始的同步方法花費時間: {sw1.ElapsedMilliseconds} ms");
            Console.WriteLine($"非同步方法花費時間: {sw2.ElapsedMilliseconds} ms");
            Console.WriteLine($"效能提升 {(((sw1.ElapsedMilliseconds - sw2.ElapsedMilliseconds) / (double)sw1.ElapsedMilliseconds) * 100).ToString("N")}% ");
        }
    }
}
