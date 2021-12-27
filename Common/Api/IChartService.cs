using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

using Common.DataCls;

namespace Common.Api {

    [Service]
    public interface IChartService {
        [Operation]
        List<Chart> FetchPrices();

        [Operation]
        void InitializePrices(List<Chart> charts);

    }
}
