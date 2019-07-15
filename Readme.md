<!-- default file list -->
*Files to look at*:

* [HeaderMenuHelper.cs](./CS/HeaderMenuCustomizationExample/HeaderMenuHelper.cs) (VB: [HeaderMenuHelper.vb](./VB/HeaderMenuCustomizationExample/HeaderMenuHelper.vb))
* [MainWindow.xaml](./CS/HeaderMenuCustomizationExample/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/HeaderMenuCustomizationExample/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/HeaderMenuCustomizationExample/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/HeaderMenuCustomizationExample/MainWindow.xaml.vb))
<!-- default file list end -->
# How to Customize the Data Header Menu to Add a Command to Change the Summary Type


This example demonstrates how to use the [PivotGridControl.PopupMenuShowing](https://docs.devexpress.com/WPF/DevExpress.Xpf.PivotGrid.PivotGridControl.PopupMenuShowing) event to add custom items to the built-in context menu. 

![screenshot](/images/screenshot.png)

The event is handled automatically if the field's **AllowFieldSummaryTypeChanging** or  **AllowFieldSummaryDisplayTypeChanging** attached properties are **true**. The properties are defined in the **HeaderMenuHelper** class. 

See also:

* [How to Customize the Pivot Grid Context Menu](https://github.com/DevExpress-Examples/how-to-create-a-context-menu-for-field-values-e2205)
