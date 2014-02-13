using System;

namespace XFlightLite
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (FlightGame game = new FlightGame())
            {
                game.Run();
            }
        }
    }
#endif
}

