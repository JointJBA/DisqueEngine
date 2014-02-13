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
using Microsoft.Win32;
using Disque.Raytracer.Rml;

namespace RenderToy
{
    /// <summary>
    /// Interaction logic for AddImage.xaml
    /// </summary>
    public partial class AddImage : Window
    {
        string file = "";
        public AddImage()
        {
            InitializeComponent();
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            bool? op = ofd.ShowDialog(this);
            if (op == true)
            {
                file = ofd.FileName;
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (file != "" && name.Text != "")
            {
                RmlReader.Images.Add(name.Text, Extensions.CreateFromBitmap(new System.Drawing.Bitmap(file)));
                Close();
            }
        }
    }
}
