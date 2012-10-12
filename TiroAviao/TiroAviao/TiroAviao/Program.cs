using System;

namespace TiroAviao
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //using (Game1 game = new Game1())
            //{
            //    game.Run();
            //}
            FormJogo form = new FormJogo();
            form.Show();
  
            
            GameXNAServidor game = new GameXNAServidor(form.getIntefaceSimulacao());
            form.game = game;
            game.Run();
        }
    }
#endif
}

