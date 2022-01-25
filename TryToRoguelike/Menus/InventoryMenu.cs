using System;
using TryToRoguelike.Entities;
using TryToRoguelike.Items;

namespace TryToRoguelike.Menus
{
    public class InventoryMenu : Menu
    {
        private Player _player;
        private int _page;

        public InventoryMenu()
        {
            _page = 0;
        }

        public void Register(Player player)
        {
            _player = player;
        }

        public override void Update()
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (_page > 0)
                    {
                        _page--;
                    }

                    break;
                case ConsoleKey.RightArrow:
                    if (_page < Math.Round(_player.Inventory.Items.Count / 10d) - 1)
                    {
                        _page++;
                    }

                    break;
                case ConsoleKey.Q:
                    Deactivate();
                    break;
                default:
                    if ((int)key >= 48 && (int)key < 58)
                    {
                        ShowItemInfo((int)key - 48);
                    }
                    else if ((int)key >= 96 && (int)key < 106)
                    {
                        ShowItemInfo((int)key - 96);
                    }
                    break;
            }
        }

        public override void Draw()
        {
            var equpped = _player.Inventory.Equpped;

            Console.Clear();

            Console.WriteLine($"Helmet:\t\t{(equpped["helmet"] != null ? equpped["helmet"] : "empty")}");
            Console.WriteLine($"Cuirass:\t{(equpped["cuirass"] != null ? equpped["cuirass"] : "empty")}");
            Console.WriteLine($"Pants:\t\t{(equpped["pants"] != null ? equpped["pants"] : "empty")}");
            Console.WriteLine($"Boots:\t\t{(equpped["boots"] != null ? equpped["boots"] : "empty")}");
            if (equpped["left_hand"] != null)
            {
                if (equpped["left_hand"].GetType() == typeof(Daggers) ||
                    equpped["left_hand"].GetType() == typeof(TwoHandedSword))
                {
                    Console.WriteLine($"Two Hands:\t{(equpped["left_hand"])}");
                }
                else
                {
                    Console.WriteLine($"Left Hand:\t{(equpped["left_hand"] != null ? equpped["left_hand"] : "empty")}");
                    Console.WriteLine($"Right Hand:\t{(equpped["right_hand"] != null ? equpped["right_hand"] : "empty")}");
                }
            }
            else
            {
                Console.WriteLine($"Left Hand:\t{(equpped["left_hand"] != null ? equpped["left_hand"] : "empty")}");
                Console.WriteLine($"Right Hand:\t{(equpped["right_hand"] != null ? equpped["right_hand"] : "empty")}");
            }

            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Inventory");
            if (_player.Inventory.Items.Count == 0)
            {
                Console.WriteLine("Here is empty");
            }
            else
            {
                for (int i = _page * 10; i < 10 * (_page + 1); i++)
                {
                    if (_player.Inventory.Items.Count == i)
                    {
                        break;
                    }

                    var item = _player.Inventory.Items[i];
                    Console.WriteLine($"{i - _page * 10}. {item.GetType().ToString().Remove(0, item.GetType().ToString().LastIndexOf('.') + 1)}");
                }
            }
            Console.SetCursorPosition(0, 20);
            Console.Write($"Page {_page + 1}/{_player.Inventory.Items.Count / 10 + 1}");
            Console.SetCursorPosition(0, 22);
            Console.Write("left arrow - previous page\nright arrow - next page\nnumber - get item info\nq - back");
        }

        private void ShowItemInfo(int index)
        {
            Console.SetCursorPosition(25, 8);

            if (index + 10 * _page < _player.Inventory.Items.Count)
            {
                var item = _player.Inventory.Items[index + 10 * _page];

                Console.SetCursorPosition(30, Console.CursorTop + 1);
                Console.Write($"Power:{item.GetPower()}");
                Console.SetCursorPosition(30, Console.CursorTop + 1);
                Console.Write($"HP:{item.HP}");
                Console.SetCursorPosition(30, Console.CursorTop + 1);
                Console.Write($"Armor:{item.Armor}");
                Console.SetCursorPosition(30, Console.CursorTop + 1);
                Console.Write($"Damage:{item.Damage}");
                Console.SetCursorPosition(30, Console.CursorTop + 1);
                Console.Write($"Dodge chance:{Math.Round(item.DodgeChance * 100, 1)}%");
                Console.SetCursorPosition(30, Console.CursorTop + 1);
                Console.Write($"Block Chance:{Math.Round(item.BlockChance * 100, 1)}%");
                Console.SetCursorPosition(30, Console.CursorTop + 1);
                Console.Write($"Crit chance:{Math.Round(item.CritChance * 100, 1)}%");
                Console.SetCursorPosition(30, Console.CursorTop + 1);
                Console.Write($"Crit increaser:{Math.Round(item.CritIncreaser * 100, 1)}%");
                Console.SetCursorPosition(30, Console.CursorTop + 2);
                Console.Write("Press ENTER for equip or q for drop");

                var input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.Enter)
                {
                    _player.Inventory.EquipItem(index + 10 * _page);
                }
                else if (input == ConsoleKey.Q)
                {
                    _player.Inventory.DropItem(index + 10 * _page);
                }
            }
        }
    }
}