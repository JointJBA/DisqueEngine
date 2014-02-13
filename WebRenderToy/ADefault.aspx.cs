using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Disque.Raytracer.Worlds;
using Disque.Raytracer.Rml;
using System.ComponentModel;
using System.Diagnostics;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using Disque;

namespace WebRenderToy
{
    public partial class Default : System.Web.UI.Page
    {
        World world;

        BackgroundWorker worker = new BackgroundWorker();

        string saveDirectory;

        Stopwatch renderTimer = new Stopwatch();

        protected void Page_Load(object sender, EventArgs e)
        {
            worker.DoWork +=new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            renderTimer.Stop();
            writeImage(RenderImage(world), renderTimer.Elapsed, DateTime.Now, world.ViewPlane.NumSamples);
            updateImageList();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            renderTimer.Reset();
            renderTimer.Start();
            world.RenderScene();
        }

        protected void render_Click(object sender, EventArgs e)
        {
            try
            {
                Disque.Raytracer.GeometricObjects.GeometricObject.Clear();
                world = RmlReader.Compile(textEditor.Text.Replace('[', '<').Replace(']', '>'), ref saveDirectory);
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        byte[] RenderImage(World screen)
        {
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(screen.ViewPlane.HRes, screen.ViewPlane.VRes);
            for (int i = 0; i < screen.Screen.Pixels.Count; i++)
            {
                Pixel p = screen.Screen.Pixels[i];
                bm.SetPixel((int)p.X, (int)p.Y, System.Drawing.Color.FromArgb((int)(MathHelper.Clamp(p.Color.R, 0, 1.0f) * 255.0f), (int)(MathHelper.Clamp(p.Color.G, 0, 1.0f) * 255.0f), (int)(MathHelper.Clamp(p.Color.B, 0, 1.0f) * 255.0f)));
            }
            return (byte[])new ImageConverter().ConvertTo(bm, typeof(byte[]));
        }

        void updateImageList()
        {
            /*
            imageList.Items.Clear();
            SqlCommand cmnd = new SqlCommand("SELECT * FROM Image", DataBaseHandler.ImageConnection);
            DataBaseHandler.ImageConnection.Open();
            SqlDataReader sdr = cmnd.ExecuteReader();
            while (sdr.Read())
            {
                ImageItem ii = new ImageItem();
                ii.SetData("~/ImageHandler.ashx?id=" + sdr[0], sdr[3].ToString(), sdr[4].ToString(), sdr[2].ToString());
                imageList.Items.Add(new ListItem(ii);
            }
             * */
        }

        void writeImage(byte[] image, TimeSpan rtime, DateTime rdate, int samples)
        {
            DataBaseHandler.ImageConnection.Open();
            SqlCommand cmnd = new SqlCommand("INSERT INTO Image (Image,Samples,RenderTime,RenderDate,IPAddress,Code) VALUES (@img,@samples,@rt,@rd,@ip,@c)", DataBaseHandler.ImageConnection);
            cmnd.Parameters.Add("@img", System.Data.SqlDbType.Image).Value = image;
            cmnd.Parameters.Add("@samples", System.Data.SqlDbType.Int).Value = samples;
            cmnd.Parameters.Add("@rt", System.Data.SqlDbType.Text).Value = rtime.ToString();
            cmnd.Parameters.Add("@rd", System.Data.SqlDbType.Text).Value = rdate.ToShortDateString();
            cmnd.Parameters.Add("@ip", System.Data.SqlDbType.Text).Value = HttpContext.Current.Request.UserHostAddress.ToString();
            cmnd.Parameters.Add("@c", System.Data.SqlDbType.Text).Value = textEditor.Text;
            cmnd.ExecuteNonQuery();
            DataBaseHandler.ImageConnection.Close();
        }
    }
}