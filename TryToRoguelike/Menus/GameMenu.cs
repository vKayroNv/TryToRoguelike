using System;
using TryToRoguelike.Entities;
using TryToRoguelike.GameElements;

namespace TryToRoguelike.Menus
{
    public class GameMenu : Menu
    {
        private Map _map;
        private Player _player;
        private MapEntities _mapEntities;

        public Map Map { get => _map; }
        public Player Player { get => _player; }
        public MapEntities MapEntities { get => _mapEntities; }

        public void Register(Map map, Player player, MapEntities mapEntities)
        {
            _map = map;
            _player = player;
            _mapEntities = mapEntities;
        }

        public void RenewGraphics()
        {
            Console.Clear();

            foreach (var room in _map.Rooms)
            {
                room.RenewGraphics();
            }
        }

        public override void Update()
        {
            if (_mapEntities.Enemies.Count == 0 && _mapEntities.Chests.Count == 0)
            {
                Program.InitializeNewLevel();
            }
            else
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        _player.MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        _player.MoveDown();
                        break;
                    case ConsoleKey.LeftArrow:
                        _player.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        _player.MoveRight();
                        break;
                    case ConsoleKey.I:
                        Program.InventoryMenu.Activate();
                        RenewGraphics();
                        break;
                    case ConsoleKey.L:
                        Program.LevelUpMenu.Activate();
                        RenewGraphics();
                        break;
                    case ConsoleKey.S:
                        SaveLoadModule.Save();
                        Console.Clear();
                        Console.WriteLine("Game saved");
                        Console.ReadKey(true);
                        RenewGraphics();
                        break;
                    case ConsoleKey.Q:
                        Deactivate();
                        break;
                    default:
                        break;
                }
            }
        }

        public override void Draw()
        {
            _map.Draw();
            DrawStaticText();
            _player.DrawPlayerInfo(18, 2);
        }

        private static void DrawStaticText()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int x = 1, y = 65;

            Console.SetCursorPosition(y, x++);
            Console.Write("Objects:");
            Console.SetCursorPosition(y, x++);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("player - ()");
            Console.SetCursorPosition(y, x++);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("enemy - ><");
            Console.SetCursorPosition(y, x++);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("chest - []");
            Console.SetCursorPosition(y, x++);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("wall - \u2588\u2588");
            Console.ForegroundColor = ConsoleColor.White;

            x = 1; y = 78;

            Console.SetCursorPosition(y, x++);
            Console.Write("Controls:");
            Console.SetCursorPosition(y, x++);
            Console.Write("arrows - movement");
            Console.SetCursorPosition(y, x++);
            Console.Write("i - inventory");
            Console.SetCursorPosition(y, x++);
            Console.Write("l - skills upgrade");
            Console.SetCursorPosition(y, x++);
            Console.Write("s - save");
            Console.SetCursorPosition(y, x++);
            Console.Write("q - exit");
        }
    }
}
