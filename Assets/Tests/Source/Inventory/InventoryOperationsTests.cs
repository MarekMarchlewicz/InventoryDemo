using System.Collections.Generic;
using InventoryDemo;
using NUnit.Framework;

namespace Tests.Inventory
{
    public class InventoryOperationsTests
    {
        IInventory m_Inventory;

        [SetUp]
        public void SetUp()
        {
            m_Inventory = new InventoryDemo.Inventory();
        }

        [TearDown]
        public void TearDown()
        {
            m_Inventory.Dispose();
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(InventoryDemo.Inventory.MaxCapacity)]
        public void Inventory_SetCorrectCapacity_CapacitySet(int capacity)
        {
            m_Inventory.SetCapacity(capacity);
            Assert.That(capacity, Is.EqualTo(m_Inventory.Capacity));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(InventoryDemo.Inventory.MaxCapacity + 1)]
        public void Inventory_SetIncorrectCapacity_CapacityNotSet(int capacity)
        {
            var originalCapacity = m_Inventory.Capacity;
            m_Inventory.SetCapacity(capacity);
            Assert.That(originalCapacity, Is.EqualTo(m_Inventory.Capacity));
        }

        [Test]
        public void Inventory_AddItem_ItemAdded()
        {
            Assert.That(m_Inventory.Count, Is.EqualTo(0));
            var item = new Item("Test Item", 1);

            var result = m_Inventory.AddItem(item);

            Assert.That(result, Is.True);

            Assert.That(m_Inventory.Count, Is.EqualTo(1));
            Assert.That(m_Inventory.GetItem(0), Is.EqualTo(item));
        }

        [Test]
        public void Inventory_AddNullItem_NotAdded()
        {
            Assert.That(m_Inventory.Count, Is.EqualTo(0));

            var result = m_Inventory.AddItem(null);

            Assert.That(result, Is.False);

            Assert.That(m_Inventory.Count, Is.EqualTo(0));
        }

        [Test]
        public void Inventory_AddItemThatAlreadyExists_NotAdded()
        {
            Assert.That(m_Inventory.Count, Is.EqualTo(0));

            var item = new Item("Test Item", 1);
            var result = m_Inventory.AddItem(item);

            Assert.That(result, Is.True);
            Assert.That(m_Inventory.Count, Is.EqualTo(1));

            result = m_Inventory.AddItem(item);

            Assert.That(result, Is.False);
            Assert.That(m_Inventory.Count, Is.EqualTo(1));
        }

