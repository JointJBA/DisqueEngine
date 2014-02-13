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
using System.Windows.Shapes;

namespace DisqueRenderToy
{
    /// <summary>
    /// Interaction logic for NewLightWindow.xaml
    /// </summary>
    public enum Type
    {
        Camera, Light
    }
    public partial class NewLightWindow : Window
    {
        Type type;
        public NewLightWindow(Type newtype)
        {
            InitializeComponent();
            type = newtype;
            if (type == Type.Light)
                new_light_grid.Visibility = Visibility.Visible;
        }

        private void cancelbutton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void okbutton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
