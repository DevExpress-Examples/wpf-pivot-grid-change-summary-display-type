using System.Data;
using System.Windows;

namespace HeaderMenuCustomizationExample {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            pivotGridControl1.DataSource = CreatePivotDataSource();
        }

        public static DataTable CreatePivotDataSource() {
            DataTable myTable = new DataTable();
            myTable.Columns.Add("Name", typeof(string));
            myTable.Columns.Add("Owner", typeof(string));
            myTable.Columns.Add("Type", typeof(string));
            myTable.Columns.Add("Value", typeof(decimal));
            myTable.Columns.Add("Target", typeof(decimal));

            myTable.Rows.Add(new object[] { "Aaa", "Andrew Fuller", "Type 1", 7, 7 });
            myTable.Rows.Add(new object[] { "Aaa", "Anne Dodsworth", "Type 1", 4, 12 });
            myTable.Rows.Add(new object[] { "Bbb", "Janet Leverling", "Type 1", 12, 13 });
            myTable.Rows.Add(new object[] { "Bbb", "Andrew Fuller", "Type 1", 14, 12 });
            myTable.Rows.Add(new object[] { "Ccc", "Anne Dodsworth", "Type 1", 11, 12 });
            myTable.Rows.Add(new object[] { "Ccc", "Janet Leverling", "Type 1", 10, 12 });

            myTable.Rows.Add(new object[] { "Aaa", "Andrew Fuller", "Type 2", 4, 6 });
            myTable.Rows.Add(new object[] { "Aaa", "Anne Dodsworth", "Type 2", 2, 5 });
            myTable.Rows.Add(new object[] { "Bbb", "Janet Leverling", "Type 2", 3, 4 });
            myTable.Rows.Add(new object[] { "Bbb", "Andrew Fuller", "Type 2", 1, 4 });
            myTable.Rows.Add(new object[] { "Ccc", "Anne Dodsworth", "Type 2", 8, 5 });
            myTable.Rows.Add(new object[] { "Ccc", "Janet Leverling", "Type 2", 22, 13 });

            return myTable;
        }
    }
}