        [Test]
        public void Inventory_AddItemOverCapacity_NotAdded()
        {
            Assert.That(m_Inventory.Count, Is.EqualTo(0));

            var capacity = 2;
            m_Inventory.SetCapacity(capacity);
            var item0 = new Item("Test Item", 1);
            var item1 = new Item("Test Item", 10);
            var item2 = new Item("Test Item", 2);
            var testItems = new List<IItem> { item0, item1, item2 };
            for (var i = 0; i < testItems.Count; i++)
            {
                var testItem = testItems[i];
                var result = m_Inventory.AddItem(testItem);

                if (i + 1 <= capacity)
                    Assert.That(result, Is.True);
                else
                    Assert.That(result, Is.False);
            }

            Assert.That(m_Inventory.Count, Is.EqualTo(capacity));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Inventory_InsertItem_ItemInserted(int indexToInsert)
        {
            var item0 = new Item("Test Item", 1);
            var item1 = new Item("Test Item", 10);
            var item2 = new Item("Test Item", 2);

            var testItems = new List<IItem> { item0, item1, item2 };
            foreach (var testItem in testItems)
                m_Inventory.AddItem(testItem);

            Assert.That(m_Inventory.Count, Is.EqualTo(testItems.Count));

            var newItem = new Item("Test Item", 100);
            var result = m_Inventory.InsertItem(indexToInsert, newItem);

            Assert.That(result, Is.True);
            Assert.That(m_Inventory.GetItem(indexToInsert), Is.EqualTo(newItem));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(InventoryDemo.Inventory.MaxCapacity + 1)]
        public void Inventory_InsertItemAtIncorrectItem_NotInserted(int indexToInsert)
        {
            var item0 = new Item("Test Item", 1);
            var item1 = new Item("Test Item", 10);
            var item2 = new Item("Test Item", 2);

            var testItems = new List<IItem> { item0, item1, item2 };
            foreach (var testItem in testItems)
                m_Inventory.AddItem(testItem);

            Assert.That(m_Inventory.Count, Is.EqualTo(testItems.Count));

            var newItem = new Item("Test Item", 100);
            var result = m_Inventory.InsertItem(indexToInsert, newItem);

            Assert.That(result, Is.False);
            Assert.That(m_Inventory.GetItem(indexToInsert), Is.Null);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Inventory_GetItem_ReturnsCorrectItem(int indexToGet)
        {
            var item0 = new Item("Test Item", 1);
            var item1 = new Item("Test Item", 10);
            var item2 = new Item("Test Item", 2);

            var testItems = new List<IItem> { item0, item1, item2 };
            foreach (var testItem in testItems)
                m_Inventory.AddItem(testItem);

            Assert.That(m_Inventory.Count, Is.EqualTo(testItems.Count));

            var item = m_Inventory.GetItem(indexToGet);
            Assert.That(item, Is.EqualTo(testItems[indexToGet]));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(InventoryDemo.Inventory.MaxCapacity + 1)]
        public void Inventory_GetItemIncorrectIndex_ReturnsNull(int indexToGet)
        {
            var item0 = new Item("Test Item", 1);
            var item1 = new Item("Test Item", 10);
            var item2 = new Item("Test Item", 2);

            var testItems = new List<IItem> { item0, item1, item2 };
            foreach (var testItem in testItems)
                m_Inventory.AddItem(testItem);

            var item = m_Inventory.GetItem(indexToGet);

            Assert.That(item, Is.Null);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Inventory_RemoveAt_RemovedItem(int indexToRemove)
        {
            var item0 = new Item("Test Item", 1);
            var item1 = new Item("Test Item", 10);
            var item2 = new Item("Test Item", 2);

            var testItems = new List<IItem> { item0, item1, item2 };
            foreach (var item in testItems)
                m_Inventory.AddItem(item);

            Assert.That(m_Inventory.Count, Is.EqualTo(testItems.Count));

            var itemToRemove = m_Inventory.GetItem(indexToRemove);
            m_Inventory.RemoveAt(indexToRemove);

            Assert.That(m_Inventory.Count, Is.EqualTo(testItems.Count - 1));
            Assert.That(m_Inventory.HasItem(itemToRemove), Is.False);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(InventoryDemo.Inventory.MaxCapacity + 1)]
        public void Inventory_RemoveAtIncorrectPosition_NOP(int indexToRemove)
        {
            var item0 = new Item("Test Item", 1);
            var item1 = new Item("Test Item", 10);
            var item2 = new Item("Test Item", 2);

            m_Inventory.AddItem(item0);
            m_Inventory.AddItem(item1);
            m_Inventory.AddItem(item2);

            Assert.That(m_Inventory.Count, Is.EqualTo(3));

            m_Inventory.RemoveAt(indexToRemove);

            Assert.That(m_Inventory.Count, Is.EqualTo(3));
        }

        [Test]
        public void Inventory_RemoveItem_RemovedItem()
        {
            var item0 = new Item("Test Item", 1);
            var item1 = new Item("Test Item", 10);
            var item2 = new Item("Test Item", 2);

            var testItems = new List<IItem> { item0, item1, item2 };
            foreach (var item in testItems)
                m_Inventory.AddItem(item);


            var itemCount = m_Inventory.Count;
            Assert.That(itemCount, Is.EqualTo(testItems.Count));

            foreach (var item in testItems)
            {
                m_Inventory.RemoveItem(item);
                itemCount--;
                Assert.That(itemCount, Is.EqualTo(m_Inventory.Count));
            }
        }

        [Test]
        public void Inventory_Clear_ClearItems()
        {
            var item0 = new Item("Test Item", 1);
            var item1 = new Item("Test Item", 10);
            var item2 = new Item("Test Item", 2);

            m_Inventory.AddItem(item0);
            m_Inventory.AddItem(item1);
            m_Inventory.AddItem(item2);

            Assert.That(m_Inventory.Count, Is.EqualTo(3));

            m_Inventory.Clear();

            Assert.That(m_Inventory.Count, Is.EqualTo(0));
        }

        [Test]
        public void Inventory_HasItem_ReturnsCorrectValue()
        {
            var item = new Item("Test Item", 1);

            Assert.That(m_Inventory.HasItem(item), Is.False);

            m_Inventory.AddItem(item);

            Assert.That(m_Inventory.HasItem(item), Is.True);
        }

        [Test]
        public void Inventory_GetItems_ReturnsCorrectCollection()
        {
            var item0 = new Item("Test Item", 1);
            var item1 = new Item("Test Item", 10);
            var item2 = new Item("Test Item", 2);

            var testItems = new List<IItem> { item0, item1, item2 };
            foreach (var item in testItems)
                m_Inventory.AddItem(item);

            Assert.That(m_Inventory.Count, Is.EqualTo(testItems.Count));

            CollectionAssert.AreEqual(m_Inventory.GetItems(), testItems);
        }
    }
}
