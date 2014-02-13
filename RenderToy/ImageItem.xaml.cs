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

namespace RenderToy
{
    /// <summary>
    /// Interaction logic for ImageItem.xaml
    /// </summary>
    public partial class ImageItem : UserControl
    {
        ImageData iD;
        public ImageItem()
        {
            InitializeComponent();
        }

        public void SetImage(ImageData id)
        {
            iD = id;
            mainImage.Source = new BitmapImage(new Uri(id.Uri));
            text.Text += "Samples: " + id.NumSamples + "\nRender Time: " + id.RenderTime.ToString() + "\nRender Date: " + id.RenderDate.ToShortDateString();
        }

        public void Select()
        {
            text.Visibility = System.Windows.Visibility.Visible;
            Height = 200;
        }

        public void UnSelect()
        {
            text.Visibility = System.Windows.Visibility.Collapsed;
            Height = 150;
        }

    }
}
