using System;

using Common;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Server {
    class Program {
        static void Main(string[] args) {
            try {
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception err) {
                Console.WriteLine(err.Message);
                Console.Read();
            }
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) {
            if(Int32.TryParse(AppMasterConfig.MASTER_PORT, out int port)) {

                //string configPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "appsettings.json");

                var configuration = new ConfigurationBuilder()
                                    .AddEnvironmentVariables()
                                    .AddCommandLine(args)
                                    .AddJsonFile("appsettings.json")
                                    .Build();

                return WebHost.CreateDefaultBuilder(args)
                    .ConfigureKestrel(option => {
                        option.ListenAnyIP(port, ListenOptions => {
                            ListenOptions.Protocols = HttpProtocols.Http2;
                        });
                    })
                    .UseStartup<Startup>().UseConfiguration(configuration);
            }
            else {
                throw new Exception("Wrong Port");
            }
        }
    }
}
