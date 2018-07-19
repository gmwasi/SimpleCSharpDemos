using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            Run("Msmg.exe", "");
        }

        public static string Run(string fileName, string args)
        {
            string returnvalue = string.Empty;

            ProcessStartInfo info = new ProcessStartInfo(fileName);
            info.UseShellExecute = false;
            info.Arguments = args;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.CreateNoWindow = true;

            using (Process process = Process.Start(info))
            {
                StreamReader sr = process.StandardOutput;
                returnvalue = sr.ReadToEnd();
            }

            return returnvalue;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}