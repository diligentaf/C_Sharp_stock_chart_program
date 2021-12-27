using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;

namespace Common.DataCls
{
	[DataContract]
	public sealed class Chart 
	{

		#region ClassMembers
		[DataMember(Order = 1)]
		public int PositionId { get; set; }

		[DataMember(Order = 2)]
		public string Ticker { get; set; }

		[DataMember(Order = 3)]
		public double SpotPrice { get; set; }

		[DataMember(Order = 4)]
		public int Qt1 { get; set; }

		[DataMember(Order = 5)]
		public int Qt0 { get; set; }

		[DataMember(Order = 6)]
		public int Change { get; set; }

		[DataMember(Order = 7)]
		public DateTime Time { get; set; }

		#endregion

		private static Lazy<List<Chart>> lazy = new Lazy<List<Chart>>(() => new List<Chart>(), true);

		public static List<Chart> NewChartList => new List<Chart>()
		{
			Chart.HongKong,
			Chart.Korea,
			Chart.NewYork,
		};

		public static Chart HongKong => new Chart()
		{
			PositionId = 1,
			Ticker = "700.HK",
			SpotPrice = 329.60,
			Qt0 = 10000,
			Qt1 = 2000,
			Change = 8000,
			Time = DateTime.Now,
		};

		public static Chart Korea => new Chart()
		{
			PositionId = 2,
			Ticker = "123.KRX",
			SpotPrice = 5.96,
			Qt0 = 30000,
			Qt1 = 100,
			Change = 900,
			Time = DateTime.Now,
		};
		public static Chart NewYork => new Chart()
		{
			PositionId = 3,
			Ticker = "456.NY",
			SpotPrice = 3.090,
			Qt0 = 50000,
			Qt1 = 1000,
			Change = 49000,
			Time = DateTime.Now,
		};

		public static List<Chart> BE => lazy.Value;
	}
}

