using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace RenderToy
{
    public static class Commands
    {
        public static RoutedUICommand Render = new RoutedUICommand("Renders an image", "Render", typeof(MWindow));
        public static RoutedUICommand AddImage = new RoutedUICommand("Adds an Image to be used by textures", "AddImage", typeof(MWindow));
        public static RoutedUICommand OpenDir = new RoutedUICommand("Open the file directory", "OpenDir", typeof(MWindow));
    }
}
