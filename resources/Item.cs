using Godot;

public class Item : Resource {
    [Export]
    public string name = "";
    [Export]
    public Texture texture;
    [Export]
    public bool isStackable = true;
    [Export]
    public int maxStacked = 100;
}
