using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grpc.Net.Client;

using ProtoBuf.Grpc.Client;

namespace Common.Api {
    public sealed class GrClient {

        public GrClient(GrpcChannel channel) {
            this.Channel = channel;
        }

        public IChartService ChartService => this.Channel.CreateGrpcService<IChartService>();

        public void ReplaceChannel(GrpcChannel nChannel) => this.Channel = nChannel;

        public GrpcChannel Channel { get; private set; }
    }
}
