using System.Collections.Generic;
using TryToRoguelike.Entities;

namespace TryToRoguelike.GameElements
{
    public class MapEntities
    {
        private Player _player;
        private List<Enemy> _enemies;
        private List<Chest> _chests;

        public List<Enemy> Enemies { get => _enemies; }
        public List<Chest> Chests { get => _chests; }

        public void Register(Player player)
        {
            _player = player;

            _enemies = new();
            _chests = new();
        }

        public void CreateChestEntity(int x, int y)
        {
            _chests.Add(new(x, y, _player));
        }
        public void CreateEnemyEntity(int x, int y)
        {
            var rnd = Program.GetRandom();
            if (rnd < .05)
            {
                _enemies.Add(new Enemy(x, y, _player));
                _enemies.Add(new Enemy(x, y, _player));
                _enemies.Add(new Enemy(x, y, _player));
            }
            else if (rnd < .3)
            {
                _enemies.Add(new Enemy(x, y, _player));
                _enemies.Add(new Enemy(x, y, _player));
            }
            else
            {
                _enemies.Add(new Enemy(x, y, _player));
            }
        }
    }
}
