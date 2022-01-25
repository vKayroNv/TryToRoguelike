using System;

namespace TryToRoguelike.Items
{
    public class Cuirass : Item
    {
        public Cuirass(
            int hp,
            int armor,
            int damage,
            double dodge_chance,
            double block_chance,
            double crit_chance,
            double crit_increaser)
        {
            _hp = hp;
            _armor = armor;
            _damage = damage;
            _dodge_chance = dodge_chance;
            _block_chance = block_chance;
            _crit_chance = crit_chance;
            _crit_increaser = crit_increaser;
        }

        public Cuirass(int level)
        {
            int item_class = Program.GetIntRandom(1, 4);

            switch (item_class)
            {
                case 1:
                    item_class = 5 * level;
                    break;
                case 2:
                    item_class = 10 * level;
                    break;
                case 3:
                    item_class = 15 * level;
                    break;
            }

            _hp = 25 * level + item_class + Program.GetIntRandom(-25 * level, 25 * level + 1);
            _armor = 5 * level + item_class / 5 + Program.GetIntRandom(-5 * level, 5 * level + 1);
            _damage = 0;
            _dodge_chance = Program.GetRandom(-.25, .25);
            _block_chance = Program.GetRandom(-.25, .25);
            _crit_chance = 0;
            _crit_increaser = 0;
        }

        public override string ToString()
        {
            return $"PWR:{GetPower()}, " +
                   $"HP:{_hp}, " +
                   $"Armor:{_armor}, " +
                   $"DC:{Math.Round(_dodge_chance * 100, 1)}%, " +
                   $"BC:{Math.Round(_block_chance * 100, 1)}%";
        }
    }
}
