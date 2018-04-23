using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.PivotGrid.Internal;
using DevExpress.Xpf.Bars;
using DevExpress.XtraPivotGrid.Localization;

namespace WpfApplication53
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pivotGridControl1.DataSource = CreatePivotDataSource();
        }

        private DataTable  CreatePivotDataSource()
        {
            DataTable myTable = new DataTable();
            myTable.Columns.Add("Name", typeof(string));
            myTable.Columns.Add("Type", typeof(string));
            myTable.Columns.Add("Value", typeof(decimal));
            myTable.Columns.Add("Target", typeof(decimal));

            myTable.Rows.Add(new object[] { "Aaa", "Type 1", 7, 7 });
            myTable.Rows.Add(new object[] { "Aaa", "Type 1", 4, 12 });
            myTable.Rows.Add(new object[] { "Bbb", "Type 1", 12,13 });
            myTable.Rows.Add(new object[] { "Bbb", "Type 1", 14, 12});
            myTable.Rows.Add(new object[] { "Ccc", "Type 1", 11,12 });
            myTable.Rows.Add(new object[] { "Ccc", "Type 1", 10,12 });

            myTable.Rows.Add(new object[] { "Aaa", "Type 2", 4,6 });
            myTable.Rows.Add(new object[] { "Aaa", "Type 2", 2, 5});
            myTable.Rows.Add(new object[] { "Bbb", "Type 2", 3,4 });
            myTable.Rows.Add(new object[] { "Bbb", "Type 2", 1, 4});
            myTable.Rows.Add(new object[] { "Ccc", "Type 2", 8, 5});
            myTable.Rows.Add(new object[] { "Ccc", "Type 2", 22, 13});

            return myTable;
        }

        private void summaryTypeSubItem_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
          
        }



       



    }

    
}
