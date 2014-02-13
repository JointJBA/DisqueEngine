using System;
using Disque.Raytracer;
using Disque.Raytracer.Math;
using Microsoft.Xna.Framework;
using Disque.Math;

namespace xnatest
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
#endif
}

