Imports System
Imports System.Reflection
Imports System.Windows
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.PivotGrid

Namespace HeaderMenuCustomizationExample
	Friend Class HeaderMenuHelper
		#Region "AttachedProperties"
		Public Shared ReadOnly AllowFieldSummaryTypeChangingProperty As DependencyProperty = DependencyProperty.RegisterAttached("AllowFieldSummaryTypeChanging", GetType(Boolean), GetType(HeaderMenuHelper))
		Public Shared Sub SetAllowFieldSummaryTypeChanging(ByVal element As DependencyObject, ByVal value As Boolean)
			element.SetValue(AllowFieldSummaryTypeChangingProperty, value)
		End Sub
		Public Shared Function GetAllowFieldSummaryTypeChanging(ByVal element As DependencyObject) As Boolean
			Return CType(element.GetValue(AllowFieldSummaryTypeChangingProperty), Boolean)
		End Function

		Public Shared ReadOnly AllowFieldSummaryDisplayTypeChangingProperty As DependencyProperty = DependencyProperty.RegisterAttached("AllowFieldSummaryDisplayTypeChanging", GetType(Boolean), GetType(HeaderMenuHelper))
		Public Shared Sub SetAllowFieldSummaryDisplayTypeChanging(ByVal element As DependencyObject, ByVal value As Boolean)
			element.SetValue(AllowFieldSummaryDisplayTypeChangingProperty, value)
		End Sub
		Public Shared Function GetAllowFieldSummaryDisplayTypeChanging(ByVal element As DependencyObject) As Boolean
			Return CType(element.GetValue(AllowFieldSummaryDisplayTypeChangingProperty), Boolean)
		End Function

		Public Shared ReadOnly AllowPopupMenuCustomizationProperty As DependencyProperty = DependencyProperty.RegisterAttached("AllowPopupMenuCustomization", GetType(Boolean), GetType(HeaderMenuHelper), New FrameworkPropertyMetadata(False, New PropertyChangedCallback(AddressOf OnAllowPopupMenuCustomization)))
		Public Shared Sub SetAllowPopupMenuCustomization(ByVal element As DependencyObject, ByVal value As Boolean)
			element.SetValue(AllowPopupMenuCustomizationProperty, value)
		End Sub
		Public Shared Function GetAllowPopupMenuCustomization(ByVal element As DependencyObject) As Boolean
			Return CType(element.GetValue(AllowPopupMenuCustomizationProperty), Boolean)
		End Function
		#End Region ' AttachedProperties

		#Region "PivotPopupMenuCustomization"
		Public Shared Sub OnAllowPopupMenuCustomization(ByVal o As DependencyObject, ByVal args As DependencyPropertyChangedEventArgs)
			Dim pivotGrid As PivotGridControl = TryCast(o, PivotGridControl)
			If pivotGrid Is Nothing Then
				Return
			End If
			If CType(args.NewValue, Boolean) = True AndAlso CType(args.OldValue, Boolean) = False Then
				AddHandler pivotGrid.PopupMenuShowing, AddressOf pivotGrid_PopupMenuShowing
			ElseIf CType(args.NewValue, Boolean) = False AndAlso CType(args.OldValue, Boolean) = True Then
				RemoveHandler pivotGrid.PopupMenuShowing, AddressOf pivotGrid_PopupMenuShowing
			End If

		End Sub

		Private Shared Sub pivotGrid_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
			If e.MenuType = PivotGridMenuType.Header Then
				Dim pivot As PivotGridControl = CType(sender, PivotGridControl)
				Dim field As PivotGridField = e.GetFieldInfo().Field
				If Convert.ToBoolean(field.GetValue(HeaderMenuHelper.AllowFieldSummaryTypeChangingProperty)) Then
					e.Customizations.Add(CreateBarSubItem("Summary Type", "SummaryType", field))
				End If
				If Convert.ToBoolean(field.GetValue(HeaderMenuHelper.AllowFieldSummaryDisplayTypeChangingProperty)) Then
					e.Customizations.Add(CreateBarSubItem("Summary Display Type", "SummaryDisplayType", field))
				End If

			End If
		End Sub
		#End Region

		#Region "CustomBarItemsCreation"

		Private Shared Function CreateBarSubItem(ByVal displayText As String, ByVal propertyName As String, ByVal field As PivotGridField) As BarSubItem
            Dim barSubItem As New BarSubItem()
            barSubItem.Name = "bsi" & propertyName
            barSubItem.Content = displayText

            Dim [property] As PropertyInfo = GetType(PivotGridField).GetProperty(propertyName)

            For Each enumValue As Object In System.Enum.GetValues([property].PropertyType)
                If enumValue.Equals(FieldSummaryDisplayType.Index) Then
                    Continue For
                End If
                Dim checkItem As New BarCheckItem()
                checkItem.Name = "bci" & propertyName & enumValue.ToString()
                checkItem.Content = enumValue.ToString()
                checkItem.IsChecked = Object.Equals(field.Tag, enumValue) OrElse enumValue.Equals(FieldSummaryDisplayType.Default) AndAlso field.Tag Is Nothing
                checkItem.Tag = New Object() {field, [property], enumValue}
                AddHandler checkItem.ItemClick, AddressOf itemClickEventHandler

                barSubItem.ItemLinks.Add(checkItem)
            Next enumValue
            Return barSubItem

        End Function

		Private Shared Sub itemClickEventHandler(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            Dim barItem As BarItem = TryCast(sender, BarItem)
            Dim barItemInfo() As Object = CType(barItem.Tag, Object())
            Dim field As PivotGridField = DirectCast(barItemInfo(0), PivotGridField)
            Dim newValue As FieldSummaryDisplayType = DirectCast(barItemInfo(2), FieldSummaryDisplayType)
            Dim sourceBinding As New DataSourceColumnBinding("Value")
            Select Case newValue
                Case FieldSummaryDisplayType.AbsoluteVariation
                    field.DataBinding = New DifferenceBinding(sourceBinding, CalculationPartitioningCriteria.RowValue, CalculationDirection.DownThenAcross, DifferenceTarget.Previous, DifferenceType.Absolute)
                Case FieldSummaryDisplayType.PercentVariation
                    field.DataBinding = New DifferenceBinding(sourceBinding, CalculationPartitioningCriteria.RowValue, CalculationDirection.DownThenAcross, DifferenceTarget.Previous, DifferenceType.Percentage)
                Case FieldSummaryDisplayType.PercentOfColumn
                    field.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValueAndRowParentValue)
                Case FieldSummaryDisplayType.PercentOfRow
                    field.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.RowValueAndColumnParentValue)
                Case FieldSummaryDisplayType.PercentOfColumnGrandTotal
                    field.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue)
                Case FieldSummaryDisplayType.PercentOfRowGrandTotal
                    field.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.RowValue)
                Case FieldSummaryDisplayType.PercentOfGrandTotal
                    field.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.None)
                Case FieldSummaryDisplayType.RankInColumnLargestToSmallest
                    field.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue, RankType.Dense, FieldSortOrder.Descending)
                Case FieldSummaryDisplayType.RankInColumnSmallestToLargest
                    field.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue, RankType.Dense, FieldSortOrder.Ascending)
                Case FieldSummaryDisplayType.RankInRowLargestToSmallest
                    field.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.RowValue, RankType.Dense, FieldSortOrder.Descending)
                Case FieldSummaryDisplayType.RankInRowSmallestToLargest
                    field.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue, RankType.Dense, FieldSortOrder.Ascending)
                Case Else
                    field.DataBinding = sourceBinding
            End Select
            field.Tag = newValue
            TryCast(field.Parent, PivotGridControl).ReloadData()

        End Sub
		#End Region ' CommonMethods
	End Class
End Namespace
