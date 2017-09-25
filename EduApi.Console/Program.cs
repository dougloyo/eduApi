using System;
using System.Configuration;
using System.Diagnostics;
using EduApi.Web;
using Microsoft.Owin.Hosting;

namespace EduApi.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = ConfigurationManager.AppSettings["BaseAddress"];

            using (WebApp.Start<Startup>(baseAddress))
            {
                Process.Start(baseAddress);
                System.Console.WriteLine($"Started on {baseAddress}...");
                System.Console.ReadKey();
            }
        }
    }
}
