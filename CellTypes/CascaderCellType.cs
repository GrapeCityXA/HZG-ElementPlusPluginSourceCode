using ElementUI.WpfControls;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ElementUI.Properties;

namespace ElementUI
{
    public class CascaderCellTypeDesigner : CellTypeDesigner<CascaderCellType>, ISupportPropertyInitialize
    {
        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new InputBoxDrawingControl(cellInfo, drawingHelper, StyleHelper.CreateStyleViewModel(cellInfo, drawingHelper), this.CellType);
            return control;
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            var list = JsonConvert.DeserializeObject<List<TreeNode>>(Properties.Resources.DefaultTreeSource);
            this.CellType.options = list.OfType<ITreeNode>().ToList();
            this.CellType.placeholder = Resources.CascaderCellType_placeholder_DefaultValue;
            this.CellType.separator = "/";
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Cascader.png")]
    [OrderWeight((int)Utils.OrderWeight.Cascader)]
    [Designer("ElementUI.CascaderCellTypeDesigner, ElementUI")]
    public class CascaderCellType : InputCellTypeBase
    {
        #region Override基类属性，仅仅是为了确保属性顺序
        public override List<Command> CommandList { get => base.CommandList; set => base.CommandList = value; }
        public override DataValidationLink DataValidationLink { get => base.DataValidationLink; set => base.DataValidationLink = value; }
        public override List<UIPermission> UIPermissions { get => base.UIPermissions; set => base.UIPermissions = value; }
        public override object DefaultValue { get => base.DefaultValue; set => base.DefaultValue = value; }
        public override bool NeedFormatDefaultValue => false;

        #endregion

        [SRDisplayName(nameof(Resources.CascaderCellType_useBinding))]
        [BoolProperty]
        [MergableProperty(false)]
        public bool useBinding { get; set; } = false;

        [SRTreeProperty(IndentLevel = 1, NodeType = typeof(TreeNode), DefaultNodeName = nameof(Resources.CascaderCellType_options_DefaultNodeName))]
        [SRDisplayName(nameof(Resources.CascaderCellType_options))]
        public List<ITreeNode> options { get; set; } = new List<ITreeNode>();

        [SRBindingDataSourceProperty(Columns = nameof(Resources.CascaderCellType_bindingOptions_Columns), IndentLevel = 1, IsIdPidStructure = true)]
        [SRDisplayName(nameof(Resources.CascaderCellType_bindingOptions))]
        public object bindingOptions { get; set; }
        [SRDisplayName(nameof(Resources.CascaderCellType_expandTrigger))]
        public ExpendTrigger expandTrigger { get; set; } = ExpendTrigger.Click;

        [SRDisplayName(nameof(Resources.CascaderCellType_placeholder))]
        [SearchableProperty]
        public string placeholder { get; set; }

        [SRDisplayName(nameof(Resources.CascaderCellType_separator))]
        [SearchableProperty]
        public string separator { get; set; }

        [SRDisplayName(nameof(Resources.CascaderCellType_multiple))]
        [BoolProperty]
        public bool multiple { get; set; }

        [BoolProperty]
        [SRDisplayName(nameof(Resources.CascaderCellType_collapseTags))]
        public bool collapseTags { get; set; }

        [SRDisplayName(nameof(Resources.Cascader_TagType))]
        [SRComboProperty(ValueList = "success|info|warning|danger", DisplayList = nameof(Resources.Cascader_TagType_DisplayList))]
        public string tagType { get; set; } = "info";

        [SRCategoryHeader(nameof(Resources.Other))]
        [SRDisplayName(nameof(Resources.CascaderCellType_checkStrictly))]
        [SRDescription(nameof(Resources.CascaderCellType_checkStrictly_Description))]
        [DefaultValue(false)]
        public bool checkStrictly { get; set; } = false;

        [SRDisplayName(nameof(Resources.CascaderCellType_showAllLevels))]
        [DefaultValue(true)]
        public bool showAllLevels { get; set; } = true;

        [SRDisplayName(nameof(Resources.CascaderCellType_emitPath))]
        public bool emitPath { get; set; }

        [SRDisplayName(nameof(Resources.CascaderCellType_filterable))]
        public bool filterable { get; set; }

        [SRDisplayName(nameof(Resources.CascaderCellType_clearable))]
        public bool clearable { get; set; }

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
            if (propertyName == nameof(collapseTags))
            {
                return multiple;
            }
            if (propertyName == nameof(tagType))
            {
                return multiple;
            }          
            if (propertyName == nameof(emitPath))
            {
                return !multiple;
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }

