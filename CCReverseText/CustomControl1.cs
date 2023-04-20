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

namespace CCReverseText
{
    public class CustomControl1 : Control
    {
        static CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
        }

        TextBox txtNormal = null;
        TextBox txtReverse = null;

        public override void OnApplyTemplate()
        {
           txtNormal = GetTemplateChild("txtNormal") as TextBox;
           txtReverse = GetTemplateChild("txtReverse") as TextBox;

            txtNormal.TextChanged += TxtNormal_TextChanged;
        }

        private void TxtNormal_TextChanged(object sender, TextChangedEventArgs e)
        {
            string normalText = txtNormal.Text;
            txtReverse.Text = new string(normalText.ToCharArray().Reverse().ToArray());
        }
    }

}
