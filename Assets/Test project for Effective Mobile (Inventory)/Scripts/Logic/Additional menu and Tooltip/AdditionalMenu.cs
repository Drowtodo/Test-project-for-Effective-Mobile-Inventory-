using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ласс отвечающий за логику дополнительного меню по взаимодействию с предметом
public class AdditionalMenu : MonoBehaviour
{
    [SerializeField] private Button DropButton;
    [SerializeField] private Button UseButton;
    [SerializeField] private Button InfoButton;

    public ItemShower CurrentShower { get; private set; }

    public void Show(ItemShower shower)
    {
        CurrentShower = shower;

        switch(CurrentShower.GetContainer().CurrentItem.Type)
        {
            case InventoryItem.ItemType.Weapon:
            case InventoryItem.ItemType.Armor:
                {
                    ChangeButtonText(UseButton, "Ёкипировать");
                    UseButton.gameObject.SetActive(true);
                    DropButton.gameObject.SetActive(true);
                }
                break;
            case InventoryItem.ItemType.Potion:
                {
                    ChangeButtonText(UseButton, "»спользовать");
                    UseButton.gameObject.SetActive(true);
                    DropButton.gameObject.SetActive(true);
                }
                break;
            case InventoryItem.ItemType.Quest:
                {
                    UseButton.gameObject.SetActive(false);
                    DropButton.gameObject.SetActive(false);
                }
                break;
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ChangeButtonText(Button button, string text)
    {
        button.GetComponentInChildren<TMP_Text>().text = text;
    }

    public void Drop()
    {
        InventoryController.Instance.DropItem(CurrentShower.gameObject);
    }

    public void Use()
    {
        InventoryController.Instance.UseItem(CurrentShower.gameObject);
    }
}
