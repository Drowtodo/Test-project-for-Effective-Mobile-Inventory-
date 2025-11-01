using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Класс отвечающий за перетаскивание предметов инвентаря
public class DragController : MonoBehaviour
{
    public static DragController Instance;

    public GameObject DragObject { get; private set; }
    private CanvasGroup _dragCanvasGroup;

    [SerializeField]private GraphicRaycaster Raycaster;
    private void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void BeginDrag(GameObject obj)
    {
        DragObject = obj;
        DragObject.transform.SetParent(transform);
        _dragCanvasGroup = DragObject.GetComponent<CanvasGroup>();
        _dragCanvasGroup.blocksRaycasts = false;
        _dragCanvasGroup.alpha = 0.6f;
    }

    public void EndDrag(PointerEventData eventData)
    {
        _dragCanvasGroup.blocksRaycasts = true;
        _dragCanvasGroup.alpha = 1f;

        List<RaycastResult> raycastResults = new();
        Raycaster.Raycast(eventData, raycastResults);
        if(raycastResults.TrueForAll(x=>!x.gameObject.TryGetComponent<IDropHandler>(out _)))
        {
            DragObject.GetComponent<ItemShower>().ReturnToParent();
        }
        DragObject = null;
        _dragCanvasGroup = null;
    }

    public void OnDragContinue()
    {
        DragObject.transform.position = Input.mousePosition;
    }
}
