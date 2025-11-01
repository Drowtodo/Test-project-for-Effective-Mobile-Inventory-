using UnityEngine;
using UnityEngine.EventSystems;

//Зона для быстрого выбросывания предметов из инвентаря
public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        InventoryController.Instance.DropItem(DragController.Instance.DragObject);
    }
}
