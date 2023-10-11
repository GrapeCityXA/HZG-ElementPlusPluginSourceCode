using ElementUI.WpfControls;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using ElementUI.Properties;
using System.Linq;

namespace ElementUI
{
    public class TransferCellTypeDesigner : CellTypeDesigner<TransferCellType>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new TransferDrawingControl(drawingHelper, this.CellType);

            return control;
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            for (int i = 1; i <= 15; i++)
            {
                string text = Resources.TransferCellType_DefaultItemName + i;

                CellType.options.Add(new TransferOptionItem()
                {
                    key = text,
                    label = text,
                    disabled = i % 5 == 0
                });
            }
            this.CellType.leftTitle = Resources.TransferCellType_leftTitle_DefaultValue;
            this.CellType.rightTitle = Resources.TransferCellType_rightTitle_DefaultValue;
            this.CellType.filterPlaceholder = Resources.TransferCellType_filterPlaceholder_DefaultValue;
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Transfer.png")]
    [OrderWeight((int)Utils.OrderWeight.Transfer)]
    [Designer("ElementUI.TransferCellTypeDesigner, ElementUI")]
    public class TransferCellType : InputCellTypeBase, ICommandCellType
    {
        #region Override基类属性，仅仅是为了确保属性顺序
        public override List<Command> CommandList { get => base.CommandList; set => base.CommandList = value; }
        [Browsable(false)]
        public override DataValidationLink DataValidationLink { get => base.DataValidationLink; set => base.DataValidationLink = value; }
        [Browsable(false)]
        public override List<UIPermission> UIPermissions { get => base.UIPermissions; set => base.UIPermissions = value; }
        public override object DefaultValue { get => base.DefaultValue; set => base.DefaultValue = value; }
        public override bool NeedFormatDefaultValue => false;

        #endregion

        [SRDisplayName(nameof(Resources.TransferCellTyle_useBinding))]
        [BoolProperty]
        [MergableProperty(false)]
        public bool useBinding { get; set; } = false;

        [SRDisplayName(nameof(Resources.TransferCellTyle_options))]
        [ListProperty(IndentLevel = 1)]
        public List<TransferOptionItem> options { get; set; } = new List<TransferOptionItem>();

        [SRBindingDataSourceProperty(Columns = nameof(Resources.TransferCellType_bindingOptions_Columns), IndentLevel = 1)]
        [SRDisplayName(nameof(Resources.TransferCellTyle_bindingOptions))]
        public object bindingOptions { get; set; }

        [SRDisplayName(nameof(Resources.TransferCellTyle_leftTitle))]
        public string leftTitle { get; set; }

        [SRDisplayName(nameof(Resources.TransferCellTyle_rightTitle))]
        public string rightTitle { get; set; }

        [SRDisplayName(nameof(Resources.TransferCellTyle_targetOrder))]
        public TargetOrder targetOrder { get; set; } = TargetOrder.original;

        [SRDisplayName(nameof(Resources.TransferCellTyle_filterable))]
        [BoolProperty]
        public bool filterable { get; set; }

        [SRDisplayName(nameof(Resources.TransferCellTyle_filterPlaceholder))]
        public string filterPlaceholder { get; set; }

        [Browsable(false)]
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

            if (propertyName == nameof(filterPlaceholder))
            {
                return filterable;
            }

            return base.GetDesignerPropertyVisible(propertyName);
        }

        [SRDisplayName(nameof(Resources.MethodName_ReloadBindingItems))]
        [SRDescription(nameof(Resources.MethodName_ReloadBindingItems_Description))]
        [RunTimeMethod]
        public void ReloadBindingItems()
        {
        }

        public override bool GetRunTimeMethodVisible(string name)
        {
            if (name == nameof(ReloadBindingItems))
            {
                return useBinding;
            }

            return base.GetRunTimeMethodVisible(name);
        }

        public override string ToString()
        {
            return Resources.Transfer;
        }
    }

    public enum TargetOrder
    {
        [SRDescription(nameof(Resources.TransferCellTyle_original))]
        original,

        [SRDescription(nameof(Resources.TransferCellTyle_push))]
        push,

        [SRDescription(nameof(Resources.TransferCellTyle_unshift))]
        unshift,
    }

    public class TransferOptionItem
    {
        [SRDisplayName(nameof(Resources.Common_Value))]
        public string key { get; set; }

        [SRDisplayName(nameof(Resources.TransferCellTyle_label))]
        public string label { get; set; }

        [SRDisplayName(nameof(Resources.TransferCellTyle_disabled))]
        public bool disabled { get; set; }
    }
}