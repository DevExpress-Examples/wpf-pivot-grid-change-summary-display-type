using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.PivotGrid.Internal;
using System.Windows;
using System.Reflection;

namespace HeaderMenuCustomizationExample {
    class HeaderMenuHelper {
        #region AttachedProperties
        public static readonly DependencyProperty AllowFieldSummaryTypeChangingProperty =
               DependencyProperty.RegisterAttached("AllowFieldSummaryTypeChanging", typeof(Boolean), typeof(HeaderMenuHelper));
        public static void SetAllowFieldSummaryTypeChanging(DependencyObject element, Boolean value) {
            element.SetValue(AllowFieldSummaryTypeChangingProperty, value);
        }
        public static Boolean GetAllowFieldSummaryTypeChanging(DependencyObject element) {
            return (Boolean)element.GetValue(AllowFieldSummaryTypeChangingProperty);
        }

        public static readonly DependencyProperty AllowFieldSummaryDisplayTypeChangingProperty =
              DependencyProperty.RegisterAttached("AllowFieldSummaryDisplayTypeChanging", typeof(Boolean), typeof(HeaderMenuHelper));
        public static void SetAllowFieldSummaryDisplayTypeChanging(DependencyObject element, Boolean value) {
            element.SetValue(AllowFieldSummaryDisplayTypeChangingProperty, value);
        }
        public static Boolean GetAllowFieldSummaryDisplayTypeChanging(DependencyObject element) {
            return (Boolean)element.GetValue(AllowFieldSummaryDisplayTypeChangingProperty);
        }

        public static readonly DependencyProperty AllowPopupMenuCustomizationProperty =
              DependencyProperty.RegisterAttached("AllowPopupMenuCustomization", typeof(Boolean), typeof(HeaderMenuHelper),
              new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnAllowPopupMenuCustomization)));
        public static void SetAllowPopupMenuCustomization(DependencyObject element, Boolean value) {
            element.SetValue(AllowPopupMenuCustomizationProperty, value);
        }
        public static Boolean GetAllowPopupMenuCustomization(DependencyObject element) {
            return (Boolean)element.GetValue(AllowPopupMenuCustomizationProperty);
        }
        #endregion AttachedProperties

        #region PivotPopupMenuCustomization
        public static void OnAllowPopupMenuCustomization(DependencyObject o, DependencyPropertyChangedEventArgs args) {
            PivotGridControl pivotGrid = o as PivotGridControl;
            if (pivotGrid == null) return;
            if( (Boolean)args.NewValue == true && (Boolean)args.OldValue == false )
                pivotGrid.PopupMenuShowing += pivotGrid_PopupMenuShowing;
            else if ( (Boolean)args.NewValue == false && (Boolean)args.OldValue == true )
                pivotGrid.PopupMenuShowing -= pivotGrid_PopupMenuShowing;

        }

        static void pivotGrid_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e) {
            if (e.MenuType == PivotGridMenuType.Header) {
                PivotGridControl pivot = (PivotGridControl)sender;
                PivotGridField field = e.GetFieldInfo().Field;
                if (Convert.ToBoolean(field.GetValue(HeaderMenuHelper.AllowFieldSummaryTypeChangingProperty)))
                    e.Customizations.Add(CreateBarSubItem("Summary Type", "SummaryType", field));
                if (Convert.ToBoolean(field.GetValue(HeaderMenuHelper.AllowFieldSummaryDisplayTypeChangingProperty)))
                    e.Customizations.Add(CreateBarSubItem("Summary Display Type", "SummaryDisplayType", field));

            }
        }
        #endregion

        #region CustomBarItemsCreation

        private static BarSubItem CreateBarSubItem(string displayText, string propertyName, PivotGridField field) {
            BarSubItem barSubItem = new BarSubItem();
            barSubItem.Name = "bsi" + propertyName;
            barSubItem.Content = displayText;

            PropertyInfo property = typeof(PivotGridField).GetProperty(propertyName);

            foreach (object enumValue in Enum.GetValues(property.PropertyType)) {
                if(enumValue.Equals(FieldSummaryDisplayType.Index)) continue;
                BarCheckItem checkItem = new BarCheckItem();
                checkItem.Name = "bci" + propertyName + enumValue;
                checkItem.Content = enumValue.ToString();
                checkItem.IsChecked = Object.Equals(field.Tag, enumValue)||
                    enumValue.Equals(FieldSummaryDisplayType.Default) && field.Tag == null;
                checkItem.Tag = new object[] { field, property, enumValue };
                checkItem.ItemClick += itemClickEventHandler;

                barSubItem.ItemLinks.Add(checkItem);
            }
            return barSubItem;
        }

        static void itemClickEventHandler(object sender, ItemClickEventArgs e) {
            BarItem barItem = sender as BarItem;
            object[] barItemInfo = (object[])barItem.Tag;
            PivotGridField field = (PivotGridField)barItemInfo[0];
            FieldSummaryDisplayType newValue = (FieldSummaryDisplayType)barItemInfo[2];
            DataSourceColumnBinding sourceBinding = new DataSourceColumnBinding("Value");
            switch(newValue){
                case FieldSummaryDisplayType.AbsoluteVariation:
                    field.DataBinding = new DifferenceBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue,
                        CalculationDirection.DownThenAcross,
                        DifferenceTarget.Previous,
                        DifferenceType.Absolute);
                    break;
                case FieldSummaryDisplayType.PercentVariation:
                    field.DataBinding = new DifferenceBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue,
                        CalculationDirection.DownThenAcross,
                        DifferenceTarget.Previous,
                        DifferenceType.Percentage);
                    break;
                case FieldSummaryDisplayType.PercentOfColumn:
                    field.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValueAndRowParentValue);
                    break;
                case FieldSummaryDisplayType.PercentOfRow:
                    field.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValueAndColumnParentValue);
                    break;
                case FieldSummaryDisplayType.PercentOfColumnGrandTotal:
                    field.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue);
                    break;
                case FieldSummaryDisplayType.PercentOfRowGrandTotal:
                    field.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue);
                    break;
                case FieldSummaryDisplayType.PercentOfGrandTotal:
                    field.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.None);
                    break;
                case FieldSummaryDisplayType.RankInColumnLargestToSmallest:
                    field.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue,
                        RankType.Dense, FieldSortOrder.Descending);
                    break;
                case FieldSummaryDisplayType.RankInColumnSmallestToLargest:
                    field.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue,
                        RankType.Dense, FieldSortOrder.Ascending);
                    break;
                case FieldSummaryDisplayType.RankInRowLargestToSmallest:
                    field.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue,
                        RankType.Dense, FieldSortOrder.Descending);
                    break;
                case FieldSummaryDisplayType.RankInRowSmallestToLargest:
                    field.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue,
                        RankType.Dense, FieldSortOrder.Ascending);
                    break;
                default:
                    field.DataBinding = sourceBinding;
                    break;
            }
            field.Tag = newValue;
            (field.Parent as PivotGridControl).ReloadData();

        }
        #endregion CommonMethods
    }
}
