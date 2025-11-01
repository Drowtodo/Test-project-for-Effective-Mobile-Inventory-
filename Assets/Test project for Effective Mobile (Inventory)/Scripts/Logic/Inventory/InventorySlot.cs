using UnityEngine;
using UnityEngine.EventSystems;

// ласс отвечающий за логику €чейки инвентар€
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryItem CurrentItem { get; private set; }
    [SerializeField] private ItemShower Shower;
    private int _count;

    public bool IsFree => CurrentItem == null;
    public string Name => CurrentItem.Name;

    public int Count
    {
        get {  return _count; }
        private set
        {
            if(value < 0)
            {
                Clear();
            }
            else
            {
                _count = value;
                Shower.ChangeCount(_count);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot slot = DragController.Instance.DragObject.GetComponent<ItemShower>().GetContainer();

        if(slot == this)
        {
            DragController.Instance.DragObject.GetComponent<ItemShower>().ReturnToParent();
            return;
        }

        if (CurrentItem == null || !CurrentItem.CanStack)
        {
            Swap(slot);
            return;
        }

        if(slot.CurrentItem.Name != CurrentItem.Name)
        {
            Swap(slot);
            return;
        }

        AddCount(slot.Count);
        slot.Clear();
    }

    public void AddCount(int count)
    {
        Count += count;
        if(Count == 0)
        {
            Clear();
        }
    }

    public void AddNewItem(InventoryItem item, int count = 1)
    {
        CurrentItem = item;
        Count = count;
        Shower.Set(item);
    }

    public void Clear()
    {
        CurrentItem = null;
        Count = 0;
        Shower.Set();
    }

    private void Swap(InventorySlot newSlot)
    {
        newSlot.Shower.ReturnToParent();

        (newSlot.Count, Count) = (Count, newSlot.Count);

        newSlot.Shower.Set(CurrentItem);
        Shower.Set(newSlot.CurrentItem);

        (newSlot.CurrentItem, CurrentItem) = (CurrentItem, newSlot.CurrentItem);
    }

    private void Start()
    {
        if(CurrentItem == null)
        {
            Shower.Set();
        }
    }
}
