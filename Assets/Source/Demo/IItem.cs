namespace InventoryDemo
{
    public interface IItem
    {
        event System.Action<IItem> OnUse;

        void Use() { }

        string Name { get; }
        int Weight { get; }
        string Description { get; }
        string Stats { get; }

        object Owner { get; }

        void SetWeight(int weight);
        void SetName(string name);
        void SetDescription(string description);
        void SetStats(string stats);
        void SetOwner(object owner);
    }
}
