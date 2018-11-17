using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Controls the equipment grid, not sure what Transform itemsParent really does
public class Inventory : MonoBehaviour {

    [SerializeField] List<equipment> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] itemSlot[] itemSlots;

    private void OnValidate()
    {
        if(itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<itemSlot>();
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = items[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }
}