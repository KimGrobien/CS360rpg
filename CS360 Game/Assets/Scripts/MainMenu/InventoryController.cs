using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {
    private TextMeshProUGUI equipmentName;
    private int currentItem;
    private Button[] equip = new Button[15];
    private Image EgosPrimary,EgosSecondary,EgosDefense;
    private Button primaryButton, secondaryButton, defenseButton, buyButton, enableBuying;
    private equipmentData buying;
    Text EgosMoney;
    // Use this for initialization
    void Start () {
        for (int i = 0; i < 15; i++)
        {
            equip[i] = GameObject.Find("slot" + i).GetComponent<Button>();
            int set = i;
            equip[i].onClick.AddListener(() => ItemClicked(set));
        }

        primaryButton = GameObject.Find("PrimaryB").GetComponent<Button>();
        primaryButton.onClick.AddListener(() => PrimaryButtonClicked(currentItem));

        secondaryButton = GameObject.Find("SecondaryB").GetComponent<Button>();
        secondaryButton.onClick.AddListener(() => SecondaryButtonClicked(currentItem));

        defenseButton = GameObject.Find("DefenseB").GetComponent<Button>();
        defenseButton.onClick.AddListener(() => DefenseButtonClicked(currentItem));

        buyButton = GameObject.Find("BuyB").GetComponent<Button>();
        buyButton.onClick.AddListener(() => BuyButtonClicked(currentItem));

        enableBuying = GameObject.Find("enableBuy").GetComponent<Button>();
        enableBuying.onClick.AddListener(() => toggleBuyingMode());

        primaryButton.interactable = false;
        secondaryButton.interactable = false;
        buyButton.interactable = false;
        defenseButton.interactable = false;

        EgosMoney = GameObject.Find("MoneyCount").GetComponent<Text>();
        //EgosMoney.text = "" + GameInfo.getMoney();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Display the item name and stats
    // Also enable buttons that are avalible (such as buy, equip as primary, equip as secondary, or equip as defense)
    // Make sure to pass id of eaah item
    private void ItemClicked(int i)
    {
        
        for (int j = 0; j < 12; j++)
        {
            if (!(j == 3 || j == 7 || j == 11))
            {
                if (!GameInfo.getEquipment(j).owned && GameInfo.buyingMode)
                {
                    GameInfo.setEquipmentColor(j, Color.gray);
                    equip[j].image.color = GameInfo.getEquipment(j).Visability;
                }
            }
        }
        //Image equipImg;
        //equipImg = GameObject.Find("slot" + (i)).GetComponent<Image>();
        //equipImg.enabled = !equipImg.enabled;
        if (GameInfo.getEquipment(i).owned || GameInfo.buyingMode)
        {
            currentItem = i;
            equipmentName = GameObject.Find("EqName").GetComponent<TextMeshProUGUI>();
            equipmentName.text = GameInfo.getEquipment(i).name;
            equipmentName = GameObject.Find("EqInfo").GetComponent<TextMeshProUGUI>();
            equipmentName.text = GameInfo.getEquipment(i).description;
        }

        //Check if equipment is attack or defense or buying
        if (!GameInfo.getEquipment(i).owned && GameInfo.buyingMode && GameInfo.getEquipment(i).Price < GameInfo.getMoney())
        {
            primaryButton.interactable = false;
            secondaryButton.interactable = false;
            buyButton.interactable = true;
            defenseButton.interactable = false;
            equip[i].image.color = Color.red;
        } else if(GameInfo.getEquipment(i).owned && GameInfo.buyingMode) {
            primaryButton.interactable = false;
            secondaryButton.interactable = false;
            buyButton.interactable = false;
            defenseButton.interactable = false;
        }
        else if (GameInfo.getEquipment(i).owned && !GameInfo.buyingMode){
            //if attack
            if (i >= 0 && i < 8 || i == 11)
            {
                primaryButton.interactable = true;
                secondaryButton.interactable = true;
                buyButton.interactable = false;
                defenseButton.interactable = false;
            }
            else if (i > 7 && i < 11)
            {
                primaryButton.interactable = false;
                secondaryButton.interactable = false;
                buyButton.interactable = false;
                defenseButton.interactable = true;
            }
            else
            {
                primaryButton.interactable = false;
                secondaryButton.interactable = false;
                buyButton.interactable = false;
                defenseButton.interactable = false;
            }
        }
    }

    private void PrimaryButtonClicked(int i)
    {
        EgosPrimary = GameObject.Find("EquipSlot0").GetComponent<Image>();
        EgosPrimary.color = Color.white;
        EgosPrimary.sprite = GameInfo.getEquipment(i).eqImage;
        GameInfo.UpdateEgosPrimary(GameInfo.getEquipment(i));
    }

    private void SecondaryButtonClicked(int i)
    {
        EgosSecondary = GameObject.Find("EquipSlot1").GetComponent<Image>();
        EgosSecondary.color = Color.white;
        EgosSecondary.sprite = GameInfo.getEquipment(i).eqImage;
        GameInfo.UpdateEgosSecondary(GameInfo.getEquipment(i));
    }

    private void DefenseButtonClicked(int i)
    {
        EgosDefense = GameObject.Find("EquipSlot2").GetComponent<Image>();
        EgosDefense.color = Color.white;
        EgosDefense.sprite = GameInfo.getEquipment(i).eqImage;
        GameInfo.UpdateEgosDefense(GameInfo.getEquipment(i));
    }

    private void BuyButtonClicked(int i)
    {
        GameInfo.setEquipmentColor(i, Color.white);
        GameInfo.setEquipmentOwned(i);
        equip[i].image.color = GameInfo.getEquipment(i).Visability;
        GameInfo.reduceMoney(GameInfo.getEquipment(i).Price);
        //EgosMoney.text = "" + GameInfo.getMoney();
        buyButton.interactable = false;
    }

    private void toggleBuyingMode()
    {
        GameInfo.buyingMode = !GameInfo.buyingMode;
        for (int i = 0; i < 12; i++)
        {
            if (!(i == 3 || i == 7 || i == 11))
            {
                if (!GameInfo.getEquipment(i).owned && GameInfo.buyingMode)
                {
                    GameInfo.setEquipmentColor(i, Color.gray);
                    equip[i].image.color = GameInfo.getEquipment(i).Visability;
                }
                else if (!GameInfo.getEquipment(i).owned && !GameInfo.buyingMode)
                {
                    GameInfo.setEquipmentColor(i, Color.clear);
                    equip[i].image.color = GameInfo.getEquipment(i).Visability;
                }
            }
        }
    }
}
