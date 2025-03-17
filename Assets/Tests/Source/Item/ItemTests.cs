using InventoryDemo;
using NUnit.Framework;

namespace Tests.Items
{
    [TestFixture]
    public class ItemTests
    {
        IItem m_Item;
        const string k_Name = "TestName";
        const int k_Weight = 8;

        [SetUp]
        public void SetUp()
        {
            m_Item = new Item(k_Name, k_Weight);
        }

        [Test]
        public void Item_InitializedProperly()
        {
            Assert.That(m_Item.Name, Is.EqualTo(k_Name));
            Assert.That(m_Item.Weight, Is.EqualTo(k_Weight));
        }

        [Test]
        [TestCase("New Name")]
        [TestCase("Demo name 1")]
        [TestCase("new_name")]
        public void Item_SetCorrectName_NameSet(string newName)
        {
            m_Item.SetName(newName);

            Assert.That(m_Item.Name, Is.EqualTo(newName));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Item_SetIncorrectName_NameSet(string newName)
        {
            var initialName = m_Item.Name;

            m_Item.SetName(newName);

            Assert.That(m_Item.Name, Is.EqualTo(initialName));
        }

        [Test]
        [TestCase(-1, false)]
        [TestCase(0, true)]
        [TestCase(1, true)]
        public void Item_SetWeight_Set(int newWeight, bool isSet)
        {
            var initialWeight = m_Item.Weight;

            m_Item.SetWeight(newWeight);

            Assert.That(m_Item.Weight, Is.EqualTo(isSet ? newWeight : initialWeight));
        }

        [Test]
        public void Item_SetDescription_DescriptionSet()
        {
            const string testDescription = "This is a test description";
            var initialDescription = m_Item.Description;

            Assert.That(testDescription, Is.Not.EqualTo(initialDescription));

            m_Item.SetDescription(testDescription);

            Assert.That(m_Item.Description, Is.EqualTo(testDescription));
        }

        [Test]
        public void Item_SetStats_StatsSet()
        {
            const string testStats = "{ Health: 0 }";
            var initialStats = m_Item.Stats;

            Assert.That(testStats, Is.Not.EqualTo(initialStats));

            m_Item.SetStats(testStats);

            Assert.That(m_Item.Stats, Is.EqualTo(testStats));
        }

        [Test]
        public void Item_SetOwner_OwnerSet()
        {
            var newOwner = new object();
            var initialOwner = m_Item.Owner;

            Assert.That(newOwner, Is.Not.EqualTo(initialOwner));

            m_Item.SetOwner(newOwner);

            Assert.That(m_Item.Owner, Is.EqualTo(newOwner));
        }
    }
}
