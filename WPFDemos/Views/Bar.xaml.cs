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



namespace MyNamespace
{
    public partial class Bar : UserControl
    {
        public Bar()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public event EventHandler DeleteRequested;

        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            BorderBrush = Brushes.DeepSkyBlue;
            textBox.Visibility = Visibility.Visible;
            sortOfScoreboard.Visibility = Visibility.Visible;
        }

        public void OnLostFocus(object sender, RoutedEventArgs e)
        {
            BorderBrush = Brushes.Gray;
            textBox.Visibility = Visibility.Hidden;
            sortOfScoreboard.Visibility = Visibility.Hidden;
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var parentGrid = this.Parent as Grid;
            if (parentGrid != null)
            {
                parentGrid.Children.Remove(this);
                DeleteRequested?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}