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
using Disque.Raytracer.Worlds;

namespace DisqueRenderToy
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public enum EditMode
    {
        Image,
        Animation
    }
    public partial class EditWindow : Window
    {
        EditMode currentMode;
        World world2render;
        public EditWindow(EditMode mode, World world)
        {
            if (mode == EditMode.Animation)
            {
                animation.Visibility = Visibility.Visible;
                image.Visibility = Visibility.Collapsed;
            }
            currentMode = mode;
            world2render = world;
            InitializeComponent();
        }
        public void Render()
        {
            if (currentMode == EditMode.Image)
            {
                pscreen.Render(world2render.Screen);
            }
        }
    }
}
