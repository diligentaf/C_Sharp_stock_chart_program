using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Base.ExCtls {
    /// <summary>
    /// ExDataGrid.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    using Base.ExCtls;

    using Common.Api;

    public partial class ExDataGrid : DataGrid
    {
        public ExDataGrid()
        {
            InitializeComponent();
            this.AutoGenerateColumns = false;
            this.Style = FindResource("GlobalDataGridStyle") as Style;

            this.ContextMenu = GenBasicContextMenu();
            this.FontSize = 11;
        }

        private ContextMenu GenBasicContextMenu()
        {
            try
            {
                ContextMenu ctxMenu = new ContextMenu();

                MenuItem mnuAutoSizeCols = new MenuItem() { Name = "autoSizeCols", Header = "Column 폭 자동정렬" };
                MenuItem mnuAutoSizeColsToCell = new MenuItem() { Name = "autoSizeCols", Header = "Cell Width" };
                mnuAutoSizeColsToCell.Click += (s, e) => this.Columns.ToList().ForEach(k => k.Width = new DataGridLength(1.0, DataGridLengthUnitType.SizeToCells));
                mnuAutoSizeCols.Items.Add(mnuAutoSizeColsToCell);

                MenuItem mnuAutoSizeColsToHeader = new MenuItem() { Name = "autoSizeCols", Header = "Header Width" };
                mnuAutoSizeColsToHeader.Click += (s, e) => this.Columns.ToList().ForEach(k => k.Width = new DataGridLength(1.0, DataGridLengthUnitType.SizeToHeader));
                mnuAutoSizeCols.Items.Add(mnuAutoSizeColsToHeader);

                MenuItem mnuAutoSizeColsToBoth = new MenuItem() { Name = "autoSizeCols", Header = "Cell/Header Width" };
                mnuAutoSizeColsToBoth.Click += (s, e) => this.Columns.ToList().ForEach(k => k.Width = new DataGridLength(1.0, DataGridLengthUnitType.Auto));
                mnuAutoSizeCols.Items.Add(mnuAutoSizeColsToBoth);

                ctxMenu.Items.Add(mnuAutoSizeCols);
                return ctxMenu;
            }
            catch (Exception err)
            {
                return null;
            }
        }
    }

}
