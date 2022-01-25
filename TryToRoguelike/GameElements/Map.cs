using System.Drawing;
using TryToRoguelike.Entities;

namespace TryToRoguelike.GameElements
{
    public class Map : Updatable
    {
        private Room[,] _rooms;
        private Player _player;
        private MapEntities _mapEntities;

        public Room[,] Rooms { get => _rooms; }
        public MapEntities MapEntities { get => _mapEntities; }

        public Map() { }

        public Map(Room[,] rooms)
        {
            _rooms = rooms;
        }

        public void Register(Player player, MapEntities mapEntities)
        {
            _player = player;
            _mapEntities = mapEntities;

            if (Program.IsNewGame)
            {
                GenerateRooms();
                GeneratePaths();
                GenerateEntities();
            }
            else
            {
                GenerateLoadedEntities();
            }
        }

        public override void Update()
        {
            foreach (var room in _rooms)
            {
                room.Update();
            }
        }

        public override void Draw()
        {
            foreach (var room in _rooms)
            {
                room.Draw();
            }
        }

        public void GenerateEntities()
        {
            for (int i = 0; i < _rooms.GetLength(0); i++)
            {
                for (int j = 0; j < _rooms.GetLength(1); j++)
                {
                    if (_rooms[i, j].IsInitialized && !_rooms[i, j].IsPlayerHere)
                    {
                        if (Program.GetRandom() < .01)
                        {
                            _rooms[i, j].SetChestHere();
                            _mapEntities.CreateChestEntity(i, j);
                        }
                        else if (Program.GetRandom() < .1)
                        {
                            _rooms[i, j].SetEnemyHere();
                            _mapEntities.CreateEnemyEntity(i, j);
                        }
                    }
                }
            }
        }

        public void GenerateLoadedEntities()
        {
            for (int i = 0; i < _rooms.GetLength(0); i++)
            {
                for (int j = 0; j < _rooms.GetLength(1); j++)
                {
                    if (_rooms[i, j].IsInitialized && !_rooms[i, j].IsPlayerHere)
                    {
                        if (_rooms[i, j].IsChestHere)
                        {
                            _mapEntities.CreateChestEntity(i, j);
                        }
                        else if (_rooms[i, j].IsEnemyHere)
                        {
                            _mapEntities.CreateEnemyEntity(i, j);
                        }
                    }
                }
            }
        }

        private void TrySetNeighbors(int direct1, int direct2, Point point1, Point point2)
        {
            if (Program.GetRandom() < .7 &&
                _rooms[point1.X, point1.Y].Neighbours[direct1] == null &&
                _rooms[point2.X, point2.Y].Neighbours[direct2] == null &&
                !_rooms[point2.X, point2.Y].IsInitialized)
            {
                _rooms[point1.X, point1.Y].SetNeighbour(direct1, _rooms[point2.X, point2.Y]);
                _rooms[point2.X, point2.Y].SetNeighbour(direct2, _rooms[point1.X, point1.Y]);
                _rooms[point2.X, point2.Y].IsInitialized = true;
                GeneratePaths(point2.X, point2.Y);
            }
        }

        private void GenerateRooms()
        {
            _rooms = new Room[8, 15];
            for (int i = 0; i < _rooms.GetLength(0); i++)
            {
                for (int j = 0; j < _rooms.GetLength(1); j++)
                {
                    _rooms[i, j] = new(new(i, j), new(1 + i * 2, 2 + j * 4));
                }
            }
        }
        private void GeneratePaths()
        {
            int x = Program.GetIntRandom(0, _rooms.GetLength(0) - 1);
            int y = Program.GetIntRandom(0, _rooms.GetLength(1) - 1);

            _rooms[x, y].SetPlayerHere();
            _player.SetPosition(x, y);

            while (
                _rooms[x, y].Neighbours[0] == null &&
                _rooms[x, y].Neighbours[1] == null &&
                _rooms[x, y].Neighbours[2] == null &&
                _rooms[x, y].Neighbours[3] == null
                )
            {
                GeneratePaths(x, y);
            }
        }
        private void GeneratePaths(int i, int j)
        {
            if (i == 0 && j == 0)
            {
                TrySetNeighbors(1, 3, new(i, j), new(i, j + 1));
                TrySetNeighbors(2, 0, new(i, j), new(i + 1, j));
            }
            else if (i == 0 && j == _rooms.GetLength(1) - 1)
            {
                TrySetNeighbors(3, 1, new(i, j), new(i, j - 1));
                TrySetNeighbors(2, 0, new(i, j), new(i + 1, j));
            }
            else if (i == _rooms.GetLength(0) - 1 && j == 0)
            {
                TrySetNeighbors(1, 3, new(i, j), new(i, j + 1));
                TrySetNeighbors(0, 2, new(i, j), new(i - 1, j));
            }
            else if (i == _rooms.GetLength(0) - 1 && j == _rooms.GetLength(1) - 1)
            {
                TrySetNeighbors(3, 1, new(i, j), new(i, j - 1));
                TrySetNeighbors(0, 2, new(i, j), new(i - 1, j));
            }
            else if (i == 0)
            {
                TrySetNeighbors(3, 1, new(i, j), new(i, j - 1));
                TrySetNeighbors(1, 3, new(i, j), new(i, j + 1));
                TrySetNeighbors(2, 0, new(i, j), new(i + 1, j));
            }
            else if (i == _rooms.GetLength(0) - 1)
            {
                TrySetNeighbors(3, 1, new(i, j), new(i, j - 1));
                TrySetNeighbors(1, 3, new(i, j), new(i, j + 1));
                TrySetNeighbors(0, 2, new(i, j), new(i - 1, j));
            }
            else if (j == 0)
            {
                TrySetNeighbors(1, 3, new(i, j), new(i, j + 1));
                TrySetNeighbors(0, 2, new(i, j), new(i - 1, j));
                TrySetNeighbors(2, 0, new(i, j), new(i + 1, j));
            }
            else if (j == _rooms.GetLength(1) - 1)
            {
                TrySetNeighbors(3, 1, new(i, j), new(i, j - 1));
                TrySetNeighbors(0, 2, new(i, j), new(i - 1, j));
                TrySetNeighbors(2, 0, new(i, j), new(i + 1, j));
            }
            else
            {
                TrySetNeighbors(3, 1, new(i, j), new(i, j - 1));
                TrySetNeighbors(1, 3, new(i, j), new(i, j + 1));
                TrySetNeighbors(0, 2, new(i, j), new(i - 1, j));
                TrySetNeighbors(2, 0, new(i, j), new(i + 1, j));
            }
        }
    }
}
