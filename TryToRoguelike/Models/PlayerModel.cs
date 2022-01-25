using TryToRoguelike.Entities;

namespace TryToRoguelike.Models
{
    public class PlayerModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int Score { get; set; }
        public int SkillPoints { get; set; }
        public int MaxHp { get; set; }
        public int Armor { get; set; }
        public int Damage { get; set; }
        public double DodgeChance { get; set; }
        public double BlockChance { get; set; }
        public double CritChance { get; set; }
        public double CritIncreaser { get; set; }
        public bool IsAlive { get; set; }
        public InventoryModel Inventory { get; set; }

        public static Player FromModelToObj(PlayerModel model)
        {
            return new(
                model.X,
                model.Y,
                model.Level,
                model.Exp,
                model.Score,
                model.SkillPoints,
                model.MaxHp,
                model.Armor,
                model.Damage,
                model.DodgeChance,
                model.BlockChance,
                model.CritChance,
                model.CritIncreaser,
                model.IsAlive,
                InventoryModel.FromModelToObj(model.Inventory));
        }

        public static PlayerModel FromObjToModel(Player player)
        {
            return new()
            {
                X = player.X,
                Y = player.Y,
                Level = player.Level,
                Exp = player.Exp,
                Score = player.Score,
                SkillPoints = player.SkillPoints,
                MaxHp = player.MaxHP,
                Armor = player.Armor,
                Damage = player.Damage,
                DodgeChance = player.DodgeChance,
                BlockChance = player.BlockChance,
                CritChance = player.CritChance,
                CritIncreaser = player.CritIncreaser,
                IsAlive = player.IsAlive,
                Inventory = InventoryModel.FromObjToModel(player.Inventory)
            };
        }
    }
}
