using System.IO;
using System.Text.Json;
using TryToRoguelike.Models;

namespace TryToRoguelike
{
    public static class SaveLoadModule
    {
        private const string FileName = "save";

        public static SaveFileModel Load()
        {
            if (File.Exists(FileName))
            {
                using (StreamReader sr = new(FileName))
                {
                    return JsonSerializer.Deserialize<SaveFileModel>(sr.ReadToEnd());
                }
            }
            else
            {
                return null;
            }
        }

        public static void Save()
        {
            SaveFileModel model = new()
            {
                Player = PlayerModel.FromObjToModel(Program.GameMenu.Player),
                Map = MapModel.FromObjToModel(Program.GameMenu.Map)
            };

            using (StreamWriter sw = new(FileName))
            {
                sw.Write(JsonSerializer.Serialize(model));
            }
        }
    }
}
