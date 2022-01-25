namespace TryToRoguelike.Items
{
    public class Sword : Item
    {
        public Sword(
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

        public Sword(int level)
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

            _hp = 0;
            _armor = 0;
            _damage = 7 * item_class + Program.GetIntRandom(0, item_class * level);
            _dodge_chance = Program.GetRandom(-.1, .1);
            _block_chance = Program.GetRandom(-.1, .1);
            _crit_chance = Program.GetRandom(0, .5);
            _crit_increaser = item_class * .15d + item_class * Program.GetRandom(-item_class, item_class) / level / 100d;
        }
    }
}
