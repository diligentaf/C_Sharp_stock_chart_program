using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Common;

namespace Client
{
	using Base;

	using Client.Base.Repository;

	using Common.DataCls;

	using System.Diagnostics;
	using System.Reflection;

	using static Common.AppMasterConfig;

	/// <summary>
	/// ChartWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class ChartWindow : Window
	{
		public ChartWindow()
		{
			InitializeComponent();

			RefreshChartList();

			this.Exit.Click += (s, e) => this.Close();
		}

		private List<Chart> _charts = new List<Chart>();

		private async void RefreshChartList(bool fromDB = true, Chart nChart = null)
		{
			int a = 0;
			while (true)
			{
				this.ChartList.ItemsSource = null;
				_charts = UoW.BE.ChartRepo.FetchPrices();
				if (a == 0)
				{
					int milliseconds = _charts[0].Time.Millisecond;
					await Task.Delay(1000 - milliseconds);
					a++;
				}

				if (this._charts != null && this._charts.Count > 0)
				{
					this.ChartList.ItemsSource = this._charts;
				}
				await Task.Delay(400);
			}
		}
	}
}
