using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.ComponentModel;
using System.Collections.ObjectModel;
using WPFDemos.ViewModels;


using System.Threading.Tasks;

using System.Runtime.CompilerServices;


/// <summary>
/// Interaction logic for SetOfBars.xaml
namespace MyNamespace
{
    public partial class SetOfBars : UserControl
    {
        private const int BarsPerRow = 4;
        private int totalNumBars;
        private int rowAddNew;
        private int colAddNew;

        private Grid grid;
        private Button addNewButton;
        private Bar temp = new Bar();

        public SetOfBars()
        {
            InitializeComponent();
            ScrollViewer.SetCanContentScroll(AreaScrollViewer, false);
        }

        private void AddBar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbNumberOfBars.Text, out int numBars))
            {
                totalNumBars = numBars;
                AddBars(numBars);
            }
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            var bar = new Bar();
            bar.Text = $"Bar";
            bar.sortOfScoreboard.Value = new Random().Next(1, 11);
            bar.Width = 113;
            bar.Height = 59;
            bar.Margin = new Thickness(5);
           
            //bar.DeleteButtonClick += DeleteBar;

            Grid.SetRow(bar, rowAddNew);
            Grid.SetColumn(bar, colAddNew);
            grid.Children.Add(bar);
            bar.OnMouseDown(sender,null);
            temp.OnLostFocus(sender,null);
            temp = bar;

            if (colAddNew == BarsPerRow-1)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
              
                //grid.RowDefinitions.Add(new RowDefinition());
                rowAddNew++;
                colAddNew = 0;
                totalNumBars++;
                Grid.SetRow(addNewButton, rowAddNew);
                Grid.SetColumn(addNewButton, 0);
            }
            else
            {
                colAddNew++;
                totalNumBars++;
                Grid.SetRow(addNewButton, rowAddNew);
                Grid.SetColumn(addNewButton, colAddNew);
            }

        }

       

        private void DeleteBar(object sender, RoutedEventArgs e)
        {
            var bar = ((Button)sender).DataContext as Bar;
            if (bar != null)
            {
                int index = BarsPanel.Children.IndexOf(bar);
                BarsPanel.Children.Remove(bar);
                if (index != -1)
                {
                    for (int i = index; i < BarsPanel.Children.Count; i++)
                    {
                        if (BarsPanel.Children[i] is Bar nextBar)
                        {
                            nextBar.Text = $"Bar{i}";
                        }
                    }
                }
                UpdateTotalCount();
            }
        }

        private void AddBars(int numBars)
        {
           
            int numRows = (numBars - 1) / BarsPerRow + 1;
            numRows += (numBars % BarsPerRow == 0) ? 1 : 0;

            BarsPanel.Children.Clear();

            grid = new Grid();
            
            for (int i = 0; i < numRows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            }
            for (int i = 0; i < BarsPerRow; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            int barIndex = 0;
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < BarsPerRow && barIndex < numBars; col++)
                {
                    var bar = new Bar();
                    bar.Text = $"Bar{barIndex}";
                    bar.sortOfScoreboard.Value = new Random().Next(1, 11);
                    bar.Width = 113;
                    bar.Height = 59;
                    bar.Margin = new Thickness(5);
                    //bar.DeleteButtonClick += DeleteBar;

                    Grid.SetRow(bar, row);
                    Grid.SetColumn(bar, col);
                    grid.Children.Add(bar);

                    barIndex++;
                }
            }

            BarsPanel.Children.Add(grid);


            addNewButton = new Button();
            addNewButton.Content = "Add new";
            addNewButton.Click += AddNew_Click;
            addNewButton.Width = 113;
            addNewButton.Height = 59;
            addNewButton.Margin = new Thickness(5);
            int addNewIndex = numBars;
            int addNewRow = (int) Math.Floor((double) (addNewIndex / BarsPerRow));
            int addNewCol = addNewIndex % BarsPerRow;
            if (addNewCol == 0)
            {
                addNewRow++;
                Grid.SetRow(addNewButton, addNewRow);
                Grid.SetColumn(addNewButton, 0);
                rowAddNew = addNewRow;
                colAddNew = 0;
            }
            else
            {
                addNewRow++;
                Grid.SetRow(addNewButton, addNewRow);
                Grid.SetColumn(addNewButton, addNewCol);
                rowAddNew = addNewRow;
                colAddNew = addNewCol;
            }
            grid.Children.Add(addNewButton);

            UpdateTotalCount();
        }


        private void UpdateTotalCount()
        {
            int totalCount = BarsPanel.Children.OfType<Bar>().Sum(b => b.sortOfScoreboard.Value);
            tbTotalCount.Text = $"Total count: {totalCount}";
        }

        private void NumBarsTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (int.TryParse(tbNumberOfBars.Text, out int numBars))
                {
                    AddBars(numBars);
                }
            }
        }
    }
}