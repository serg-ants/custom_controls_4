using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SortOfScoreboard
{
    public class SortOfScoreboard : Control
    {



        static SortOfScoreboard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SortOfScoreboard), new FrameworkPropertyMetadata(typeof(SortOfScoreboard)));
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
    "Value", typeof(int), typeof(SortOfScoreboard), new PropertyMetadata(0, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int newValue = (int)e.NewValue;
            int maxValue = 50000;
            int minValue = -50000;

            if (newValue > maxValue)
            {
                ((SortOfScoreboard)d).Value = maxValue;
            }
            else if (newValue < minValue)
            {
                ((SortOfScoreboard)d).Value = minValue;
            }
        }


        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty IncreaseCommandProperty =
            DependencyProperty.Register(nameof(IncreaseCommand), typeof(ICommand), typeof(SortOfScoreboard), new PropertyMetadata(null));

        public ICommand IncreaseCommand
        {
            get => (ICommand)GetValue(IncreaseCommandProperty);
            set => SetValue(IncreaseCommandProperty, value);
        }

        public static readonly DependencyProperty DecreaseCommandProperty =
            DependencyProperty.Register(nameof(DecreaseCommand), typeof(ICommand), typeof(SortOfScoreboard), new PropertyMetadata(null));

        public ICommand DecreaseCommand
        {
            get => (ICommand)GetValue(DecreaseCommandProperty);
            set => SetValue(DecreaseCommandProperty, value);
        }

        public SortOfScoreboard()
        {
            IncreaseCommand = new RelayCommand(() => Value++);
            DecreaseCommand = new RelayCommand(() => Value--);
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute) : this(execute, null) { }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    
}
