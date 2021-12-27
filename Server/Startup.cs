using Common.Api;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc.Server;

using Server.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server {
    using Common;
	using Common.DataCls;

    using Microsoft.Extensions.Configuration;

    using Services;

    public sealed class Startup {

        public Startup(IConfiguration configuration, IWebHostEnvironment env) {
            this._config = configuration;
            _env = env;
            List<Chart> charts = Chart.NewChartList;
            ChartService chartService = new ChartService();
            chartService.InitializePrices(charts);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {

            services.AddCodeFirstGrpc(config => {
                config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
                config.EnableDetailedErrors = true;
                config.MaxReceiveMessageSize = (int?)AppMasterConfig.MAX_MSG_SIZE;
            });

            services.TryAddSingleton(BinderConfiguration.Create(binder: new ServiceBinderWithServiceResolutionFromServiceCollection(services)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment _) {
            app.UseRouting();

            app.UseEndpoints(endpoints => {
				endpoints.MapGrpcService<ChartService>();
            });
        }

        public IConfiguration _config { get; private set; }

        public IWebHostEnvironment _env { get; private set; }
    }
}
