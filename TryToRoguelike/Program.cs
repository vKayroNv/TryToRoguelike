using System;
using TryToRoguelike.Entities;
using TryToRoguelike.GameElements;
using TryToRoguelike.Menus;
using TryToRoguelike.Models;

namespace TryToRoguelike
{
    internal class Program
    {
        public const string wall = "\u2588\u2588";
        public const string enemy = "><";
        public const string player = "()";
        public const string chest = "[]";
        public const string space = "  ";

        private static Random rnd;
        private static BattleMenu _battleMenu;
        private static InventoryMenu _inventoryMenu;
        private static LevelUpMenu _levelUpMenu;
        private static GameMenu _gameMenu;
        private static StartMenu _startMenu;

        private static Player _player;
        private static Map _map;
        private static MapEntities _mapEntities;

        private static bool _isNewGame;

        public static BattleMenu BattleMenu { get => _battleMenu; }
        public static InventoryMenu InventoryMenu { get => _inventoryMenu; }
        public static LevelUpMenu LevelUpMenu { get => _levelUpMenu; }
        public static GameMenu GameMenu { get => _gameMenu; }
        public static StartMenu StartMenu { get => _startMenu; }

        public static bool IsNewGame { get => _isNewGame; }

        public static double GetRandom()
        {
            return rnd.NextDouble();
        }
        public static double GetRandom(double min, double max)
        {
            return (max - min) * rnd.NextDouble() + min;
        }
        public static int GetIntRandom(int min, int max)
        {
            return rnd.Next(min, max);
        }

        public static void GameOver()
        {
            Console.Clear();
            Console.WriteLine("\n\n\nGAME OVER");
            Console.WriteLine($"Your score {GameMenu.Player.Score}");

            Reset();

            Console.ReadKey(true);
            Main(null);
        }

        public static void InitializeNewGame()
        {
            _isNewGame = true;

            _player = new();
            _map = new();
            _mapEntities = new();

            InitializeRegistration();
        }

        public static void InitializeLoadGame()
        {
            _isNewGame = false;

            SaveFileModel model = SaveLoadModule.Load();

            _player = PlayerModel.FromModelToObj(model.Player);
            _map = MapModel.FromModelToObj(model.Map);
            _mapEntities = new();

            InitializeRegistration();

            _isNewGame = true;
        }

        public static void InitializeNewLevel()
        {
            _mapEntities = new();
            _map = new();

            _player.Register(_map);
            _mapEntities.Register(_player);
            _map.Register(_player, _mapEntities);

            _battleMenu.Register(_player, _map);
            _gameMenu.Register(_map, _player, _mapEntities);

            _gameMenu.RenewGraphics();
        }

        public static void Reset()
        {
            _player = null;
            _map = null;
            _mapEntities = null;

            _startMenu = null;
            _gameMenu = null;
            _battleMenu = null;
            _inventoryMenu = null;
            _levelUpMenu = null;

            GC.Collect(3);
        }

        private static void Initialize()
        {
            rnd = new Random();

            _startMenu = new();
        }

        private static void InitializeRegistration()
        {
            _player.Register(_map);
            _mapEntities.Register(_player);
            _map.Register(_player, _mapEntities);

            _battleMenu = new();
            _inventoryMenu = new();
            _levelUpMenu = new();
            _gameMenu = new();

            _battleMenu.Register(_player, _map);
            _inventoryMenu.Register(_player);
            _levelUpMenu.Register(_player);
            _gameMenu.Register(_map, _player, _mapEntities);
        }

        private static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Initialize();

            _startMenu.Activate();
        }
    }
}
