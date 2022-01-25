using System;
using TryToRoguelike.Entities;

namespace TryToRoguelike.Menus
{
    public class LevelUpMenu : Menu
    {
        private Player _player;

        public void Register(Player player)
        {
            _player = player;
        }

        public override void Update()
        {
            switch (Console.ReadKey(true).KeyChar)
            {
                case '1':
                    _player.UpgradeHP();
                    break;
                case '2':
                    _player.UpgradeArmor();
                    break;
                case '3':
                    _player.UpgradeDamage();
                    break;
                case '4':
                    _player.UpgradeDodgeChance();
                    break;
                case '5':
                    _player.UpgradeBlockChance();
                    break;
                case '6':
                    _player.UpgradeCritChance();
                    break;
                case '7':
                    _player.UpgradeCritIncreaser();
                    break;
                case 'q':
                    Deactivate();
                    break;
            }
        }

        public override void Draw()
        {
            Console.Clear();

            Console.WriteLine($"You have {_player.SkillPoints} points\n");

            Console.WriteLine($"1. Upgrade HP ({_player.MaxHP}+{10 * _player.Level})");
            Console.WriteLine($"2. Upgrade Armor ({_player.Armor}+{1 * _player.Level})");
            Console.WriteLine($"3. Upgrade Damage ({_player.Damage}+{2 * _player.Level})");
            Console.WriteLine($"4. Upgrade Dodge Chance ({Math.Round(_player.DodgeChance * 100, 1)}%+1%)");
            Console.WriteLine($"5. Upgrade Block Chance ({Math.Round(_player.BlockChance * 100, 1)}+1%)");
            Console.WriteLine($"6. Upgrade Crit Chance ({Math.Round(_player.CritChance * 100, 1)}+1%)");
            Console.WriteLine($"7. Upgrade Crit Increaser ({Math.Round(_player.CritIncreaser * 100, 1)}+5%)");

            Console.WriteLine($"q. Back");
        }
    }
}
