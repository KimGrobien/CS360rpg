using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

// Controls the equipment grid, not sure what Transform itemsParent really does
public class Inventory : MonoBehaviour {

    [SerializeField] List<equipment> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] itemSlot[] itemSlots;
    private Button[] equip = new Button[15];

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
            equip[i] = GameObject.Find("slot" + i).GetComponent<Button>();
            equip[i].image.color = GameInfo.getEquipment(i).Visability;
            itemSlots[i].Item = items[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }
}