using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Класс отвечающий за логику отображения предметов в инвентаре
[RequireComponent(typeof(CanvasGroup))]
public class ItemShower : MonoBehaviour
{
    [SerializeField] private TMP_Text Name;
    [SerializeField] private TMP_Text Count;
    [SerializeField] private Image Icon;

    [SerializeField] private InventorySlot ParentContainer;
    private CanvasGroup CanvasGroup;

    private void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Set(InventoryItem item = null)
    {
        if (item == null)
        {
            gameObject.SetActive(false);
            return;
        }

        Name.text = item.Name;
        Count.text = ParentContainer.Count.ToString();
        Icon.sprite = item.Icon;
        gameObject.SetActive(true);
    }

    public void ChangeCount(int count)
    {
        Count.text = count.ToString();

        if (count < 2)
        {
            Count.gameObject.SetActive(false);
        }
        else
        {
            Count.gameObject.SetActive(true);
        }
    }

    public void ReturnToParent()
    {
        transform.SetParent(ParentContainer.transform);
        transform.localPosition = Vector3.zero;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1;
    }

    public InventorySlot GetContainer()
    {
        return ParentContainer;
    }
}
