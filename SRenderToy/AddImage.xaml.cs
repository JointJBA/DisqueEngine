using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

namespace SRenderToy
{
    public partial class AddImage : ChildWindow
    {
        MainPage mainPage;

        OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif" };

        Stream stream;

        RenderService.RenderServiceSoapClient rssc = new RenderService.RenderServiceSoapClient();

        string n;

        public string Name
        {
            get
            {
                return n;
            }
        }

        public AddImage(MainPage m)
        {
            mainPage = m;
            InitializeComponent();
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            bool? res = ofd.ShowDialog();
            if (res == true)
            {
                stream = ofd.File.OpenRead();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameText.Text != "")
            {
                rssc.HasNameAsync(nameText.Text);
                rssc.HasNameCompleted += new EventHandler<RenderService.HasNameCompletedEventArgs>(rssc_HasNameCompleted);
                disable();
            }
        }

        void rssc_HasNameCompleted(object sender, RenderService.HasNameCompletedEventArgs e)
        {
            if (!e.Result)
            {
                UploadFile(nameText.Text, stream);
            }
            else
            {
                MessageBox.Show("Name already used.");
                enable();
            }
        }

        void disable()
        {
            browse.IsEnabled = AddButton.IsEnabled = nameText.IsEnabled = false;
        }

        void enable()
        {
            browse.IsEnabled = AddButton.IsEnabled = nameText.IsEnabled = true;
        }

        void PushData(Stream input, Stream output)
        {
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) != 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

        void UploadFile(string fileName, Stream data)
        {
            n = fileName;
            string host = Application.Current.Host.Source.AbsoluteUri;
            host = host.Remove(host.IndexOf("/ClientBin"));
            UriBuilder ub = new UriBuilder(host + "/ImageRend.ashx");
            ub.Query = "image=" + fileName + "&typ=up&typ2=y";
            WebClient c = new WebClient();
            c.OpenWriteCompleted += (sender, e) =>
            {
                PushData(data, e.Result);
                e.Result.Close();
                data.Close();
            };
            c.WriteStreamClosed += (sender, e) => 
            {
                enable();
                try
                {
                    mainPage.names.Add(n);
                    mainPage.updateImageList();
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
                DialogResult = true;
            };
            c.OpenWriteAsync(ub.Uri);
        } 


    }
}

