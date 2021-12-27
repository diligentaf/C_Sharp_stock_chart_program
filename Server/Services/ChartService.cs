using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Api;
using Common.DataCls;


namespace Server.Services {
    //using DataPool;

    using ProtoBuf.Grpc;

    using Common;

    public class ChartService : IChartService {
        static List<Chart> charts_;
        static Random random = new Random();

		public List<Chart> FetchPrices() {
            return charts_;
        }

        public async void InitializePrices(List<Chart> charts)
        {
			while (true)
			{
				for (int i = 0; i < charts.Count(); i++)
				{
					int num = random.Next(1000, 9000);
					charts[i].Qt1 = num;
                    charts[i].Change = charts[i].Qt0 - num;
				}
				charts_ = charts;
				await Task.Delay(1000);
			}
		}

	}
}
