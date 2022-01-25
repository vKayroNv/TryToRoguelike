using System.Drawing;
using TryToRoguelike.GameElements;

namespace TryToRoguelike.Models
{
    public class RoomModel
    {
        public Point Position { get; set; }
        public Point DrawPosition { get; set; }
        public bool IsEnemyHere { get; set; }
        public bool IsPlayerHere { get; set; }
        public bool IsChestHere { get; set; }
        public bool IsInitialized { get; set; }
        public Point[] Neighbours { get; set; }

        public static Room FromModelToObj(RoomModel model)
        {
            return new(
                model.Position,
                model.DrawPosition,
                model.IsEnemyHere,
                model.IsPlayerHere,
                model.IsChestHere,
                model.IsInitialized);
        }

        public static RoomModel FromObjToModel(Room room)
        {
            return new()
            {
                Position = room.Position,
                DrawPosition = room.DrawPosition,
                IsEnemyHere = room.IsEnemyHere,
                IsPlayerHere = room.IsPlayerHere,
                IsChestHere = room.IsChestHere,
                IsInitialized = room.IsInitialized,
                Neighbours = new Point[]
                {
                    room.Neighbours[0] != null ? room.Neighbours[0].Position : new(-1, -1),
                    room.Neighbours[1] != null ? room.Neighbours[1].Position : new(-1, -1),
                    room.Neighbours[2] != null ? room.Neighbours[2].Position : new(-1, -1),
                    room.Neighbours[3] != null ? room.Neighbours[3].Position : new(-1, -1)
                }
            };
        }
    }
}
