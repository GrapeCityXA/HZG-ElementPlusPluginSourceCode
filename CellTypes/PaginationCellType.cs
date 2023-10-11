using ElementUI.WpfControls;
using GrapeCity.Forguncy.CellTypes;
using GrapeCity.Forguncy.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ElementUI.Properties;
using System.ComponentModel.DataAnnotations;

namespace ElementUI.CellTypes
{
    public class PaginationCellTypeDesigner : CellTypeDesigner<PaginationCellType>, ISupportPropertyInitialize
    {
        public void InitDefaultPropertyValues(IBuilderContext context)
        {
            string[] defaults = "prev, pager, next, jumper, ->, total".Split(',').Select(i => i.Trim()).ToArray();
            foreach (var item in defaults)
            {
                this.CellType.layout.Add(new PageLayout()
                {
                    layoutItem = item
                });
            }
            var pageSizes = new List<int> { 10, 20, 30, 40, 50, 100 };
            foreach (var pageSize in pageSizes)
            {
                this.CellType.pageSizes.Add(new PageSize() { value = pageSize });
            }
            this.CellType.ListviewName = context.EnumAllListViewInfos(context.PageName).FirstOrDefault()?.ListViewName;
        }

        public override FrameworkElement GetDrawingControl(ICellInfo cellInfo, IDrawingHelper drawingHelper)
        {
            var cellStyle = StyleHelper.CreateStyleViewModel(cellInfo, drawingHelper);
            var control = new PaginationDrawingControl(cellStyle, this.CellType, drawingHelper);
            return control;
        }
    }

    [Icon("pack://application:,,,/ElementUI;component/Resources/Images/Pagination.png")]
    [OrderWeight((int)Utils.OrderWeight.Pagination)]
    [Designer("ElementUI.CellTypes.PaginationCellTypeDesigner, ElementUI")]
    public class PaginationCellType : ElementCellTypeBase, ISupportDisable, IPagingCellType
    {
        [SRCustomCommandObject(InitParamProperties = "currentPage|pageSize|total", InitParamValues = nameof(Resources.PaginationCellType_PagingChangeCommands_InitParamValues))]
        [SRDisplayName(nameof(Resources.PaginationCellType_PagingChangeCommands))]
        public object PagingChangeCommands { get; set; }

        [SRDisplayName(nameof(Resources.PaginationCellType_ListviewName))]
        [ListviewProperty]
        public string ListviewName { get; set; }

        [IntProperty(Min = 1)]
        [SRDisplayName(nameof(Resources.PaginationCellType_pageSize))]
        [DefaultValue(10)]
        public int pageSize { get; set; } = 10;

        private int _pagerCount = 7;
        [IntProperty(Min = 1)]
        [SRDisplayName(nameof(Resources.PaginationCellType_pagerCount))]
        [SRDescription(nameof(Resources.PaginationCellType_pagerCount_Description))]
        public int pagerCount
        {
            get
            {
                return _pagerCount;
            }
            set
            {
                _pagerCount = value;
                if (_pagerCount % 2 == 0)
                {
                    _pagerCount++;
                }
                if (_pagerCount < 5)
                {
                    _pagerCount = 5;
                }
                if (_pagerCount > 21)
                {
                    _pagerCount = 21;
                }
            }
        }

        [SRDisplayName(nameof(Resources.PaginationCellType_layout))]
        [ListProperty]
        public List<PageLayout> layout { get; set; } = new List<PageLayout>();

        [ListProperty]
        [SRDisplayName(nameof(Resources.PaginationCellType_pageSizes))]
        public List<PageSize> pageSizes { get; set; } = new List<PageSize>();

        [SRDisplayName(nameof(Resources.PaginationCellType_prevText))]
        public string prevText { get; set; }

        [SRDisplayName(nameof(Resources.PaginationCellType_nextText))]
        public string nextText { get; set; }

        [SRCategoryHeader(nameof(Resources.Other))]
        [SRDisplayName(nameof(Resources.PaginationCellType_small))]
        public bool small { get; set; }

        [SRDisplayName(nameof(Resources.PaginationCellType_background))]
        public bool background { get; set; }

        [SRDisplayName(nameof(Resources.PaginationCellType_hideOnSinglePage))]
        public bool hideOnSinglePage { get; set; }
        
        [SRDisplayName(nameof(Resources.PaginationCellType_IsDisabled))]
        public bool IsDisabled { get; set; }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.PaginationCellType_SetPageSize))]
        public void SetPageSize([Required][SRItemDisplayName(nameof(Resources.PaginationCellType_PageSize_Param))] int pageSize)
        {

        }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.PaginationCellType_SetCurrentPageIndex))]
        public void SetCurrentPageIndex([Required][SRItemDisplayName(nameof(Resources.PaginationCellType_PageIndex_Param))] int index)
        {

        }

        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.PaginationCellType_SetTotal))]
        public void SetTotal([Required][SRItemDisplayName(nameof(Resources.PaginationCellType_Total_Param))]int total)
        {

        }
        [RunTimeMethod]
        [SRDisplayName(nameof(Resources.PaginationCellType_ExecuteCommand))]
        public void ExecuteCommand()
        {

        }

        public override bool GetRunTimeMethodVisible(string name)
        {
            if (name == nameof(SetTotal))
            {
                return ListviewName == null;
            }
            return base.GetRunTimeMethodVisible(name);
        }

        public override string ToString()
        {
            return Resources.Pagination;
        }

        public override bool GetDesignerPropertyVisible(string propertyName)
        {
            if (propertyName == nameof(pagerCount) ||
                propertyName == nameof(background))
            {
                return this.layout.Any(i => i.layoutItem == "pager");
            }
            if (propertyName == nameof(pageSizes))
            {
                return this.layout.Any(i => i.layoutItem == "sizes");
            }
            if (propertyName == nameof(prevText))
            {
                return this.layout.Any(i => i.layoutItem == "prev");
            }
            if (propertyName == nameof(nextText))
            {
                return this.layout.Any(i => i.layoutItem == "next");
            }
            return base.GetDesignerPropertyVisible(propertyName);
        }
    }

    public class PageLayout : ObjectPropertyBase
    {
        [SRDisplayName(nameof(Resources.PaginationCellType_layoutItem))]
        [SRComboProperty(ValueList = "sizes|prev|pager|next|jumper|->|total", DisplayList = nameof(Resources.PaginationCellType_layoutItem_DisplayList))]
        public string layoutItem { get; set; } = "pager";
    }

    public class PageSize : ObjectPropertyBase
    {
        [SRDisplayName(nameof(Resources.PaginationCellType_value))]
        [IntProperty(Min = 1)]
        [DefaultValue(10)]
        public int value { get; set; } = 10;
    }
}
