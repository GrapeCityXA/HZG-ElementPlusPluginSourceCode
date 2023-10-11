using ElementUI.WpfControls;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ElementUI.Properties;
using System.ComponentModel.DataAnnotations;

namespace ElementUI
{
    public class SelectCellTypeDesigner : CellTypeDesigner<SelectCellType>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new InputBoxDrawingControl(cellInfo, drawingHelper, StyleHelper.CreateStyleViewModel(cellInfo, drawingHelper), this.CellType);

            return control;
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            string[] defaults = Resources.SelectCellType_defaultSetting.Split(',');
            foreach (var item in defaults)
            {
                this.CellType.options.Add(new NormalItem()
                {
                    value = item,
                    label = item,
                });
            }
            this.CellType.placeholder = Resources.SelectCellType_placeholder_DefaultValue;
            this.CellType.noDataText = Resources.SelectCellType_noDataText_DefaultValue;
            this.CellType.noMatchText = Resources.SelectCellType_noMatchText_DefaultValue;
            this.CellType.filterInServerOptions.loadingText = Resources.SelectCellType_loadingText_DefaultValue;
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Select.png")]
    [OrderWeight((int)Utils.OrderWeight.Select)]
    [Designer("ElementUI.SelectCellTypeDesigner, ElementUI")]
    public class SelectCellType : InputCellTypeBase
    {
        #region Override基类属性，仅仅是为了确保属性顺序
        public override List<Command> CommandList { get => base.CommandList; set => base.CommandList = value; }
        public override DataValidationLink DataValidationLink { get => base.DataValidationLink; set => base.DataValidationLink = value; }
        public override List<UIPermission> UIPermissions { get => base.UIPermissions; set => base.UIPermissions = value; }
        public override object DefaultValue { get => base.DefaultValue; set => base.DefaultValue = value; }
        public override bool NeedFormatDefaultValue => false;
        #endregion

        [SRDisplayName(nameof(Resources.SelectCellType_useBinding))]
        [BoolProperty]
        [MergableProperty(false)]
        public bool useBinding { get; set; } = false;

        [ListProperty(IndentLevel = 1)]
        [SRDisplayName(nameof(Resources.SelectCellType_options))]
        public List<NormalItem> options { get; set; } = new List<NormalItem>();

        [SRBindingDataSourceProperty(Columns = nameof(Resources.SelectCellType_bindingOptions_Columns), IndentLevel = 1)]
        [SRDisplayName(nameof(Resources.SelectCellType_bindingOptions))]
        public object bindingOptions { get; set; }

        [SRDisplayName(nameof(Resources.Select_AllowAddEmptyItem))]
        [BoolProperty]
        public bool AllowAddEmptyItem { get; set; }

        [SRDisplayName(nameof(Resources.Select_EmptyItemText))]
        public string EmptyItemLabel { get; set; }

        [SRDisplayName(nameof(Resources.SelectCellType_placeholder))]
        [SearchableProperty]
        public string placeholder { get; set; }

        [SRDisplayName(nameof(Resources.SelectCellType_noDataText))]
        public string noDataText { get; set; }

        [SRDisplayName(nameof(Resources.SelectCellType_multiple))]
        [BoolProperty]
        public bool multiple { get; set; }

        [SRDisplayName(nameof(Resources.SelectCellType_collapseTags))]
        [BoolProperty]
        public bool collapseTags { get; set; }

        [SRDisplayName(nameof(Resources.SelectCellType_multipleLimit))]
        [SRIntProperty(AllowNull = true, Min = 1, Watermark = nameof(Resources.NoLimit))]
        public int? multipleLimit { get; set; }

        [SRDisplayName(nameof(Resources.SelectCellType_filterable))]
        [BoolProperty]
        public bool filterable { get; set; }

        [SRDisplayName(nameof(Resources.SelectCellType_filterInServer))]
        [BoolProperty]
        public bool filterInServer { get; set; }

        [SRDisplayName(nameof(Resources.SelectCellType_filterInServerOptions))]
        [ObjectProperty(ObjType = typeof(FilterInServerOptions), IndentLevel = 2)]
        public FilterInServerOptions filterInServerOptions { get; set; } = new FilterInServerOptions();

        [SRDisplayName(nameof(Resources.SelectCellType_reserveKeyword))]
        [SRDescription(nameof(Resources.SelectCellType_reserveKeyword_Description))]
        [BoolProperty]
        public bool reserveKeyword { get; set; } = false;

        [SRDisplayName(nameof(Resources.SelectCellType_allowCreate))]
        [BoolProperty]
        public bool allowCreate { get; set; } = false;

