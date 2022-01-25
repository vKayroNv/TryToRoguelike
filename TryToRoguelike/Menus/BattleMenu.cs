using System;
using System.Collections.Generic;
using System.Linq;
using TryToRoguelike.Entities;
using TryToRoguelike.GameElements;

namespace TryToRoguelike.Menus
{
    public class BattleMenu : Menu
    {
        private List<Enemy> _enemies;
        private Player _player;
        private Map _map;
        private int _x, _y;

        private readonly List<string> _log;
        private string _msg;

        private bool _isBattleActive;

        public BattleMenu()
        {
            _log = new List<string>();
        }

        public void Register(Player player, Map map)
        {
            _player = player;
            _map = map;
        }

        public override void Update()
        {
            if (_isBattleActive)
            {
                if (!_player.IsAlive)
                {
                    Console.SetCursorPosition(0, 4);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You lose");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("                                      ");
                    Console.ReadKey(true);
                    Program.GameOver();
                }
                else if (_enemies.Count == 0)
                {
                    Console.SetCursorPosition(0, 4);
                    Console.WriteLine("You win");
                    Console.Write("                                      ");
                    Console.ReadKey(true);

                    _player.Heal();
                    _map.Rooms[_x, _y].SetEnemyHere();
                    Deactivate();
                }
                else
                {
                    _log.Clear();

                    int input;
                    while (true)
                    {
                        try
                        {
                            input = int.Parse(Console.ReadKey(true).KeyChar.ToString());
                            break;
                        }
                        catch { }
                    }

                    if (input > 0 && input <= _enemies.Count)
                    {
                        _msg = _player.Attack(_enemies[input - 1]).ToString();
                        Log($"You attacked Enemy {input} for {_msg} damage ");

                        for (int i = _enemies.Count - 1; i >= 0; i--)
                        {
                            if (!_enemies[i].IsAlive)
                            {
                                _player.AddExp(_enemies[i].GetPower());
                                _player.AddScore(
                                    (int)Math.Round(Program.GetRandom(
                                        _enemies[i].GetPower() - _enemies[i].GetPower() / 2,
                                        _enemies[i].GetPower() - _enemies[i].GetPower() / 4)));
                                if (Program.GetRandom() < .3)
                                {
                                    _player.Inventory.AddItem(_enemies[i].GetLoot());
                                }
                                _map.MapEntities.Enemies.Remove(_enemies[i]);
                                _enemies.Remove(_enemies[i]);
                            }
                            if (_player.IsAlive && _enemies.Count != 0 && i < _enemies.Count)
                            {
                                _msg = _enemies[i].Attack(_player).ToString();
                                Log($"Enemy {input} attacked you for {_msg} damage ");
                            }
                        }
                    }
                }
            }
        }

        public override void Draw()
        {
            if (_isBattleActive)
            {
                Console.Clear();

                for (int i = 0; i < _enemies.Count; i++)
                {
                    Console.WriteLine(
                        $"Enemy {i + 1} (HP:{_enemies[i].HP}/{_enemies[i].MaxHP}) [" +
                        $"PWR:{ _enemies[i].GetPower()} " +
                        $"Armor:{_enemies[i].Armor} " +
                        $"DMG:{_enemies[i].Damage} " +
                        $"DC:{Math.Round(_enemies[i].DodgeChance * 100, 1)}% " +
                        $"BC:{Math.Round(_enemies[i].BlockChance * 100, 1)}% " +
                        $"CC:{Math.Round(_enemies[i].CritChance * 100, 1)}% " +
                        $"CI:{Math.Round(_enemies[i].CritIncreaser * 100, 1)}%]");
                }

                _player.DrawPlayerInfo(8, 2);

                Console.SetCursorPosition(0, 5);
                Console.Write("Choose enemy with numbers for attack");

                for (int i = _log.Count - 1; i >= 0; i--)
                {
                    Console.SetCursorPosition(40, 8 + _log.Count - 1 - i);
                    Console.Write(_log[i]);
                }
            }
        }

        public void CreateBattle(IEnumerable<Enemy> enemies, Player player, Map map)
        {
            _enemies = enemies.ToList();
            _player = player;
            _map = map;

            _isBattleActive = true;

            _x = _enemies[0].X;
            _y = _enemies[0].Y;
        }

        private void Log(string line)
        {
            _log.Add(line);
        }
    }
}
