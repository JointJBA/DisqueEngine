using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RenderToy
{
    public class FileManager
    {
        public string CurrentFileUri = "", CurrentFile = "";

        string title;

        public string Title
        {
            get
            {
                return title;
            }
        }

        public void Save()
        {
            File.WriteAllText(CurrentFileUri, CurrentFile);
            title = Path.GetFileNameWithoutExtension(CurrentFileUri);
        }

        public void Open(string uri)
        {
            CurrentFileUri = uri;
            CurrentFile = File.ReadAllText(uri);
            title = Path.GetFileNameWithoutExtension(CurrentFileUri);
        }
    }
}
