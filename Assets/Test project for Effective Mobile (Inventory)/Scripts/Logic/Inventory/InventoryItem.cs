using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public ItemType Type;
    public bool CanStack;

    public enum ItemType
    {
        Armor,
        Weapon,
        Quest,
        Potion
    }
}
