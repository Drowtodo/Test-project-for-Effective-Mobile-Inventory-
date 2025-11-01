using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Простое добавление предметов в инвентарь
public class SimpleInventoryAdder : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown ItemsChoser;
    [SerializeField] private List<InventoryItem> Items;

    public void Add()
    {
        InventoryController.Instance.AddItem(Items[ItemsChoser.value]);
    }
}
