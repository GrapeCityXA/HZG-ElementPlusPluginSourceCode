using ElementUI.WpfControls;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Commands;
using GrapeCity.Forguncy.Plugin;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using ElementUI.Properties;

namespace ElementUI
{
    public class StepsCellTypeDesigner : CellTypeDesigner<StepsCellType>, ISupportPropertyInitialize
    {

        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var control = new StepsDrawingControl(drawingHelper, this.CellType);

            return control;
        }

        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            CellType.options.Add(new StepsOptionlItem()
            {
                value = "1",
                title = Resources.StepsCellType_step1,
                description = Resources.StepsCellType_step1_Description,
            });

            CellType.options.Add(new StepsOptionlItem()
            {
                value = "2",
                title = Resources.StepsCellType_step2,
                description = Resources.StepsCellType_step2_Description,
            });
            CellType.options.Add(new StepsOptionlItem()
            {
                value = "3",
                title = Resources.StepsCellType_step3,
                description = Resources.StepsCellType_step3_Description,
            });
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Steps.png")]
    [OrderWeight((int)Utils.OrderWeight.Steps)]
    [Designer("ElementUI.StepsCellTypeDesigner, ElementUI")]
    public class StepsCellType : ElementCellTypeBase, ICommandCellType
    {
        [SRDisplayName(nameof(Resources.Common_ValueChangeCommand))]
        public List<Command> CommandList { get; set; } = new List<Command>();

        public CommandExcuteKind CommandExcuteKind => CommandExcuteKind.OnValueChanged;

        [SRDisplayName(nameof(Resources.StepsCellType_useBinding))]
        [BoolProperty]
        [MergableProperty(false)]
        public bool useBinding { get; set; } = false;

        [SRDisplayName(nameof(Resources.StepsCellType_options))]
        [ListProperty(IndentLevel = 1)]
        public List<StepsOptionlItem> options { get; set; } = new List<StepsOptionlItem>();

        [SRBindingDataSourceProperty(Columns = nameof(Resources.StepsCellType_bindingOptions_Columns), IndentLevel = 1)]
        [SRDisplayName(nameof(Resources.StepsCellType_bindingOptions))]
        public object bindingOptions { get; set; }

        [SRDisplayName(nameof(Resources.StepsCellType_processStatus))]
        [DefaultValue(Status.process)]
        public Status processStatus { get; set; } = Status.process;

        [SRDisplayName(nameof(Resources.StepsCellType_finishStatus))]
        [DefaultValue(Status.finish)]
        public Status finishStatus { get; set; } = Status.finish;

        [SRDisplayName(nameof(Resources.layout))]
        [SRRadioGroupProperty( ValueList ="horizontal|vertical", DisplayList = nameof(Resources.layout_DisplayList))]
        public string layout { get; set; } = "horizontal";

        [SRDisplayName(nameof(Resources.StepsCellType_simple))]
        [BoolProperty]
        public bool simple { get; set; }

        [SRDisplayName(nameof(Resources.StepsCellType_alignCenter))]
        [BoolProperty]
        public bool alignCenter { get; set; }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.StepsCellType_UpdateProcessState))]
        public void UpdateProcessState(
            [SRItemDisplayName(nameof(Resources.StepsCellType_State))]
            Status state)
        {

        }

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(options)) {
                return !useBinding;
            }
            if (propertyName == nameof(bindingOptions))
            {
                return useBinding;
            }
            if (propertyName == nameof(simple))
            {
                return layout != "vertical";
            }
            if (propertyName == nameof(alignCenter))
            {
                return layout != "vertical" && !simple;
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
            return Resources.Steps;
        }
    }
    public class StepsOptionlItem
    {
        [SRDisplayName(nameof(Resources.StepsCellType_value))]
        public string value { get; set; }

        [SRDisplayName(nameof(Resources.StepsCellType_title))]
        public string title { get; set; }

        [SRDisplayName(nameof(Resources.StepsCellType_description))]
        public string description { get; set; }

        private object _icon;

        [SRDisplayName(nameof(Resources.StepsCellType_icon))]
        [IconProperty(DefaultUseCellColor = true, SupportUseCellColor = true)]
        public object icon
        {
            get
            {
                return _icon;
            }
            set
            {
                if (value is ImageValue || value is null)
                {
                    _icon = value;
                }
            }
        }
    }

    public enum Status
    {
        [SRDescription(nameof(Resources.StepsCellType_wait))]
        wait,
        [SRDescription(nameof(Resources.StepsCellType_process))]
        process,
        [SRDescription(nameof(Resources.StepsCellType_finish))]
        finish,
        [SRDescription(nameof(Resources.StepsCellType_error))]
        error,
        [SRDescription(nameof(Resources.StepsCellType_success))]
        success,
    }
}
