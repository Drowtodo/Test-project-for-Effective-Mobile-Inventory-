using UnityEngine;
using UnityEngine.EventSystems;

//Класс контролирующий логику поведения дополнительного меню/подсказки
public class AdditionalMenuController : MonoBehaviour
{
    [SerializeField] private Tooltip Tooltip;
    [SerializeField] private AdditionalMenu Menu;

    private State _activeState = State.None;

    private enum State
    {
        None,
        Tooltip,
        Menu
    }

    public void OpenTooltip(GameObject shower)
    {
        if(_activeState == State.Menu)
        {
            return;
        }

        Move();
        gameObject.SetActive(true);
        Tooltip.Show(shower.GetComponent<ItemShower>());
        _activeState = State.Tooltip;
    }

    public void CloseTooltip()
    {
        if(_activeState == State.Menu)
        {
            return;
        }

        gameObject.SetActive(false);
        Tooltip.Hide();
        _activeState = State.None;
    }


    public void ForceTooltipOPen()
    {
        Menu.Hide();
        Tooltip.Show(Menu.CurrentShower);
        _activeState = State.Tooltip;
    }

    public void OpenAdditionalMenu(GameObject shower)
    {
        if(_activeState == State.Tooltip)
        {
            Tooltip.Hide();
        }

        Move();
        Menu.Show(shower.GetComponent<ItemShower>());
        gameObject.SetActive(true);
        _activeState = State.Menu;
    }

    private void Move()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 offs = new(rectTransform.sizeDelta.x / 2 + 5, -rectTransform.sizeDelta.y / 2 - 5);
        Vector3 tryPos = Input.mousePosition + offs;
        Vector2 screenSize = new(Screen.currentResolution.width, Screen.currentResolution.height);
        if(tryPos.x + rectTransform.sizeDelta.x + 5 > screenSize.x)
        {
            tryPos = new(screenSize.x -  rectTransform.sizeDelta.x/2 -5, tryPos.y);
        }

        if(tryPos.y - rectTransform.sizeDelta.y - 5 < 0)
        {
            tryPos = new(tryPos.x, rectTransform.sizeDelta.y/2 + 5);
        }

        transform.position = tryPos;
    }

    public void Hide()
    {
        switch(_activeState)
        {
            case State.Menu:
                {
                    Menu.Hide();
                }
                break;
            case State.Tooltip:
                {
                    Tooltip.Hide();
                }
                break;
        }

        _activeState = State.None;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                GameObject clickedObject = GetClickedUIObject();

                if (clickedObject != null && !IsDescendantOf(clickedObject.transform, transform))
                {
                    Hide();
                }
            }
            else
            {
                Hide();
            }
        }
    }

    private GameObject GetClickedUIObject()
    {
        PointerEventData pointerData = new(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
            return results[0].gameObject;

        return null;
    }

    private bool IsDescendantOf(Transform child, Transform potentialParent)
    {
        while (child != null)
        {
            if (child == potentialParent)
                return true;
            child = child.parent;
        }
        return false;
    }
}
