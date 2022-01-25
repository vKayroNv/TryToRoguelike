using System;

namespace TryToRoguelike.Items
{
    public class Item
    {
        protected int
            _hp,
            _armor,
            _damage;
        protected double
            _dodge_chance,
            _block_chance,
            _crit_chance,
            _crit_increaser;

        public int HP { get => _hp; }
        public int Armor { get => _armor; }
        public int Damage { get => _damage; }
        public double DodgeChance { get => _dodge_chance; }
        public double BlockChance { get => _block_chance; }
        public double CritChance { get => _crit_chance; }
        public double CritIncreaser { get => _crit_increaser; }

        public Item() { }

        public Item(int hp,
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

        public override string ToString()
        {
            return $"PWR:{GetPower()}, " +
                   $"DMG:{_damage}, " +
                   $"DC:{Math.Round(_dodge_chance * 100, 1)}%, " +
                   $"BC:{Math.Round(_block_chance * 100, 1)}%, " +
                   $"CC:{Math.Round(_crit_chance * 100, 1)}%, " +
                   $"CI:{Math.Round(_crit_increaser * 100, 1)}%";
        }

        public int GetPower() => (int)Math.Floor(_hp * .25 + _armor * .25 + _damage * .4 + (_dodge_chance + _block_chance + _crit_chance + _crit_increaser) * 10);
    }
}