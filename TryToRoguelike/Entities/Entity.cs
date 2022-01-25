using System;
using TryToRoguelike.GameElements;
using TryToRoguelike.Items;

namespace TryToRoguelike.Entities
{
    public class Entity : Elementaries
    {
        protected int
            _level,
            _hp,
            _max_hp,
            _armor,
            _damage;
        protected double
            _dodge_chance,
            _block_chance,
            _crit_chance,
            _crit_increaser;
        protected bool _is_alive;

        public int Level { get => _level; }
        public int HP { get => _hp; }
        public int MaxHP { get => _max_hp; }
        public int Armor { get => _armor; }
        public int Damage { get => _damage; }
        public double DodgeChance { get => _dodge_chance; }
        public double BlockChance { get => _block_chance; }
        public double CritChance { get => _crit_chance; }
        public double CritIncreaser { get => _crit_increaser; }

        public bool IsAlive { get => _is_alive; }

        protected bool IsDodged => Program.GetRandom() < _dodge_chance;
        protected bool IsBlocked => Program.GetRandom() < _block_chance;
        protected bool IsCrit => Program.GetRandom() < _crit_chance;

        public Entity()
        {
            _is_alive = true;
            _inventory = new Inventory(this);
        }

        public int Attack(Entity target)
        {
            if (target == null)
            {
                throw new ArgumentNullException();
            }

            int result = 0;

            if (!target.IsBlocked && !target.IsDodged)
            {
                if (IsCrit)
                {
                    result = _damage + (int)Math.Round(_damage * _crit_increaser);
                }
                else
                {
                    result = _damage;
                }
            }

            return target.GetDamage(result);
        }

        public int GetDamage(int damage)
        {
            int result;

            if (_armor <= damage)
            {
                result = damage - _armor;
            }
            else
            {
                result = 0;
            }

            _hp -= result;

            if (_hp <= 0)
            {
                _hp = 0;
                _is_alive = false;
            }

            return result;
        }

        public int GetPower()
        {
            return (int)Math.Floor(_max_hp * .25 + _armor * .25 + _damage * .4 + (_dodge_chance + _block_chance + _crit_chance + _crit_increaser) * 10);
        }

        public void IncreaseStats(Item item)
        {
            _hp += item.HP;
            _max_hp += item.HP;
            _armor += item.Armor;
            _damage += item.Damage;
            _dodge_chance += item.DodgeChance;
            _block_chance += item.BlockChance;
            _crit_chance += item.CritChance;
            _crit_increaser += item.CritIncreaser;
        }

        public void DecreaseStats(Item item)
        {
            _hp -= item.HP;
            _max_hp -= item.HP;
            _armor -= item.Armor;
            _damage -= item.Damage;
            _dodge_chance -= item.DodgeChance;
            _block_chance -= item.BlockChance;
            _crit_chance -= item.CritChance;
            _crit_increaser -= item.CritIncreaser;
        }
    }
}
