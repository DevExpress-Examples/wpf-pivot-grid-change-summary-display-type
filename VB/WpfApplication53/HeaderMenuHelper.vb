Imports System
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.PivotGrid
Imports System.Windows
Imports System.Reflection

Namespace WpfApplication53

    Friend Class HeaderMenuHelper

'#Region "AttachedProperties"
        Public Shared ReadOnly AllowFieldSummaryTypeChangingProperty As DependencyProperty = DependencyProperty.RegisterAttached("AllowFieldSummaryTypeChanging", GetType(Boolean), GetType(HeaderMenuHelper))

        Public Shared Sub SetAllowFieldSummaryTypeChanging(ByVal element As DependencyObject, ByVal value As Boolean)
            element.SetValue(AllowFieldSummaryTypeChangingProperty, value)
        End Sub

        Public Shared Function GetAllowFieldSummaryTypeChanging(ByVal element As DependencyObject) As Boolean
            Return CBool(element.GetValue(AllowFieldSummaryTypeChangingProperty))
        End Function

        Public Shared ReadOnly AllowFieldSummaryDisplayTypeChangingProperty As DependencyProperty = DependencyProperty.RegisterAttached("AllowFieldSummaryDisplayTypeChanging", GetType(Boolean), GetType(HeaderMenuHelper))

        Public Shared Sub SetAllowFieldSummaryDisplayTypeChanging(ByVal element As DependencyObject, ByVal value As Boolean)
            element.SetValue(AllowFieldSummaryDisplayTypeChangingProperty, value)
        End Sub

        Public Shared Function GetAllowFieldSummaryDisplayTypeChanging(ByVal element As DependencyObject) As Boolean
            Return CBool(element.GetValue(AllowFieldSummaryDisplayTypeChangingProperty))
        End Function

        Public Shared ReadOnly AllowPopupMenuCustomizationProperty As DependencyProperty = DependencyProperty.RegisterAttached("AllowPopupMenuCustomization", GetType(Boolean), GetType(HeaderMenuHelper), New FrameworkPropertyMetadata(False, New PropertyChangedCallback(AddressOf OnAllowPopupMenuCustomization)))

        Public Shared Sub SetAllowPopupMenuCustomization(ByVal element As DependencyObject, ByVal value As Boolean)
            element.SetValue(AllowPopupMenuCustomizationProperty, value)
        End Sub

        Public Shared Function GetAllowPopupMenuCustomization(ByVal element As DependencyObject) As Boolean
            Return CBool(element.GetValue(AllowPopupMenuCustomizationProperty))
        End Function

'#End Region  ' AttachedProperties
'#Region "PivotPopupMenuCustomization"
        Public Shared Sub OnAllowPopupMenuCustomization(ByVal o As DependencyObject, ByVal args As DependencyPropertyChangedEventArgs)
            Dim pivotGrid As PivotGridControl = TryCast(o, PivotGridControl)
            If pivotGrid Is Nothing Then Return
            If CBool(args.NewValue) = True AndAlso CBool(args.OldValue) = False Then
                AddHandler pivotGrid.PopupMenuShowing, AddressOf pivotGrid_PopupMenuShowing
            ElseIf CBool(args.NewValue) = False AndAlso CBool(args.OldValue) = True Then
                RemoveHandler pivotGrid.PopupMenuShowing, AddressOf pivotGrid_PopupMenuShowing
            End If
        End Sub

        Private Shared Sub pivotGrid_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
            If e.MenuType = PivotGridMenuType.Header Then
                Dim pivot As PivotGridControl = CType(sender, PivotGridControl)
                Dim field As PivotGridField = e.GetFieldInfo().Field
                If Convert.ToBoolean(field.GetValue(AllowFieldSummaryTypeChangingProperty)) Then e.Customizations.Add(CreateBarSubItem("Summary Type", "SummaryType", field))
                If Convert.ToBoolean(field.GetValue(AllowFieldSummaryDisplayTypeChangingProperty)) Then e.Customizations.Add(CreateBarSubItem("Summary Display Type", "SummaryDisplayType", field))
            End If
        End Sub

'#End Region
'#Region "CustomBarItemsCreation"
        Private Shared Function CreateBarSubItem(ByVal displayText As String, ByVal propertyName As String, ByVal field As PivotGridField) As BarSubItem
            Dim barSubItem As BarSubItem = New BarSubItem()
            barSubItem.Name = "bsi" & propertyName
            barSubItem.Content = displayText
            Dim [property] As PropertyInfo = GetType(PivotGridField).GetProperty(propertyName)
            For Each enumValue As Object In [Enum].GetValues([property].PropertyType)
                Dim checkItem As BarCheckItem = New BarCheckItem()
                checkItem.Name = "bci" & propertyName & enumValue
                checkItem.Content = enumValue.ToString()
                checkItem.IsChecked = Equals([property].GetValue(field, New Object(-1) {}), enumValue)
                checkItem.Tag = New Object() {field, [property], enumValue}
                AddHandler checkItem.ItemClick, AddressOf itemClickEventHandler
                barSubItem.ItemLinks.Add(checkItem)
            Next

            Return barSubItem
        End Function

        Private Shared Sub itemClickEventHandler(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            Dim barItem As BarItem = TryCast(sender, BarItem)
            Dim barItemInfo As Object() = CType(barItem.Tag, Object())
            Dim field As PivotGridField = CType(barItemInfo(0), PivotGridField)
            Dim [property] As PropertyInfo = CType(barItemInfo(1), PropertyInfo)
            Dim newValue As Object = barItemInfo(2)
            [property].SetValue(field, newValue, New Object(-1) {})
        End Sub
'#End Region  ' CommonMethods
    End Class
End Namespace
