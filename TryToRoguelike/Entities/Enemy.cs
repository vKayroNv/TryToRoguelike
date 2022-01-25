using TryToRoguelike.Items;

namespace TryToRoguelike.Entities
{
    public class Enemy : Entity
    {
        private const double mod = 68d;
        private readonly Player _player;

        public Enemy(int x, int y, Player player)
        {
            _x = x;
            _y = y;

            _player = player;

            double k = Program.GetRandom(
                player.GetPower() / mod - player.GetPower() / mod * .2,
                player.GetPower() / mod + player.GetPower() / mod * .2);

            _hp = _max_hp = (int)(100 * k);
            _armor = 0;
            _damage = (int)(10 * k);
            _dodge_chance = .05 + k / 100;
            _block_chance = .05 + k / 100;
            _crit_chance = .05 + k / 100;
            _crit_increaser = .2 + k / 25;

            SetEquipment(player.GetPower());
        }

        public Item GetLoot()
        {
            return Program.GetIntRandom(0, 8) switch
            {
                0 => new Boots(_player.Level),
                1 => new Cuirass(_player.Level),
                2 => new Daggers(_player.Level),
                3 => new Helmet(_player.Level),
                4 => new Pants(_player.Level),
                5 => new Shield(_player.Level),
                6 => new Sword(_player.Level),
                7 => new TwoHandedSword(_player.Level),
                _ => null
            };
        }

        private void SetEquipment(int power)
        {
            while (true)
            {
                if (Program.GetRandom() < power / 6400d && _inventory.Items.Count < 6)
                {
                    _inventory.AddItem(GetLoot());
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < _inventory.Items.Count; i++)
            {
                _inventory.EquipItem(0);
            }
            _inventory.Items.Clear();
        }
    }
}
