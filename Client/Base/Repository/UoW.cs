using Common;
using Common.DataCls;
using Common.Api;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProtoBuf.Grpc.Client;
using System.Net.Sockets;
using Grpc.Net.Client;
using System.Net;
using System.Data;

namespace Client.Base.Repository {
    public sealed class UoW {
        private static readonly Lazy<UoW> lazy = new Lazy<UoW>(() => new UoW(), true);

        private UoW() {
            this.RemoteAddr = AppMasterConfig.MASTER_ADDR;
            Client = new GrClient(GetGrpcChannel(this.RemoteAddr));

            _repositories.Add(nameof(Chart), new ChartRepository(this.Client));
        }

        private GrpcChannel GetGrpcChannel(string addr, int userId = 0) {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            string ipStr = String.Join(",", Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(k => k.AddressFamily == AddressFamily.InterNetwork));

            Uri uri = new Uri(addr);
                GrpcChannelOptions options = new GrpcChannelOptions();
                options.MaxReceiveMessageSize = (int?)AppMasterConfig.MAX_MSG_SIZE;
                options.HttpClient = new System.Net.Http.HttpClient();
                options.HttpClient.DefaultRequestHeaders.Add("LOCAL_IPS", ipStr);
                return GrpcChannel.ForAddress(uri, options);
        }

        public static UoW BE => lazy.Value;

        private GrClient Client = null;

        public string RemoteAddr { get; private set; }

        public ChartRepository ChartRepo => this._repositories[nameof(Chart)] as ChartRepository;

        private Hashtable _repositories = new Hashtable();
    }
}