        public override string ToString()
        {
            return Resources.Cascader;
        }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.CascaderCellType_SetDataSourceByObjTree))]
        [SRDescription(nameof(Resources.CascaderCellType_SetDataSourceByObjTree_Description))]
        public void SetDataSourceByObjTree(
                [SRItemDisplayName(nameof(Resources.CascaderCellType_dataSource))]
                [Required]
                string dataSource,
                [SRItemDisplayName(nameof(Resources.CascaderCellType_valueProperty))]
                [SRDescription(nameof(Resources.CascaderCellType_valueProperty_Description))]
                [Required]
                string valueProperty = "value",
                [SRItemDisplayName(nameof(Resources.CascaderCellType_labelProperty))]
                [SRDescription(nameof(Resources.CascaderCellType_labelProperty_Description))]
                string labelProperty = "label",
                [SRItemDisplayName(nameof(Resources.CascaderCellType_childrenProperty))]
                [SRDescription(nameof(Resources.CascaderCellType_childrenProperty_Description))]
                [Required]
                string childrenProperty = "children")
        {

        }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.CascaderCellType_SetDataSourceByIdPidTable))]
        [SRDescription(nameof(Resources.CascaderCellType_SetDataSourceByIdPidTable_Description))]
        public void SetDataSourceByIdPidTable(
                [SRItemDisplayName(nameof(Resources.CascaderCellType_dataSource))]
                [Required]
                string dataSource,
                [SRItemDisplayName(nameof(Resources.CascaderCellType_valueProperty))]
                [Required]
                string valueProperty = "value",
                [SRItemDisplayName(nameof(Resources.CascaderCellType_labelProperty))]
                [SRDescription(nameof(Resources.CascaderCellType_SetDataSourceByIdPidTable_labelProperty_Description))]
                string labelProperty = "label",
                [SRItemDisplayName(nameof(Resources.CascaderCellType_parentValue))]
                [Required]
                string parentValue = "parentValue")
        {

        }

        [SRDisplayName(nameof(Resources.CascaderCellType_GetCheckedNodes))]
        [RunTimeMethod]
        public GetCheckedNodesResult GetCheckedNodes(
            [SRItemDisplayName(nameof(Resources.CascaderCellType_leafOnly))]
            bool leafOnly = false)
        {
            return null;
        }

        [SRDisplayName(nameof(Resources.MethodName_ReloadBindingItems))]
        [SRDescription(nameof(Resources.MethodName_ReloadBindingItems_Description))]
        [RunTimeMethod]
        public void ReloadBindingItems()
        {
        }
        public override bool GetRunTimeMethodVisible(string name)
        {
            if (name == nameof(GetCheckedNodes))
            {
                return multiple;
            }
            if (name == nameof(ReloadBindingItems))
            {
                return useBinding;
            }
            return base.GetRunTimeMethodVisible(name);
        }

        public class GetCheckedNodesResult
        {
            [SRDisplayName(nameof(Resources.CascaderCellType_CheckedItems))]
            public object CheckedItems { get; set; }
        }
    }

    public enum ExpendTrigger
    {
        [SRDescription(nameof(Resources.CascaderCellType_Click))]
        Click,
        [SRDescription(nameof(Resources.CascaderCellType_Hover))]
        Hover
    }

    public class CascaderBindingTreeItem : ICloneable
    {
        [SRDisplayName(nameof(Resources.CascaderCellType_value))]
        public string value { get; set; }
        [SRDisplayName(nameof(Resources.CascaderCellType_label))]
        public string label { get; set; }
        [SRDisplayName(nameof(Resources.CascaderCellType_parentValue))]
        public string parentValue { get; set; }

        public object Clone()
        {
            return PluginUtilities.CommonClone(this);
        }
    }

    public class TreeNode : TreeNodeBase<TreeNode>
    {

    }

    public class TreeNodeBase<T> : ITreeNode, ICloneable where T : ITreeNode
    {
        [SRDisplayName(nameof(Resources.CascaderCellType_value))]
        [Required]
        public string value { get; set; }
        [SRDisplayName(nameof(Resources.CascaderCellType_label))]
        [Required]
        public string label { get; set; }

        public List<T> children { get; set; } = new List<T>();

        public bool ShouldSerializechildren()
        {
            return children.Count != 0;
        }

        string ITreeNode.Text
        {
            get
            {
                return label;
            }
            set
            {
                if (label == this.value)
                {
                    this.value = value;
                }
                label = value;
            }
        }

        IEnumerable<ITreeNode> ITreeNode.Children
        {
            get
            {
                return children as IEnumerable<ITreeNode>;
            }
            set
            {
                children = value.OfType<T>().ToList();
            }
        }

        public object Clone()
        {
            return PluginUtilities.CommonClone(this);
        }
    }
}
