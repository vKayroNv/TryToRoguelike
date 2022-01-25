using System;
using System.Drawing;

namespace TryToRoguelike.GameElements
{
    public class Room : Updatable
    {
        private Point _position, _drawPosition;
        private bool _isEnemyHere, _isPlayerHere, _isChestHere, _isInitialized, _isDataChanged;
        private readonly Room[] _neighbours;

        public Point Position { get => _position; }
        public Point DrawPosition { get => _drawPosition; }
        public bool IsEnemyHere { get => _isEnemyHere; }
        public bool IsPlayerHere { get => _isPlayerHere; }
        public bool IsChestHere { get => _isChestHere; }
        public bool IsDataChanged { get => _isDataChanged; }
        public bool IsInitialized
        {
            get => _isInitialized;
            set => _isInitialized = true;
        }
        public Room[] Neighbours { get => _neighbours; }

        public Room(Point position, Point drawPosition)
        {
            _position = position;
            _drawPosition = drawPosition;
            _neighbours = new Room[4]; // 0-top,1-right,2-bottom,3-left
            _isDataChanged = true;
        }

        public Room(
            Point Position,
            Point DrawPosition,
            bool IsEnemyHere,
            bool IsPlayerHere,
            bool IsChestHere,
            bool IsInitialized)
        {
            _position = Position;
            _drawPosition = DrawPosition;
            _isEnemyHere = IsEnemyHere;
            _isPlayerHere = IsPlayerHere;
            _isChestHere = IsChestHere;
            _isInitialized = IsInitialized;
            _neighbours = new Room[4];
            _isDataChanged = true;
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(_drawPosition.Y, _drawPosition.X);

            if (_isDataChanged)
            {
                if (Neighbours[0] == null && Neighbours[1] == null &&
                    Neighbours[2] == null && Neighbours[3] == null)
                {
                    Console.Write(Program.wall + Program.wall + Program.wall);
                    Console.SetCursorPosition(_drawPosition.Y, _drawPosition.X + 1);
                    Console.Write(Program.wall + Program.wall + Program.wall);
                    Console.SetCursorPosition(_drawPosition.Y, _drawPosition.X + 2);
                    Console.Write(Program.wall + Program.wall + Program.wall);
                }
                else
                {

                    Console.Write(Program.wall);
                    if (_neighbours[0] != null)
                    {
                        Console.Write(Program.space);
                    }
                    else
                    {
                        Console.Write(Program.wall);
                    }

                    Console.Write(Program.wall);
                    Console.SetCursorPosition(_drawPosition.Y, _drawPosition.X + 1);
                    if (_neighbours[3] != null)
                    {
                        Console.Write(Program.space);
                    }
                    else
                    {
                        Console.Write(Program.wall);
                    }

                    if (_isPlayerHere) { Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write(Program.player); }
                    else if (_isEnemyHere) { Console.ForegroundColor = ConsoleColor.Red; Console.Write(Program.enemy); }
                    else if (_isChestHere) { Console.ForegroundColor = ConsoleColor.Blue; Console.Write(Program.chest); }
                    else
                    {
                        Console.Write(Program.space);
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGray;

                    if (_neighbours[1] != null)
                    {
                        Console.Write(Program.space);
                    }
                    else
                    {
                        Console.Write(Program.wall);
                    }

                    Console.SetCursorPosition(_drawPosition.Y, _drawPosition.X + 2);
                    Console.Write(Program.wall);
                    if (_neighbours[2] != null)
                    {
                        Console.Write(Program.space);
                    }
                    else
                    {
                        Console.Write(Program.wall);
                    }

                    Console.Write(Program.wall);
                }

                _isDataChanged = false;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void SetNeighbour(int direction, Room room)
        {
            _neighbours[direction] = room;
        }

        public void SetPlayerHere()
        {
            _isPlayerHere = !_isPlayerHere;
            _isDataChanged = true;
        }
        public void SetChestHere()
        {
            _isChestHere = !_isChestHere;
            _isDataChanged = true;
        }
        public void SetEnemyHere()
        {
            _isEnemyHere = !_isEnemyHere;
            _isDataChanged = true;
        }

        public void RenewGraphics()
        {
            _isDataChanged = true;
        }
    }
}
