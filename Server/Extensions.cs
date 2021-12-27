using Common.DataCls;

using Microsoft.Extensions.Configuration;

using ProtoBuf.Grpc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server {

    public static class Extensions {
        public static string LocalIPs(this CallContext ctx) {
            if (ctx.CallOptions.Headers != null)
            {
                return ctx.CallOptions.Headers.FirstOrDefault(k => k.Key.Equals("local_ips")).Value;
            }
            else return String.Empty;
        }
    }
}
