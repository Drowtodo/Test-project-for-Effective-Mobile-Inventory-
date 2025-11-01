using TMPro;
using UnityEngine;

//Класс для простого вывода происходящего в инвентаре
public class Loger : MonoBehaviour
{
    [SerializeField] private TMP_Text Log;

    public void OnInventoryFull()
    {
        Log.text = "Инвентарь заполнен!";
    }

    public void OnItemAdded(InventoryItem item, int count)
    {
        Log.text = $"В инвентарь добавлено {count} \"{item.Name}\".";
    }

    public void OnItemEquipped(InventoryItem item)
    {
        Log.text =  $"{item.Name} экепирован.";
    }

    public void OnItemUsed(InventoryItem item)
    {
        Log.text = $"{item.Name} использован.";
    }

    public void OnItemDroped(InventoryItem item)
    {
        Log.text = $"{item.Name} выброшен.";
    }
}
