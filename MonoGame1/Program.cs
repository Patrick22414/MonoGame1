using System;

namespace MonoGame1
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using (var game = new MyGame())
            {
                game.Run();
            }
        }
    }
}