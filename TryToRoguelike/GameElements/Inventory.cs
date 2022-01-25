using System.Collections.Generic;
using TryToRoguelike.Entities;
using TryToRoguelike.Items;

namespace TryToRoguelike.GameElements
{
    public class Inventory
    {
        private readonly Dictionary<string, Item> _equpped;
        private readonly List<Item> _items;
        private int _max_size;
        private Entity _entity;

        public Dictionary<string, Item> Equpped { get => _equpped; }
        public List<Item> Items { get => _items; }
        public int MaxSize { get => _max_size; }

        public Inventory(Dictionary<string, Item> equpped, List<Item> items, int max_size)
        {
            _equpped = equpped;
            _items = items;
            _max_size = max_size;
        }

        public Inventory(Entity entity)
        {
            if (entity != null)
            {
                _entity = entity;
            }

            _max_size = 20;

            _equpped = new Dictionary<string, Item>()
            {
                {"helmet",    null },
                {"cuirass",   null },
                {"pants",     null },
                {"boots",     null },
                {"left_hand", null },
                {"right_hand",null }
            };

            _items = new List<Item>();
        }

        public void SetEntity(Entity entity)
        {
            _entity = entity;
        }

        public void AddItem(Item item)
        {
            if (item == null)
            {
                return;
            }
            if (_items.Count < _max_size)
            {
                _items.Add(item);
            }
        }

        public void DropItem(int index)
        {
            _items.RemoveAt(index);
        }

        public void EquipItem(int index)
        {
            var t = _items[index].GetType();

            if (t == typeof(Helmet))
            {
                EquipHelmet(_items[index]);
            }
            else if (t == typeof(Cuirass))
            {
                EquipCuirass(_items[index]);
            }
            else if (t == typeof(Pants))
            {
                EquipPants(_items[index]);
            }
            else if (t == typeof(Boots))
            {
                EquipBoots(_items[index]);
            }
            else if (t == typeof(Sword))
            {
                EquipSword(_items[index]);
            }
            else if (t == typeof(Shield))
            {
                EquipShield(_items[index]);
            }
            else if (t == typeof(Daggers))
            {
                EquipDaggers(_items[index]);
            }
            else if (t == typeof(TwoHandedSword))
            {
                EquipTwoHandedSword(_items[index]);
            }
        }

        public void UnequipItem(string name)
        {
            _entity.DecreaseStats(_equpped[name]);
            _items.Add(_equpped[name]);
            _equpped[name] = null;
        }

        public void IncreaseMaxSize()
        {
            _max_size += 5;
        }

        private void EquipHelmet(Item item)
        {
            if (_equpped["helmet"] != null)
            {
                UnequipItem("helmet");
            }

            _entity.IncreaseStats(item);
            _equpped["helmet"] = item;
            _items.Remove(item);
        }
        private void EquipCuirass(Item item)
        {
            if (_equpped["cuirass"] != null)
            {
                UnequipItem("cuirass");
            }

            _entity.IncreaseStats(item);
            _equpped["cuirass"] = item;
            _items.Remove(item);
        }
        private void EquipPants(Item item)
        {
            if (_equpped["pants"] != null)
            {
                UnequipItem("pants");
            }

            _entity.IncreaseStats(item);
            _equpped["pants"] = item;
            _items.Remove(item);
        }
        private void EquipBoots(Item item)
        {
            if (_equpped["boots"] != null)
            {
                UnequipItem("boots");
            }

            _entity.IncreaseStats(item);
            _equpped["boots"] = item;
            _items.Remove(item);
        }
        private void EquipSword(Item item)
        {
            if (_equpped["right_hand"] != null)
            {
                if (_equpped["right_hand"].GetType() == typeof(TwoHandedSword) ||
                    _equpped["right_hand"].GetType() == typeof(Daggers))
                {
                    _equpped["left_hand"] = null;
                }

                UnequipItem("right_hand");
            }

            _entity.IncreaseStats(item);
            _equpped["right_hand"] = item;
            _items.Remove(item);
        }
        private void EquipShield(Item item)
        {
            if (_equpped["left_hand"] != null)
            {
                if (_equpped["left_hand"].GetType() == typeof(TwoHandedSword) ||
                    _equpped["left_hand"].GetType() == typeof(Daggers))
                {
                    _equpped["right_hand"] = null;
                }

                UnequipItem("left_hand");
            }

            _entity.IncreaseStats(item);
            _equpped["left_hand"] = item;
            _items.Remove(item);
        }
        private void EquipDaggers(Item item)
        {
            if (_equpped["left_hand"] != null && (
                _equpped["left_hand"].GetType() == typeof(TwoHandedSword) ||
                _equpped["left_hand"].GetType() == typeof(Daggers)))
            {
                UnequipItem("left_hand");
                _equpped["right_hand"] = null;
            }
            else
            {
                if (_equpped["right_hand"] != null)
                {
                    UnequipItem("right_hand");
                }
                if (_equpped["left_hand"] != null)
                {
                    UnequipItem("left_hand");
                }
            }

            _entity.IncreaseStats(item);
            _equpped["right_hand"] = item;
            _equpped["left_hand"] = item;

            _items.Remove(item);
        }
        private void EquipTwoHandedSword(Item item)
        {
            if (_equpped["left_hand"] != null && (
                _equpped["left_hand"].GetType() == typeof(TwoHandedSword) ||
                _equpped["left_hand"].GetType() == typeof(Daggers)))
            {
                UnequipItem("left_hand");
                _equpped["right_hand"] = null;
            }
            else
            {
                if (_equpped["right_hand"] != null)
                {
                    UnequipItem("right_hand");
                }
                if (_equpped["left_hand"] != null)
                {
                    UnequipItem("left_hand");
                }
            }

            _entity.IncreaseStats(item);
            _equpped["right_hand"] = item;
            _equpped["left_hand"] = item;

            _items.Remove(item);
        }
    }
}
