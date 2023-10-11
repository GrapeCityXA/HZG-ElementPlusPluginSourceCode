using GrapeCity.Forguncy.CellTypes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ElementUI.WpfControls.DrawingObject
{
    /// <summary>
    /// Interaction logic for BreadcrumbDrawingObject.xaml
    /// </summary>
    public partial class BreadcrumbDrawingObject : UserControl
    {
        public BreadcrumbDrawingObject(ICellInfo cellInfo, CellTypeViewModelStyle cellStyle, BreadcrumbCellType cellType)
        {
            var viewModel = new BreadcrumbDrawingObjectViewModel();
            viewModel.SeperateStr = cellType.separator;
            viewModel.SetItems(cellInfo.Value as string, cellStyle);
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }

    public class BreadcrumbDrawingObjectViewModel : ModelBase
    {
        public BreadcrumbDrawingObjectViewModel()
        {
        }

        public void SetItems(string text, CellTypeViewModelStyle cellStyle)
        {
            if (string.IsNullOrEmpty(text))
            {
                text = String.Join(this.SeperateStr, Properties.Resources.BreadcrumbCellType_NullPreview.Split('/'));
            }

            var strs = text.Split(new string[] { this.SeperateStr }, StringSplitOptions.RemoveEmptyEntries);
            var itemList = new List<Item>();

            for (var i = 0; i < strs.Length; i++)
            {
                var str = strs[i];
                var item = new Item() { Text = str, CellStyle = cellStyle.Clone() as CellTypeViewModelStyle };
                var isLast = i == strs.Length - 1;
                if (isLast)
                {
                    item.CellStyle.FontWeight = FontWeights.Normal;
                    item.CellStyle.Foreground = item.CellStyle.Foreground ?? ColorHelper.Text;
                }
                else
                {
                    item.CellStyle.FontWeight = FontWeights.Bold;
                    item.CellStyle.Foreground = item.CellStyle.Foreground ?? ColorHelper.DarkText;
                }
                itemList.Add(item);
                if (!isLast)
                {
                    var seperateItem = new Item() 
                    {
                        Text = SeperateStr,
                        CellStyle = cellStyle.Clone() as CellTypeViewModelStyle,
                        Margin = new Thickness(9, 0, 9, 0),
                    };
                    seperateItem.CellStyle.Foreground = item.CellStyle.Foreground ?? ColorHelper.BorderColorLightest;
                    seperateItem.CellStyle.FontWeight = FontWeights.Bold;

                    itemList.Add(seperateItem);
                }
            }

            this.Items = itemList;
        }

        public string SeperateStr { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string Text { get; set; }
        public Thickness Margin { get; set; }
        public CellTypeViewModelStyle CellStyle { get; set; }
    }
}