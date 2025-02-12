using Unity.VisualScripting;

[System.Serializable]
public class Item
{
    public string itemName;
    public int Strength;
    public string Type;
    public string Description;
    public string Debuffs;
    public string ImagePath;

    public Item(string name,int strngth,string Type,string Description,string Debuffs,string ImagePath)
    {
        this.itemName = name;
        this.Strength = strngth;
        this.Type = Type;
        this.Description = Description;
        this.Debuffs = Debuffs;
        this.ImagePath = ImagePath;
    }
}