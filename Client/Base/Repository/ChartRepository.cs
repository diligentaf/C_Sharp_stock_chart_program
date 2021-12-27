using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Grpc.Net.Client;

using ProtoBuf.Grpc.Client;

using Common.Api;
using Common.DataCls;

namespace Client.Base.Repository {

    public sealed class ChartRepository : Repository<Chart> {

        public ChartRepository(GrClient client) : base(client) {

        }

		public List<Chart> FetchPrices() => base.Client.ChartService.FetchPrices();

	}
}
