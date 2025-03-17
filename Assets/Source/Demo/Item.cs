using System;

namespace InventoryDemo
{
    public class Item : IItem
    {
        public event Action<IItem> OnUse;

        public string Name => m_Name;
        public int Weight => m_Weight;

        public string Description => m_Description;
        public string Stats => m_Stats;
        public object Owner => m_Owner;

        string m_Name;
        int m_Weight;
        string m_Description;
        string m_Stats;
        object m_Owner;

        public Item(string name, int weight)
        {
            m_Name = name;
            m_Weight = weight;
        }

        public void Use()
        {
            // Use the item

            OnUse?.Invoke(this);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            m_Name = name;
        }

        public void SetWeight(int weight)
        {
            if (weight < 0)
                return;

            m_Weight = weight;
        }

        public void SetDescription(string description) => m_Description = description;
        public void SetStats(string stats) => m_Stats = stats;
        public void SetOwner(object owner) => m_Owner = owner;
    }
}
