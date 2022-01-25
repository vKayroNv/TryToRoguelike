using System;
using System.Linq;
using TryToRoguelike.GameElements;

namespace TryToRoguelike.Entities
{
    public class Player : Entity
    {
        private int _exp, _score, _skillPoints;
        private Map _map;

        public int Exp { get => _exp; }
        public int Score { get => _score; }
        public int SkillPoints { get => _skillPoints; }

        public Player()
        {
            _level = 1;
            _exp = 0;
            _score = 0;
            _skillPoints = 5;
            _hp = _max_hp = 100;
            _armor = 0;
            _damage = 10;
            _dodge_chance = .05;
            _block_chance = .05;
            _crit_chance = .05;
            _crit_increaser = .2;
        }

        public Player(
            int x,
            int y,
            int level,
            int exp,
            int score,
            int skillPoints,
            int max_hp,
            int armor,
            int damage,
            double dodge_chance,
            double block_chance,
            double crit_chance,
            double crit_increaser,
            bool is_alive,
            Inventory inventory)
        {
            _x = x;
            _y = y;
            _level = level;
            _exp = exp;
            _score = score;
            _skillPoints = skillPoints;
            _hp = _max_hp = max_hp;
            _armor = armor;
            _damage = damage;
            _dodge_chance = dodge_chance;
            _block_chance = block_chance;
            _crit_chance = crit_chance;
            _crit_increaser = crit_increaser;
            _is_alive = is_alive;
            _inventory = inventory;

            _inventory.SetEntity(this);
        }

        public void Register(Map map)
        {
            _map = map;
        }

        public void AddExp(int value)
        {
            _exp += value;

            if (_exp >= _level * 1000)
            {
                _exp -= _level++ * 1000;
                _skillPoints += 2;
                _inventory.IncreaseMaxSize();
            }
        }
        public void AddScore(int value)
        {
            _score += value;
        }

        public void SetPosition(int x, int y)
        {
            _x = x; _y = y;
        }

        public void MoveUp()
        {
            if (_map.Rooms[_x, _y].Neighbours[0] != null)
            {
                if (_map.Rooms[_x, _y].Neighbours[0].IsEnemyHere)
                {
                    GetEnemy(-1, 0);
                }
                else
                {
                    GetChest(0, -1, 0);
                    _map.Rooms[_x--, _y].SetPlayerHere();
                    _map.Rooms[_x, _y].SetPlayerHere();
                }
            }
        }
        public void MoveDown()
        {
            if (_map.Rooms[_x, _y].Neighbours[2] != null)
            {
                if (_map.Rooms[_x, _y].Neighbours[2].IsEnemyHere)
                {
                    GetEnemy(1, 0);
                }
                else
                {
                    GetChest(2, 1, 0);
                    _map.Rooms[_x++, _y].SetPlayerHere();
                    _map.Rooms[_x, _y].SetPlayerHere();
                }
            }
        }
        public void MoveLeft()
        {
            if (_map.Rooms[_x, _y].Neighbours[3] != null)
            {
                if (_map.Rooms[_x, _y].Neighbours[3].IsEnemyHere)
                {
                    GetEnemy(0, -1);
                }
                else
                {
                    GetChest(3, 0, -1);
                    _map.Rooms[_x, _y--].SetPlayerHere();
                    _map.Rooms[_x, _y].SetPlayerHere();
                }
            }
        }
        public void MoveRight()
        {
            if (_map.Rooms[_x, _y].Neighbours[1] != null)
            {
                if (_map.Rooms[_x, _y].Neighbours[1].IsEnemyHere)
                {
                    GetEnemy(0, 1);
                }
                else
                {
                    GetChest(1, 0, 1);
                    _map.Rooms[_x, _y++].SetPlayerHere();
                    _map.Rooms[_x, _y].SetPlayerHere();
                }
            }
        }

        public void Heal()
        {
            _hp = _max_hp;
        }

        public void GetMapData(Map map) => _map = map;

