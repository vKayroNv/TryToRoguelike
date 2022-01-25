using System.Drawing;
using TryToRoguelike.GameElements;

namespace TryToRoguelike.Models
{
    public class MapModel
    {
        public RoomModel[][] Rooms { get; set; }

        public static Map FromModelToObj(MapModel model)
        {
            Room[,] rooms = new Room[model.Rooms.GetLength(0), model.Rooms[0].GetLength(0)];

            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    rooms[i, j] = RoomModel.FromModelToObj(model.Rooms[i][j]);
                }
            }
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        if (model.Rooms[i][j].Neighbours[k] != new Point(-1, -1))
                        {
                            rooms[i, j].SetNeighbour(k, rooms[model.Rooms[i][j].Neighbours[k].X, model.Rooms[i][j].Neighbours[k].Y]);
                        }
                    }
                }
            }

            return new(rooms);
        }

        public static MapModel FromObjToModel(Map map)
        {
            RoomModel[][] rooms = new RoomModel[map.Rooms.GetLength(0)][];
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                rooms[i] = new RoomModel[map.Rooms.GetLength(1)];
            }

            for (int i = 0; i < rooms.Length; i++)
            {
                for (int j = 0; j < rooms[0].Length; j++)
                {
                    rooms[i][j] = RoomModel.FromObjToModel(map.Rooms[i, j]);
                }
            }

            return new()
            {
                Rooms = rooms
            };
        }
    }
}
