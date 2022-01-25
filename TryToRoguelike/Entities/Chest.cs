using TryToRoguelike.GameElements;
using TryToRoguelike.Items;

namespace TryToRoguelike.Entities
{
    public class Chest : Elementaries
    {
        private readonly Player _player;

        public Chest(int x, int y, Player player)
        {
            _x = x;
            _y = y;

            _player = player;

            _inventory = new Inventory(null);

            GenerateLoot();
        }

        private void GenerateLoot()
        {
            for (int i = 0; i < 5; i++)
            {
                if (Program.GetRandom() < .5 / i)
                {
                    _inventory.AddItem(GetLoot());
                }
                else
                {
                    break;
                }
            }
        }

        private Item GetLoot()
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
    }
}
