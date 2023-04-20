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

        public SetOfBars()
        {
            InitializeComponent();
            ScrollViewer.SetCanContentScroll(AreaScrollViewer, false);
        }

        private void AddBar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbNumberOfBars.Text, out int numBars))
            {
                AddBars(numBars + 1);
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
            BarsPanel.RowDefinitions.Clear();
            BarsPanel.Children.Clear();
            for (int i = 0; i < numRows; i++)
            {
                BarsPanel.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < numBars; i++)
            {
                var bar = new Bar();
                bar.Text = $"Bar{i}";
                bar.sortOfScoreboard.Value = new Random().Next(1, 11);
                //bar.DeleteButtonClick += DeleteBar;
                bar.Width = 113;
                bar.Height = 59;
                int row = i / BarsPerRow;
                int col = i % BarsPerRow;
                Grid.SetRow(bar, row);
                Grid.SetColumn(bar, col);
                BarsPanel.Children.Add(bar);
            }

            var addNewButton = new Button();
            addNewButton.Content = "Add new";
            addNewButton.Click += AddBar_Click;
            addNewButton.Width = 113;
            addNewButton.Height = 59;
            int addNewIndex = numBars - 1;
            int addNewRow = addNewIndex / BarsPerRow;
            int addNewCol = addNewIndex % BarsPerRow;
            Grid.SetRow(addNewButton, addNewRow);
            Grid.SetColumn(addNewButton, addNewCol);
            BarsPanel.Children.Add(addNewButton);

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