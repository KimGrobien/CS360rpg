using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {
    private TextMeshProUGUI equipmentName, equipmentDetails, EgosMoney;
    private int currentItem;
    private Color TextColor;
    private Button[] equip = new Button[15];
    private Image EgosPrimary,EgosSecondary,EgosDefense, equipmentImage;
    private Button primaryButton, secondaryButton, defenseButton, buyButton, enableBuying;
    private Button exitMM, exitGame;
    private GameObject exitButtons;
    private equipmentData buying;

    /// <summary>
    /// When ever the main menu is closed, if buying mode is currently enabled, disable it 
    /// and set all unowned items back to invisable
    /// </summary>
    private void OnDestroy()
    {
        // if buying mode was left on, reset everything
        if (GameInfo.buyingMode)
        {
            GameInfo.buyingMode = false;
            for (int i = 0; i < 12; i++)
            {
                if (!(i == 3 || i == 7 || i == 11))
                {
                    if (!GameInfo.getEquipment(i).owned)
                    {
                        GameInfo.setEquipmentColor(i, Color.clear);
                        equip[i].image.color = GameInfo.getEquipment(i).Visability;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Used for initialization of menu, can enable it to refresh everything, this refresh is done 
    /// in engage in dialogue when not interacting with an NPC
    /// sets up all the button listeners and displays the correct items in inventory
    /// </summary>
    void OnEnable () {

        //exitMM = GameObject.Find("ExitMenu").GetComponent<Button>();
        //exitGame = GameObject.Find("ExitGame").GetComponent<Button>();
        //if (GameInfo.currentNPC == -1)
        //{
        //    exitMM.onClick.AddListener(ExitTheMainMenu);
        //    exitGame.onClick.AddListener(ExitGame);
        //}
        //else
        //{
        //    exitButtons = GameObject.Find("ExitMenu");
        //    exitButtons.SetActive(false);
        //    exitButtons = GameObject.Find("ExitGame");
        //    exitButtons.SetActive(false);
        //}

        // Set on Click-listener for each item slot and set the correct visability color
        for (int i = 0; i < 12; i++)
        {
            equip[i] = GameObject.Find("slot" + i).GetComponent<Button>();
            int set = i;
            equip[set].image.color = GameInfo.getEquipment(set).Visability;
            equip[set].onClick.AddListener(() => ItemClicked(set));
        }
        
        // Determine if bounty object is owned and display them, set bountyListener
        for (int i = 12; i <15; i++)
        {
            equip[i] = GameObject.Find("slot" + i).GetComponent<Button>();
            int set = i;
            if (GameInfo.getOwnedStatus(set))
            {
                GameInfo.setEquipmentColor(set, Color.white);
            }
            else
            {
                GameInfo.setEquipmentColor(set, Color.clear);
            }
            equip[set].image.color = GameInfo.getEquipment(set).Visability;
            equip[set].onClick.AddListener(() => BountyClicked(set));
        }

        // Display Ego's Equipped Items
        EgosPrimary = GameObject.Find("EquipSlot0").GetComponent<Image>();
        if (GameInfo.getEquipped(0).eqImage != null)
        {
            EgosPrimary.color = Color.white;
            EgosPrimary.sprite = GameInfo.getEquipped(0).eqImage;
        }
        EgosSecondary = GameObject.Find("EquipSlot1").GetComponent<Image>();
        if (GameInfo.getEquipped(1).eqImage != null)
        {
            EgosSecondary.color = Color.white;
            EgosSecondary.sprite = GameInfo.getEquipped(1).eqImage;
        }
        EgosDefense = GameObject.Find("EquipSlot2").GetComponent<Image>();
        if (GameInfo.getEquipped(2).eqImage != null)
        {
            EgosDefense.color = Color.white;
            EgosDefense.sprite = GameInfo.getEquipped(2).eqImage;
        }

        // Set Listeners for each button in the equipment side of main menu
        primaryButton = GameObject.Find("PrimaryB").GetComponent<Button>();
        primaryButton.onClick.AddListener(() => PrimaryButtonClicked(currentItem));

        secondaryButton = GameObject.Find("SecondaryB").GetComponent<Button>();
        secondaryButton.onClick.AddListener(() => SecondaryButtonClicked(currentItem));

        defenseButton = GameObject.Find("DefenseB").GetComponent<Button>();
        defenseButton.onClick.AddListener(() => DefenseButtonClicked(currentItem));

        buyButton = GameObject.Find("BuyB").GetComponent<Button>();
        buyButton.onClick.AddListener(() => BuyButtonClicked(currentItem));

        enableBuying = GameObject.Find("Trade").GetComponent<Button>();
        enableBuying.onClick.AddListener(() => toggleBuyingMode());

        // Set each of these buttons to not interactable to start
        SetButtonsVisablity(false, false, false, false);

        // Set all information section to empty strings and images when loading
        equipmentName = GameObject.Find("EqName").GetComponent<TextMeshProUGUI>();
        equipmentName.text = "";
        equipmentDetails = GameObject.Find("EqInfo").GetComponent<TextMeshProUGUI>();
        equipmentDetails.text = "";
        equipmentImage = GameObject.Find("EqImage").GetComponent<Image>();

        // Display Ego's current money count
        EgosMoney = GameObject.Find("MoneyCount").GetComponentInChildren<TextMeshProUGUI>();
        TextColor = EgosMoney.color;
        EgosMoney.text = "$" + GameInfo.getMoney();
    }
	
    // Display the item name and stats
    // Also enable buttons that are avalible (such as buy, equip as primary, equip as secondary, or equip as defense)
    // Make sure to pass id of each item
    private void ItemClicked(int i)
    {
        Debug.Log("itemClicked");
        Debug.Log("ObjectClicked: " + i);
        Debug.Log("Wooden Sheild owned? " + GameInfo.getOwnedStatus(0));
        Debug.Log("Wooden Sheild Equipped? " + GameInfo.getEquipment(0).equipped);
        // are you clicking on a gift?
        bool gift = false;
        if (i == 3 || i == 7 || i == 11)
        {
            gift = true;
        }

        // For each object during buying mode (shop items only)
        for (int j = 0; j < 12; j++)
        {
            if (!(j == 3 || j == 7 || j == 11))
            {
                if (!GameInfo.getOwnedStatus(j) && GameInfo.buyingMode)
                {
                    GameInfo.setEquipmentColor(j, Color.gray);
                    equip[j].image.color = GameInfo.getEquipment(j).Visability;
                }
            }
        }

        // You own the object or in buying mode and not gift
        if (GameInfo.getOwnedStatus(i) || (GameInfo.buyingMode && !gift))
        {
            currentItem = i;
            equipmentName.text = GameInfo.getEquipment(i).name;
            equipmentDetails.text = GameInfo.getEquipment(i).description;
            equipmentImage.overrideSprite = GameInfo.getEquipment(i).eqImage;
            equipmentImage.color = Color.white;
        }

        // Buying Mode, ignore gifts
        if (GameInfo.buyingMode && !gift)
        {
            // and you have enough Money
            if (!GameInfo.getOwnedStatus(i) && GameInfo.getEquipment(i).Price <= GameInfo.getMoney())
            {
                SetButtonsVisablity(false, false, false, true);
                EgosMoney.color = Color.green;
                equip[i].image.color = Color.green;
            }
            // Dont have enough money
            else if (!GameInfo.getOwnedStatus(i) && GameInfo.getEquipment(i).Price > GameInfo.getMoney())
            {
                SetButtonsVisablity(false, false, false, false);
                EgosMoney.color = Color.red;
                equip[i].image.color = Color.red;
            }
            // Buying mode but you already own the object
            else if (GameInfo.getOwnedStatus(i))
            {
                SetButtonsVisablity(false, false, false, false);
                EgosMoney.color = TextColor;
            }
        }

        // you own the object and its not in buying mode and its not already equipped
        else if (GameInfo.getOwnedStatus(i) && !GameInfo.buyingMode && !GameInfo.getEquipment(i).equipped)
        {
            EgosMoney.color = TextColor;
            //if attack object
            //Debug.Log("ObjectClicked: " + i);
            if (i < 8 || i == 11)
            {
                //Debug.Log("ObjectClicked: " + i);
                SetButtonsVisablity(true, true, false, false);
            }
            // if defense object
            else if (i > 7 && i < 11)
            {
                SetButtonsVisablity(false, false, true, false);
            }
            //if nothing
            else
            {
                SetButtonsVisablity(false, false, false, false);
            }
        }

        if (GameInfo.getEquipment(i).equipped)
        {
            SetButtonsVisablity(false, false, false, false);
        }
    }

    /// <summary>
    /// If a bounty item that is owned is clicked in the main menu it displays its stats
    /// </summary>
    /// <param name="i"> This is the item index in GameInfo, used to get information of item</param>
    private void BountyClicked(int i)
    {
        if (GameInfo.getOwnedStatus(i))
        {
            currentItem = -1;
            equipmentName.text = GameInfo.getEquipment(i).name;
            equipmentDetails.text = GameInfo.getEquipment(i).description;
            equipmentImage.overrideSprite = GameInfo.getEquipment(i).eqImage;
            equipmentImage.color = Color.white;
            SetButtonsVisablity(false, false, false, false);
        }
    }

    /// <summary>
    /// Sets Visablity of the four option buttons for equipment
    /// </summary>
    /// <param name="pri">Set interactablity of Primary button to this</param>
    /// <param name="sec">Set interactablity of secondary button to this</param>
    /// <param name="def">Set interactablity of defense button to this</param>
    /// <param name="buy">Set interactablity of buy button to this</param>
    private void SetButtonsVisablity(bool pri, bool sec, bool def, bool buy)
    {
        primaryButton.interactable = pri;
        secondaryButton.interactable = sec;
        buyButton.interactable = buy;
        defenseButton.interactable = def;
    }

    /// <summary>
    /// Equip the item to the designated slot and disable it from being eqipped again
    /// until another item replaces it
    /// </summary>
    /// <param name="i">This is the item to be equipped</param>
    private void PrimaryButtonClicked(int i)
    {
        primaryButton.interactable = false;
        secondaryButton.interactable = false;
        EgosPrimary.color = Color.white;
        EgosPrimary.sprite = GameInfo.getEquipment(i).eqImage;
        GameInfo.UpdateEgosPrimary(GameInfo.getEquipment(i));
        GameInfo.setEquipment(0, i);
        GameInfo.toggleEquipped(GameInfo.equippedIndexes[0]);
        GameInfo.equippedIndexes[0] = i;
        GameInfo.toggleEquipped(i);
    }
    private void SecondaryButtonClicked(int i)
    {
        primaryButton.interactable = false;
        secondaryButton.interactable = false;
        EgosSecondary.color = Color.white;
        EgosSecondary.sprite = GameInfo.getEquipment(i).eqImage;
        GameInfo.UpdateEgosSecondary(GameInfo.getEquipment(i));
        GameInfo.setEquipment(1, i);
        GameInfo.toggleEquipped(GameInfo.equippedIndexes[1]);
        GameInfo.equippedIndexes[1] = i;
        GameInfo.toggleEquipped(i);
    }
    private void DefenseButtonClicked(int i)
    {
        defenseButton.interactable = false;
        EgosDefense.color = Color.white;
        EgosDefense.sprite = GameInfo.getEquipment(i).eqImage;
        GameInfo.UpdateEgosDefense(GameInfo.getEquipment(i));
        GameInfo.setEquipment(2, i);
        GameInfo.toggleEquipped(GameInfo.equippedIndexes[2]);
        GameInfo.equippedIndexes[2] = i;
        GameInfo.toggleEquipped(i);
    }

    /// <summary>
    /// Buy item! Whoop Whoop!
    /// </summary>
    /// <param name="i"></param>
    private void BuyButtonClicked(int i)
    {
        GameInfo.setEquipmentColor(i, Color.white);
        GameInfo.setEquipmentOwned(i);
        equip[i].image.color = GameInfo.getEquipment(i).Visability;
        GameInfo.reduceMoney(GameInfo.getEquipment(i).Price);
        EgosMoney.text = "$" + GameInfo.getMoney();
        buyButton.interactable = false;
        EgosMoney.color = TextColor;
    }

    /// <summary>
    /// When the trade button is pressed, show the avaliable items to buy
    /// When exiting trad hide the items that have yet to be bought
    /// </summary>
    public void toggleBuyingMode()
    {
        EgosMoney.color = TextColor;
        SetButtonsVisablity(false, false, false, false);
        equipmentName.text = "";
        equipmentDetails.text = "";
        equipmentImage.color = Color.clear;

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

    public static void ExitTheMainMenu()
    {
        SceneManager.LoadScene(GameInfo.prevScene);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
