using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {
    private TextMeshProUGUI equipmentName;
    private Button[] equip = new Button[12];
    private Button primaryButton, secondaryButton, defenseButton, buyButton;
    Text stat1, stat2, stat3;
    // Use this for initialization
    void Start () {
        for (int i = 0; i < 12; i++)
        {
            equip[i] = GameObject.Find("slot" + i).GetComponent<Button>();
            int set = i;
            equip[i].onClick.AddListener(() => ItemClicked(set));
        }

        primaryButton = GameObject.Find("PrimaryB").GetComponent<Button>();
        primaryButton.onClick.AddListener(() => ItemClicked(1));

        secondaryButton = GameObject.Find("SecondaryB").GetComponent<Button>();
        secondaryButton.onClick.AddListener(() => ItemClicked(1));

        defenseButton = GameObject.Find("DefenseB").GetComponent<Button>();
        defenseButton.onClick.AddListener(() => ItemClicked(1));

        buyButton = GameObject.Find("BuyB").GetComponent<Button>();
        buyButton.onClick.AddListener(() => ItemClicked(1));

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Display the item name and stats
    // Also enable buttons that are avalible (such as buy, equip as primary, equip as secondary, or equip as defense)
    // Make sure to pass id of eaah item
    private void ItemClicked(int i)
    {
        //Image equipImg;
        //equipImg = GameObject.Find("slot" + (i)).GetComponent<Image>();
        //equipImg.enabled = !equipImg.enabled;

        int equipment = i;
        equipmentName = GameObject.Find("EqName").GetComponent<TextMeshProUGUI>();
        equipmentName.text  = "Equipment " + equipment;
    }
}
