using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Класс для обработки событий взаимодействия мышкой с предметом
public class InteractionProvider : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Range(0f, 1f)]
    public float doubleClickTime = 0.3f;
    private float _lastClickTime = 0f;

    [Range(0f, 5f)]
    public float pointerEnterDelayTime = 1f;
    private float _lasEnterTime = 0f;
    private bool _isPointerEnterTriggered = false;
    private bool _isPointerInside = false;

    #region Events
    #region Click
    public UnityEvent<GameObject> OnLeftClick;
    public UnityEvent<GameObject> OnDoubleLeftClick;
    public UnityEvent<GameObject> OnRightClick;
    public UnityEvent<GameObject> OnMiddleClick;
    #endregion
    #region Drag
    public UnityEvent<GameObject> OnDragBegin;
    public UnityEvent OnDragContinue;
    public UnityEvent<PointerEventData> OnDragEnd;
    #endregion
    #region Enter
    public UnityEvent<GameObject> OnMouseEnterWithDelay;
    public UnityEvent OnMouseExit;
    #endregion
    #endregion


    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                {
                    if (Time.time - _lastClickTime <= doubleClickTime)
                    {
                        OnDoubleLeftClick?.Invoke(gameObject);
                    }
                    else
                    {
                        OnLeftClick?.Invoke(gameObject);
                        _lastClickTime = Time.time;
                    }
                }
                break;
            case PointerEventData.InputButton.Right:
                {
                    OnRightClick?.Invoke(gameObject);
                }
                break;
            case PointerEventData.InputButton.Middle:
                {
                    OnMiddleClick?.Invoke(gameObject);
                }
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnDragBegin?.Invoke(gameObject);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEnd?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragContinue?.Invoke();
    }

    public async void OnPointerEnter(PointerEventData eventData)
    {
        _lasEnterTime = Time.time;
        _isPointerInside = true;
        await UniTask.WaitForSeconds(pointerEnterDelayTime);
        if(Time.time - _lasEnterTime >= pointerEnterDelayTime && _isPointerInside)
        {
            _isPointerEnterTriggered = true;
            OnMouseEnterWithDelay?.Invoke(gameObject);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerInside = false;
        if(_isPointerEnterTriggered)
        {
            _isPointerEnterTriggered = false;
            OnMouseExit?.Invoke();
        }
    }
}
