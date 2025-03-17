using System.Collections.Generic;

namespace InventoryDemo
{
    public class Inventory : IInventory
    {
        List<IItem> m_Items = new List<IItem>();
        Dictionary<IItem, int> m_InventoryMap = new Dictionary<IItem, int>();

        public int Capacity { get; private set;}
        public int Count => m_Items.Count;

        public const int MaxCapacity = 9999;
        public const int DefaultCapacity = 10;

        public Inventory()
        {
            SetCapacity(DefaultCapacity);
        }

        public Inventory(int capacity)
        {
            SetCapacity(capacity);
        }

        public void SetCapacity(int capacity)
        {
            if(capacity < 0 || capacity > MaxCapacity)
                return;

            Capacity = capacity;
        }

        public void Clear()
        {
            m_Items.Clear();
            m_InventoryMap.Clear();
        }

        public bool HasItem(IItem item)
        {
            return m_InventoryMap.ContainsKey(item);
        }

        public bool AddItem(IItem item)
        {
            if (item == null)
                return false;

            if (m_InventoryMap.ContainsKey(item))
                return false;

            if(m_InventoryMap.Count >= Capacity)
                return false;

            m_Items.Add(item);
            m_InventoryMap[item] = m_Items.Count - 1;

            return true;
        }

        public bool RemoveItem(IItem item)
        {
            if (item == null)
                return false;

            if (!m_InventoryMap.Remove(item, out var index))
                return false;

            m_Items.RemoveAt(index);
            for(var i = index ; i < m_Items.Count; i++)
            {
                m_InventoryMap[m_Items[i]] = i;
            }

            return true;
        }

        public bool InsertItem(int index, IItem item)
        {
            if (item == null)
                return false;

            if (m_InventoryMap.ContainsKey(item))
                return false;

            if (index < 0 || index > m_Items.Count)
                return false;

            m_Items.Insert(index, item);

            for(var i = index; i < m_Items.Count; i++)
            {
                m_InventoryMap[m_Items[i]] = i;
            }

            return true;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= Capacity)
                return false;

            var itemToRemove = m_Items[index];
            return RemoveItem(itemToRemove);
        }

        public IItem GetItem(int index)
        {
            if (index < 0 || index >= Capacity)
                return null;

            return m_Items[index];
        }

        public IList<IItem> GetItems()
        {
            return m_Items;
        }

        public void Dispose()
        {
            m_Items.Clear();
            m_InventoryMap.Clear();
            m_Items = null;
            m_InventoryMap = null;
        }
    }
}
