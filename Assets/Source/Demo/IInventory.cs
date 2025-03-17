using System;
using System.Collections.Generic;

namespace InventoryDemo
{
    public interface IInventory : IDisposable
    {
        int Capacity { get; }
        int Count { get; }
        void SetCapacity(int capacity);
        void Clear();
        bool HasItem(IItem item);
        bool AddItem(IItem item);
        bool RemoveItem(IItem item);
        bool InsertItem(int index, IItem item);
        bool RemoveAt(int index);
        IItem GetItem(int index);
        IList<IItem> GetItems();
    }
}
