using System;

namespace InteractivePiano
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new Game.InteractivePiano();
            game.Run();
        }
    }
}