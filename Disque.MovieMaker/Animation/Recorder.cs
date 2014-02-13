using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Worlds;
using System.Threading;
using System.Threading.Tasks;

namespace Disque.MovieMaker.Animation
{
    public delegate void FrameCompletedEventHandler(object sender, FrameCompletedEventArgs e);
    public class Recorder
    {
        World world;
        int nframes;
        List<PixelScreen> frames = new List<PixelScreen>();
        public Recorder(World w, int number_of_frames)
        {
            world = w;
            nframes = number_of_frames;
        }
        public List<PixelScreen> Frames
        {
            get
            {
                return frames;
            }
        }
        public event FrameCompletedEventHandler NextFrame;
        public event FrameCompletedEventHandler RenderingCompleted;
        void fireNextFrame(FrameCompletedEventArgs e)
        {
            if (NextFrame != null)
                NextFrame(this, e);
        }
        void fireRenderCompleted(FrameCompletedEventArgs e)
        {
            if (RenderingCompleted != null)
                RenderingCompleted(this, e);
        }
        public void Start(bool stereo)
        {
            Task th;
            if (!stereo)
                th = new Task(new Action(render));
            else
                th = new Task(new Action(renderStereo));
            th.Start();
        }
        void render()
        {
            for (int i = 0; i < nframes; i++)
            {
                PixelScreen ps = new PixelScreen(world.ViewPlane);
                world.Screen = ps;
                world.RenderScene();
                frames.Add(ps);
                FrameCompletedEventArgs e = new FrameCompletedEventArgs();
                e.FinishedFrame = ps;
                e.FinishedFrameNumber = i + 1;
                fireNextFrame(e);
            }
            fireRenderCompleted(new FrameCompletedEventArgs());
        }
        void renderStereo()
        {
        }
    }
    public class FrameCompletedEventArgs : EventArgs
    {
        public int FinishedFrameNumber;
        public PixelScreen FinishedFrame;
    }
}
