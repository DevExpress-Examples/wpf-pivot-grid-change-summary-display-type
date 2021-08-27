<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128578583/19.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E20028)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [HeaderMenuHelper.cs](./CS/HeaderMenuCustomizationExample/HeaderMenuHelper.cs)
* [MainWindow.xaml](./CS/HeaderMenuCustomizationExample/MainWindow.xaml)
* [MainWindow.xaml.cs](./CS/HeaderMenuCustomizationExample/MainWindow.xaml.cs)
<!-- default file list end -->
# How to Customize the Data Header Menu to Add a Command to Change the Summary Type


This example demonstrates how to use the [PivotGridControl.PopupMenuShowing](https://docs.devexpress.com/WPF/DevExpress.Xpf.PivotGrid.PivotGridControl.PopupMenuShowing) event to add custom items to the built-in context menu. 

![screenshot](/images/screenshot.png)

The event is handled automatically if the field's **AllowFieldSummaryTypeChanging** or  **AllowFieldSummaryDisplayTypeChanging** attached properties are **true**. The properties are defined in the **HeaderMenuHelper** class. 

See also:

* [How to Customize the Pivot Grid Context Menu](https://github.com/DevExpress-Examples/how-to-create-a-context-menu-for-field-values-e2205)
