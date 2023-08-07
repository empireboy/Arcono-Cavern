using System;

namespace Arcono
{
	public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ArconoEnvironment())
                game.Run();
        }
    }
}
