using TMPro;
using UnityEngine;

//Класс отвечающий за логику подсказки о предмете
public class Tooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text Name;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private TMP_Text Type;

    public void Show(ItemShower shower)
    {
        Name.text = shower.GetContainer().CurrentItem.Name;
        Description.text = shower.GetContainer().CurrentItem.Description;
        string str = "Тип: ";
        switch(shower.GetContainer().CurrentItem.Type)
        {
            case InventoryItem.ItemType.Weapon:
                {
                    str += "Оружие";
                }
                break;
            case InventoryItem.ItemType.Armor:
                {

                    str += "Броня";
                }
                break;
            case InventoryItem.ItemType.Potion:
                {

                    str += "Зелье";
                }
                break;
            case InventoryItem.ItemType.Quest:
                {

                    str += "Задание";
                }
                break;
        }
        Type.text = str;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
