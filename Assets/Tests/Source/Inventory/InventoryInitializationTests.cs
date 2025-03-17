using InventoryDemo;
using NUnit.Framework;

namespace Tests.Inventory
{
    public class InventoryInitializationTests
    {
        IInventory m_Inventory;

        [TearDown]
        public void TearDown()
        {
            m_Inventory.Dispose();
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(InventoryDemo.Inventory.DefaultCapacity)]
        [TestCase(InventoryDemo.Inventory.MaxCapacity)]
        public void Inventory_SetCorrectCapacity_CapacitySet(int capacity)
        {
            m_Inventory = new InventoryDemo.Inventory();
            m_Inventory.SetCapacity(capacity);
            Assert.That(capacity, Is.EqualTo(m_Inventory.Capacity));
        }

        [Test]
        public void Inventory_Capacity_IsDefault()
        {
            m_Inventory = new InventoryDemo.Inventory();

            Assert.That(InventoryDemo.Inventory.DefaultCapacity, Is.EqualTo(m_Inventory.Capacity));
        }

        [Test]
        public void Inventory_Count_IsZero()
        {
            m_Inventory = new InventoryDemo.Inventory();

            Assert.That(0, Is.EqualTo(m_Inventory.Count));
        }

        [Test]
        public void Inventory_GetItems_ReturnsEmptyList()
        {
            m_Inventory = new InventoryDemo.Inventory();

            var items = m_Inventory.GetItems();
            Assert.IsEmpty(items);
        }
    }
}
