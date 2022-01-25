using System.Collections.Generic;
using TryToRoguelike.GameElements;
using TryToRoguelike.Items;

namespace TryToRoguelike.Models
{
    public class InventoryModel
    {
        public Dictionary<string, ItemModel> Equpped { get; set; }
        public List<ItemModel> Items { get; set; }
        public int MaxSize { get; set; }

        public static Inventory FromModelToObj(InventoryModel model)
        {
            Dictionary<string, Item> equpped = new();
            List<Item> items = new();
            foreach (var item in model.Equpped)
            {
                equpped.Add(item.Key, ItemModel.FromModelToObj(item.Value));
            }
            foreach (var item in model.Items)
            {
                items.Add(ItemModel.FromModelToObj(item));
            }

            return new(equpped, items, model.MaxSize);
        }

        public static InventoryModel FromObjToModel(Inventory inventory)
        {
            InventoryModel model = new()
            {
                Equpped = new Dictionary<string, ItemModel>(),
                Items = new List<ItemModel>(),
                MaxSize = inventory.MaxSize
            };
            foreach (var item in inventory.Equpped)
            {
                model.Equpped.Add(item.Key, ItemModel.FromObjToModel(item.Value));
            }
            foreach (var item in inventory.Items)
            {
                model.Items.Add(ItemModel.FromObjToModel(item));
            }
            return model;
        }
    }
}
