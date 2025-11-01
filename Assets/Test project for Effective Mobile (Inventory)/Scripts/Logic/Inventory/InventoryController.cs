using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//Класс контролирующий поведение инвентаря
public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

    [SerializeField] private List<InventorySlot> Slots;

    public UnityEvent OnInventoryFull;
    public UnityEvent<InventoryItem, int> OnItemAdded;
    public UnityEvent<InventoryItem> OnItemEquipped;
    public UnityEvent<InventoryItem> OnItemUsed;
    public UnityEvent<InventoryItem> OnItemDroped;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    public void AddItem(InventoryItem item, int count = 1)
    {
        if (item.CanStack)
        {
            InventorySlot temp = Slots.Where(x => !x.IsFree).FirstOrDefault(x => x.Name == item.Name);
            if (temp != null)
            {
                temp.AddCount(count);
                OnItemAdded?.Invoke(item, count);
                return;
            }
        }

        InventorySlot firstEmpty = Slots.FirstOrDefault(x=>x.IsFree);

        if (firstEmpty != null)
        {
            firstEmpty.AddNewItem(item, count);
            OnItemAdded?.Invoke(item, count);
        }
        else
        {
            OnInventoryFull?.Invoke();
        }
    }

    public void UseItem(GameObject shower)
    {
        InventorySlot slot = shower.GetComponent<ItemShower>().GetContainer();

        switch(slot.CurrentItem.Type)
        {
            case InventoryItem.ItemType.Armor:
            case InventoryItem.ItemType.Weapon:
                {
                    OnItemEquipped?.Invoke(slot.CurrentItem);
                    slot.AddCount(-1);
                }
                break;
            case InventoryItem.ItemType.Potion:
                {
                    OnItemUsed?.Invoke(slot.CurrentItem);
                    slot.AddCount(-1);
                }
                break;
        }
    }

    public void DropItem(GameObject shower)
    {
        ItemShower itemShower = shower.GetComponent<ItemShower>();
        InventorySlot slot = itemShower.GetContainer();

        switch (slot.CurrentItem.Type)
        {
            case InventoryItem.ItemType.Armor:
            case InventoryItem.ItemType.Weapon:
            case InventoryItem.ItemType.Potion:
                {
                    OnItemDroped?.Invoke(slot.CurrentItem);
                    slot.Clear();
                    itemShower.ReturnToParent();

                }
                break;
            case InventoryItem.ItemType.Quest:
                {
                    itemShower.ReturnToParent();
                }
                break;
        }
    }

}
