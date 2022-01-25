using System;

namespace TryToRoguelike.Menus
{
    public class StartMenu : Menu
    {
        public override void Update()
        {
            switch (Console.ReadKey(true).KeyChar)
            {
                case '1':
                    Console.Clear();
                    Program.InitializeNewGame();
                    Program.GameMenu.Activate();
                    break;
                case '2':
                    Console.Clear();
                    Program.InitializeLoadGame();
                    Program.GameMenu.Activate();
                    break;
                case 'q':
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        public override void Draw()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Welcome to TryToRoguelike");
            Console.WriteLine();
            Console.WriteLine("1. New game");
            Console.WriteLine("2. Load game");
            Console.WriteLine("q. Exit");
        }
    }
}