        public void DrawPlayerInfo(int x, int y)
        {
            Console.SetCursorPosition(y, x++);
            Console.Write("Player info");
            Console.SetCursorPosition(y, x++);
            Console.Write($"Power:\t\t{GetPower()}");
            Console.SetCursorPosition(y, x++);
            Console.Write($"Level:\t\t{_level} ({_exp}/{_level * 1000})");
            Console.SetCursorPosition(y, x++);
            Console.Write($"HP:\t\t\t{_hp}/{_max_hp}");
            Console.SetCursorPosition(y, x++);
            Console.Write($"Armor:\t\t{_armor}");
            Console.SetCursorPosition(y, x++);
            Console.Write($"Damage:\t\t{_damage}");
            Console.SetCursorPosition(y, x++);
            Console.Write($"Dodge chance:\t\t{Math.Round(_dodge_chance * 100, 1)}%");
            Console.SetCursorPosition(y, x++);
            Console.Write($"Block chance:\t\t{Math.Round(_block_chance * 100, 1)}%");
            Console.SetCursorPosition(y, x++);
            Console.Write($"Crit chance:\t\t{Math.Round(_crit_chance * 100, 1)}% ({Math.Round(_crit_increaser * 100, 1)}%)");
            Console.SetCursorPosition(y, x++);
            Console.Write($"Inventory: {_inventory.Items.Count} Skill points: {_skillPoints} Score: {_score}");
        }

        public void UpgradeHP()
        {
            if (_skillPoints > 0)
            {
                _max_hp += 10 * _level;
                _hp = _max_hp;
                _skillPoints--;
            }
        }
        public void UpgradeArmor()
        {
            if (_skillPoints > 0)
            {
                _armor += 1 * _level;
                _skillPoints--;
            }
        }
        public void UpgradeDamage()
        {
            if (_skillPoints > 0)
            {
                _damage += 2 * _level;
                _skillPoints--;
            }
        }
        public void UpgradeDodgeChance()
        {
            if (_skillPoints > 0)
            {
                _dodge_chance += .01;
                _skillPoints--;
            }
        }
        public void UpgradeBlockChance()
        {
            if (_skillPoints > 0)
            {
                _block_chance += .01;
                _skillPoints--;
            }
        }
        public void UpgradeCritChance()
        {
            if (_skillPoints > 0)
            {
                _crit_chance += .01;
                _skillPoints--;
            }
        }
        public void UpgradeCritIncreaser()
        {
            if (_skillPoints > 0)
            {
                _crit_increaser += .05;
                _skillPoints--;
            }
        }

        private bool AskAccept(int x, int y)
        {
            var enemies = _map.MapEntities.Enemies.Where(s => s.X == x && s.Y == y);

            Console.SetCursorPosition(40, 18);
            if (enemies.Count() > 1)
            {
                var avgHP = 0;
                foreach (var enemy in enemies)
                {
                    avgHP += enemy.HP;
                }
                avgHP /= enemies.Count();
                Console.Write($"{enemies.Count()} enemies (Avg.HP: {avgHP}) are waiting for you in next room");
            }
            else
            {
                Console.Write($"1 enemy (HP: {enemies.First().HP}) are waiting for you in next room");
            }

            Console.SetCursorPosition(40, 19);
            Console.Write("Press ENTER for accept attack");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Enter:
                    return true;
                default:
                    Console.SetCursorPosition(40, 18);
                    Console.Write("                                                          ");
                    Console.SetCursorPosition(40, 19);
                    Console.Write("                             ");
                    return false;
            }
        }

        private void GetChest(int direction, int x, int y)
        {
            if (_map.Rooms[_x, _y].Neighbours[direction].IsChestHere)
            {
                var chest = _map.MapEntities.Chests.Where(s => s.X == _x + x && s.Y == _y + y).First();
                foreach (var item in chest.Inventory.Items)
                {
                    _inventory.AddItem(item);
                }
                _map.Rooms[_x, _y].Neighbours[direction].SetChestHere();
                _map.MapEntities.Chests.Remove(chest);
            }
        }
        private void GetEnemy(int x, int y)
        {
            if (AskAccept(_x + x, _y + y))
            {
                Program.BattleMenu.CreateBattle(_map.MapEntities.Enemies.Where(s => s.X == _x + x && s.Y == _y + y), this, _map);
                Program.BattleMenu.Activate();
                Program.GameMenu.RenewGraphics();
            }
        }
    }
}
