<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128578583/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E20028)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# PivotGrid for WPF - How to Change SummaryDisplayType in the Context Menu

The following example shows how to customize the field header's context menu in the [PivotGridControl.PopupMenuShowing](https://docs.devexpress.com/WPF/DevExpress.Xpf.PivotGrid.PivotGridControl.PopupMenuShowing) event to add custom items to the built-in context menu. 

![screenshot](/images/screenshot.png)

<!-- default file list -->
## Files to Review

* [HeaderMenuHelper.cs](./CS/HeaderMenuCustomizationExample/HeaderMenuHelper.cs) (VB: [HeaderMenuHelper.vb](./VB/HeaderMenuCustomizationExample/HeaderMenuHelper.vb))
* [MainWindow.xaml](./CS/HeaderMenuCustomizationExample/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/HeaderMenuCustomizationExample/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/HeaderMenuCustomizationExample/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/HeaderMenuCustomizationExample/MainWindow.xaml.vb))
<!-- default file list end -->

The event adds a new item to the _fieldValuer_ field header's context menu and allows you to change the [SummaryDisplayType](https://docs.devexpress.com/WPF/8053/controls-and-libraries/pivot-grid/data-shaping/aggregation/summaries/summary-display-modes) value.
The event is handled automatically if the field's **AllowFieldSummaryTypeChanging** or  **AllowFieldSummaryDisplayTypeChanging** attached properties are **true**. The properties are defined in the **HeaderMenuHelper** class.

## API
- [PivotGridControl.PopupMenuShowing](https://docs.devexpress.com/WPF/DevExpress.Xpf.PivotGrid.PivotGridControl.PopupMenuShowing)
- [Summary Display Modes](https://docs.devexpress.com/WPF/8053/controls-and-libraries/pivot-grid/data-shaping/aggregation/summaries/summary-display-modes)

## More Examples

* [How to Customize the Pivot Grid Context Menu](https://github.com/DevExpress-Examples/how-to-create-a-context-menu-for-field-values-e2205)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-pivot-grid-change-summary-display-type&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-pivot-grid-change-summary-display-type&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
