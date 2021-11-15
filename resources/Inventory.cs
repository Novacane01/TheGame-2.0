using Godot;

public class Inventory : Resource {
    private const int NUMBER_OF_SLOTS = 7;




    [Export]
    public Item[] items = new Item[NUMBER_OF_SLOTS] { null, null, null, null, null, null, null };
    [Export]
    public bool isStackable = true;
    [Export]
    public int maxStacked = 100;

    [Signal]
    public delegate void ItemChanged(int[] indicies);

    public void EmitItemChanged(int[] indicies) {
        EmitSignal("ItemChanged", indicies);
    }

    Item setItem(int index, Item newItem) {
        if (boundsValid(index)) {
            Item previousItem = items[index];
            items[index] = newItem;
            EmitItemChanged(new int[] { index });
            return previousItem;
        }
        return null;
    }

    Item removeItem(int index) {
        if (boundsValid(index)) {
            Item previousItem = items[index];
            items[index].texture = null;
            EmitItemChanged(new int[] { index });
            return previousItem;
        }
        return null;
    }

    private bool boundsValid(int index) {
        return index >= 0 && index < NUMBER_OF_SLOTS;
    }
}

