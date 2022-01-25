using TryToRoguelike.Items;

namespace TryToRoguelike.Models
{
    public class ItemModel
    {
        public string Type { get; set; }
        public int HP { get; set; }
        public int Armor { get; set; }
        public int Damage { get; set; }
        public double DodgeChance { get; set; }
        public double BlockChance { get; set; }
        public double CritChance { get; set; }
        public double CritIncreaser { get; set; }

        public static Item FromModelToObj(ItemModel model)
        {
            if (model == null)
            {
                return null;
            }

            if (model.Type == "Boots")
            {
                return new Boots(
                    model.HP,
                    model.Armor,
                    model.Damage,
                    model.DodgeChance,
                    model.BlockChance,
                    model.CritChance,
                    model.CritIncreaser);
            }
            else if (model.Type == "Cuirass")
            {
                return new Cuirass(
                    model.HP,
                    model.Armor,
                    model.Damage,
                    model.DodgeChance,
                    model.BlockChance,
                    model.CritChance,
                    model.CritIncreaser);
            }
            else if (model.Type == "Daggers")
            {
                return new Daggers(
                    model.HP,
                    model.Armor,
                    model.Damage,
                    model.DodgeChance,
                    model.BlockChance,
                    model.CritChance,
                    model.CritIncreaser);
            }
            else if (model.Type == "Helmet")
            {
                return new Helmet(
                    model.HP,
                    model.Armor,
                    model.Damage,
                    model.DodgeChance,
                    model.BlockChance,
                    model.CritChance,
                    model.CritIncreaser);
            }
            else if (model.Type == "Pants")
            {
                return new Pants(
                    model.HP,
                    model.Armor,
                    model.Damage,
                    model.DodgeChance,
                    model.BlockChance,
                    model.CritChance,
                    model.CritIncreaser);
            }
            else if (model.Type == "Shield")
            {
                return new Shield(
                    model.HP,
                    model.Armor,
                    model.Damage,
                    model.DodgeChance,
                    model.BlockChance,
                    model.CritChance,
                    model.CritIncreaser);
            }
            else if (model.Type == "Sword")
            {
                return new Sword(
                    model.HP,
                    model.Armor,
                    model.Damage,
                    model.DodgeChance,
                    model.BlockChance,
                    model.CritChance,
                    model.CritIncreaser);
            }
            else if (model.Type == "TwoHandedSword")
            {
                return new TwoHandedSword(
                    model.HP,
                    model.Armor,
                    model.Damage,
                    model.DodgeChance,
                    model.BlockChance,
                    model.CritChance,
                    model.CritIncreaser);
            }
            else
            {
                return null;
            }
        }

        public static ItemModel FromObjToModel(Item item)
        {
            if (item == null)
            {
                return null;
            }

            return new()
            {
                Type = item.GetType().Name,
                HP = item.HP,
                Armor = item.Armor,
                Damage = item.Damage,
                DodgeChance = item.DodgeChance,
                BlockChance = item.BlockChance,
                CritChance = item.CritChance,
                CritIncreaser = item.CritIncreaser
            };
        }
    }
}
