namespace TryToRoguelike.Items
{
    public class Daggers : Item
    {
        public Daggers(
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

        public Daggers(int level)
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
            _damage = 5 * item_class + Program.GetIntRandom(0, item_class * level);
            _dodge_chance = Program.GetRandom(-.2, .2);
            _block_chance = Program.GetRandom(-.2, .2);
            _crit_chance = Program.GetRandom(0, .5);
            _crit_increaser = item_class * .2d + item_class * Program.GetRandom(-item_class, item_class) / level / 100d;
        }
    }
}
