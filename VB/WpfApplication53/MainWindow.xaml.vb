Imports System.Windows
Imports System.Windows.Controls
Imports System.Data
Imports DevExpress.Xpf.PivotGrid
Imports DevExpress.Xpf.PivotGrid.Internal
Imports DevExpress.Xpf.Bars
Imports DevExpress.XtraPivotGrid.Localization

Namespace WpfApplication53

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            pivotGridControl1.DataSource = CreatePivotDataSource()
        End Sub

        Private Function CreatePivotDataSource() As DataTable
            Dim myTable As DataTable = New DataTable()
            myTable.Columns.Add("Name", GetType(String))
            myTable.Columns.Add("Type", GetType(String))
            myTable.Columns.Add("Value", GetType(Decimal))
            myTable.Columns.Add("Target", GetType(Decimal))
            myTable.Rows.Add(New Object() {"Aaa", "Type 1", 7, 7})
            myTable.Rows.Add(New Object() {"Aaa", "Type 1", 4, 12})
            myTable.Rows.Add(New Object() {"Bbb", "Type 1", 12, 13})
            myTable.Rows.Add(New Object() {"Bbb", "Type 1", 14, 12})
            myTable.Rows.Add(New Object() {"Ccc", "Type 1", 11, 12})
            myTable.Rows.Add(New Object() {"Ccc", "Type 1", 10, 12})
            myTable.Rows.Add(New Object() {"Aaa", "Type 2", 4, 6})
            myTable.Rows.Add(New Object() {"Aaa", "Type 2", 2, 5})
            myTable.Rows.Add(New Object() {"Bbb", "Type 2", 3, 4})
            myTable.Rows.Add(New Object() {"Bbb", "Type 2", 1, 4})
            myTable.Rows.Add(New Object() {"Ccc", "Type 2", 8, 5})
            myTable.Rows.Add(New Object() {"Ccc", "Type 2", 22, 13})
            Return myTable
        End Function

        Private Sub summaryTypeSubItem_DataContextChanged(ByVal sender As Object, ByVal e As DependencyPropertyChangedEventArgs)
        End Sub
    End Class
End Namespace
