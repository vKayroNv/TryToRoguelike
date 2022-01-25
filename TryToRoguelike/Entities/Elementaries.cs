using TryToRoguelike.GameElements;

namespace TryToRoguelike.Entities
{
    public class Elementaries
    {
        protected int _x, _y;
        protected Inventory _inventory;

        public int X { get => _x; }
        public int Y { get => _y; }
        public Inventory Inventory { get => _inventory; }
    }
}