        [SRDisplayName(nameof(Resources.SelectCellType_noMatchText))]
        public string noMatchText { get; set; }

        [SRCategoryHeader(nameof(Resources.Other))]
        [SRDisplayName(nameof(Resources.SelectCellType_clearable))]
        public bool clearable { get; set; }

        [SRDisplayName(nameof(Resources.SelectCellType_IsDisabled))]
        public override bool IsDisabled { get; set; }

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(options))
            {
                return !useBinding;
            }
            if (propertyName == nameof(bindingOptions))
            {
                return useBinding;
            }
            if (propertyName == nameof(allowCreate) || propertyName == nameof(noMatchText))
            {
                return filterable;
            }
            if (propertyName == nameof(collapseTags) ||
                propertyName == nameof(multipleLimit))
            {
                return multiple;
            }
            if (propertyName == nameof(reserveKeyword))
            {
                return multiple && filterable;
            }
            if (propertyName == nameof(filterInServer))
            {
                return useBinding && filterable;
            }
            if (propertyName == nameof(filterInServerOptions))
            {
                return useBinding && filterable && filterInServer;
            }
            if (propertyName == nameof(AllowAddEmptyItem))
            {
                return useBinding;
            }
            if (propertyName == nameof(EmptyItemLabel))
            {
                return useBinding && AllowAddEmptyItem;
            }
            if (propertyName == nameof(noDataText)) {
                return !AllowAddEmptyItem || !useBinding;
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }

        public override string ToString()
        {
            return Resources.Selector;
        }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.SelectCellType_SetDataSourceByStringArray))]
        [SRDescription(nameof(Resources.SelectCellType_SetDataSourceByStringArray_Description))]
        public void SetDataSourceByStringArray(
            [SRItemDisplayName(nameof(Resources.SelectCellType_dataSource))]
            [Required]
            string dataSource)
        {

        }


        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.SelectCellType_SetDataSourceByObjArray))]
        [SRDescription(nameof(Resources.SelectCellType_SetDataSourceByObjArray_Description))]
        public void SetDataSourceByObjArray(
                [SRItemDisplayName(nameof(Resources.SelectCellType_dataSource))]
                [Required]
                string dataSource,
                [SRItemDisplayName(nameof(Resources.SelectCellType_valueProperty))]
                [Required]
                string valueProperty = "value",
                [SRItemDisplayName(nameof(Resources.SelectCellType_labelProperty))]
                [Required]
                [SRDescription(nameof(Resources.SelectCellType_SetDataSourceByObjArray_labelProperty_Description))]
                string labelProperty = "label")
        {

        }

        [SRDisplayName(nameof(Resources.SelectCellType_Focus))]
        [RunTimeMethod]
        [SRDescription(nameof(Resources.SelectCellType_Focus_Description))]
        public void Focus()
        {
        }

        [SRDisplayName(nameof(Resources.MethodName_ReloadBindingItems))]
        [SRDescription(nameof(Resources.MethodName_ReloadBindingItems_Description))]
        [RunTimeMethod]
        public void ReloadBindingItems()
        {
        }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.GetDisplayText))]
        public GetSelectedTextResult GetSelectedText()
        {
            return null;
        }
        public class GetSelectedTextResult
        {
            [SRDisplayName(nameof(Resources.DisplayText))]
            public string SelectedText { get; set; }
        }

        public override bool GetRunTimeMethodVisible(string name)
        {
            if (name == nameof(ReloadBindingItems))
            {
                return useBinding;
            }
            return base.GetRunTimeMethodVisible(name);
        }
    }

    public class FilterInServerOptions : ObjectPropertyBase
    {
        [SRDisplayName(nameof(Resources.SelectCellType_defaultMaxOptionsCount))]
        [DefaultValue(0)]
        public int defaultMaxOptionsCount { get; set; } = 0;

        [SRDisplayName(nameof(Resources.SelectCellType_maxFilterResultCount))]
        [DefaultValue(500)]
        public int maxFilterResultCount { get; set; } = 500;

        [SRDisplayName(nameof(Resources.SelectCellType_matchMethod))]
        [SRComboProperty(ValueList = "contains|startWith", DisplayList = nameof(Resources.SelectCellType_matchMethod_DisplayList))]
        public string matchMethod { get; set; } = "contains";

        [SRDisplayName(nameof(Resources.SelectCellType_loadingText))]
        public string loadingText { get; set; }
    }

    public class NormalItem
    {
        [SRDisplayName(nameof(Resources.Common_Value))]
        public string value { get; set; }

        [SRDisplayName(nameof(Resources.SelectCellType_label))]
        public string label { get; set; }
    }
}
