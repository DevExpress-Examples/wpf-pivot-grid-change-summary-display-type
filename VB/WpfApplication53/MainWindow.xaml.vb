Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Data
Imports DevExpress.Xpf.PivotGrid
Imports DevExpress.Xpf.PivotGrid.Internal
Imports DevExpress.Xpf.Bars
Imports DevExpress.XtraPivotGrid.Localization

Namespace WpfApplication53
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()

		End Sub

		Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			pivotGridControl1.DataSource = CreatePivotDataSource()
		End Sub

		Private Function CreatePivotDataSource() As DataTable
			Dim myTable As New DataTable()
			myTable.Columns.Add("Name", GetType(String))
			myTable.Columns.Add("Type", GetType(String))
			myTable.Columns.Add("Value", GetType(Decimal))

			myTable.Rows.Add(New Object() { "Aaa", "Type 1", 7 })
			myTable.Rows.Add(New Object() { "Aaa", "Type 1", 4 })
			myTable.Rows.Add(New Object() { "Bbb", "Type 1", 12 })
			myTable.Rows.Add(New Object() { "Bbb", "Type 1", 14 })
			myTable.Rows.Add(New Object() { "Ccc", "Type 1", 11 })
			myTable.Rows.Add(New Object() { "Ccc", "Type 1", 10 })

			myTable.Rows.Add(New Object() { "Aaa", "Type 2", 4 })
			myTable.Rows.Add(New Object() { "Aaa", "Type 2", 2 })
			myTable.Rows.Add(New Object() { "Bbb", "Type 2", 3 })
			myTable.Rows.Add(New Object() { "Bbb", "Type 2", 1 })
			myTable.Rows.Add(New Object() { "Ccc", "Type 2", 8 })
			myTable.Rows.Add(New Object() { "Ccc", "Type 2", 22 })

			Return myTable
		End Function

		Private Sub summaryTypeSubItem_DataContextChanged(ByVal sender As Object, ByVal e As DependencyPropertyChangedEventArgs)

		End Sub







	End Class


End Namespace
