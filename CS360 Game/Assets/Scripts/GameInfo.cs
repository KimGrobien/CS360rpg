using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Structure layout for NPC Data, which will be used to define each NPC 
public struct NPCData{
    public string name;
	public string primaryName;
	public string secondaryName;
    public int giftItemID;
	public int health;
    public int MAXhealth;
	public int primaryStat;
	public int secondaryStat;
	public int runRange;
	public int enemyDamage;
	public int enemyDamageBonus;
}

// Dialogue Node that will be used in Communitaction with NPCs
public struct Node{
    public string option1;
    public string option2;
    public string response;
    public int indexForOption1;
    public int indexForOption2;
}

// Structure that will be used to give detail to each equipment item in the game, also layout in array
public struct equipmentData
{
    public string name;
    public string description;
    public int attackBonus;
    public int defenseBonus;
    public int healBonus;
    public int Price;
    public Sprite eqImage;
    public bool equipped;
    public bool owned;
    public Color Visability;
}

public struct PartySlot{
    public int slotID;
	public NPCData npc;
    public bool isAssigned;
}

// Start of Main Class which houses all of the games global information
public class GameInfo : MonoBehaviour
{
    //VeryGlobal Data
    private static bool Gameloaded = false;

    // Ego's Data
    private static string CharacterName = "Ego";
    private static int money = 300;
    private static bool isAlive = true;
    private static int health = 100;
    public static PartySlot[] party = new PartySlot[2];
    private static equipmentData[] equippedItems = new equipmentData[3];
    public static int[] equippedIndexes = new int[3];

    //Egos Data for Combat
    //Ego wil always deal damage from 2 to 17 plus whatever bonus from the equipment
    private static int AttackRangeMIN = 2;
    private static int AttackRangeMAX = 17;
    private static int primaryBonus = 0;
    private static int secondaryBonus = 0;
    private static int primarydefenseBonus = 0;
    private static int secondarydefenseBonus = 0;
    private static int defenseBonus = 0;
    private static int egoHeal = 0;

    // Data for overworld Navigation}
    public static int prevScene = -1;
    public static Vector3 prevPos;
    public static int currentNPC = -1;
    public static int bountyOwed = 1;

    // Dont need this! bounty items are in equipmentList (ids 12, 13, 14) use equipmentList[i].owned to check and set
    public static bool[] bountiesOwed = {true, true, true};

    // For equipment
    public static bool buyingMode = false;
    // atk, defense, heal, price (buy or sell for bounty)
    private static Sprite[] equipmentSprites = new Sprite[15];
    private static int[,] equipmentStats = new int[15, 4] { { 5, 10, 0, 25 }, { 20, 20, 0, 40 }, { 50, 50, 0, 100 }, { 15, 0, 0, 0 }, { 10, 0, 0, 10 }, { 25, 0, 0, 20 }, { 50, 0, 0, 50 }, { 35, 0, 0, 0 }, { 0, 20, 0, 10 }, { 0, 30, 0, 20 }, { 0, 50, 0, 50 }, { 15, 0, 0, 0 }, { 0, 0, 0, 10 }, { 0, 0, 0, 5 }, { 0, 0, 0, 7 } };
    public static string[,] equipmentStrings = new string[15, 2] { { "Wooden Shield", "Low Protection\nLow Attack\nPrice: $25" }, { "Iron Shield", "Medium Protection\nMedium Attack\nPrice: $40" }, { "Spiked Shield", "High Protection\nHigh Attack\nPrice: $100" }, { "Scalpel", "Low Attack\nA Gift" }, { "Gila Dagger", "Low Attack\nPrice: $10" }, { "Sword", "Medium Attack\nPrice: $20" }, { "Fire Staff", "High Attack\nPrice: $50" }, { "Sickle", "Medium Attack\nA Gift" }, { "Leather Set", "Low Protection\nPrice: $10" }, { "Chainmail Set", "Medium Protection\nPrice: $20" }, { "Knight Set", "High Protection\nPrice: $50" }, { "Heal Spell", "Low Ability to Heal\nA Gift" }, { "Rock Hat", "Redeemable Bounty\nReward: $10" }, { "Rabbit Tail", "Redeemable Bounty\nReward: $5" }, { "Fox Fur", "Redeemable Bounty\nReward: $7" }, };
    private static equipmentData[] equipmentList = new equipmentData[15];

    //Data for NPC interaction/Combat
    public static NPCData[] NPCList = new NPCData[11];
    //NPC Data
    // Name, Move One, Move Two
    /*NPC ID will be array Index 
    (RECRUITABLES) Cynthia = 0, Anker(ShopKeeper) = 1, Edward(Doctor) = 2, Emrick(Farmer) = 3, 
    (SHADOWS)      Berndy(ShadowCreature) = 4, Modir(Mother) = 5, Farenvir(Father) = 6, Ozul(Antagonist) = 7,
    (BOUNTY)       Fox = 8, Rock Creature = 9, Rabbit = 10*/
    private static string[,] NPCstringData = new string[11, 3] { { "Cynthia", "Heal", "Revive" }, { "Anker", "Use Item", "Rage" }, { "Edward", "Heal", "Infect" }, { "Emrik", "Impale", "Kick" }, { "Berndy", "", "" }, { "Modir", "", "" }, { "Farenvir", "", "" }, { "Ozul", "", "" }, { "Fox", "", "" }, { "Rock Creature", "", "" }, { "Rabbit", "", "" } };
    /* Heath, PrimaryStat, SecondaryStat, RunRange (FOR first 4 (0 to 3 ID indexes) Recruitable NPCs)
        Heath, Set Attack, additional Attack Range to be added to Attack, RunRange (last 7(4 to 10 ID indexes) enemy NPCs)*/
    private static int[,] NPCintData = new int[11, 4] { { 25, 50, 0, 1 }, { 150, 150, 35, 10 }, { 75, 25, 30, 6 }, { 35, 50, 20, 8 }, { 65, 15, 5, 5 }, { 100, 35, 10, 10 }, { 120, 25, 7, 7 }, { 250, 75, 10, 100 }, { 25, 5, 2, 3 }, { 50, 10, 10, 5 }, { 25, 3, 1, 1 } };
    public static Sprite[] NPCsprites = new Sprite[11];

    //PartySlot objects for dialogue use
    public static PartySlot[] potentialNPC = new PartySlot[4];

    //DialogueTrees
    public static Node[][] DialogueTrees = new Node[11][];
    public static int[] encountered = new int[11];
    public static bool[] recruitable = {false,false,false,false,false,false,false,false,false,false,false};

    
    // Used to populate all the initial data of the game
    private void Start()
    {

        DontDestroyOnLoad(this.gameObject);
        //If you are initializing Arrays or data in this start function put it in this if statment so that it isnt reinizalized everytime you reload the start screen
        if (!Gameloaded)
        {
            Gameloaded = true;
            PopulateNPCList();
            LoadDialogue.createDialogueTrees();
            PopulateEquipmentList();
            PopulatePotentialNPCPartSlot();
        }
       // LoadDialogue.createDialogueTrees();
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.D)){
            //Toggle Menu
            if (SceneManager.GetActiveScene().name == "Menu"){
                SceneManager.LoadScene(prevScene);
            }else{
                prevScene = SceneManager.GetActiveScene().buildIndex;
                prevPos = GameObject.Find("Player").GetComponent<SpriteRenderer>().transform.position;
                currentNPC = -1; //Not interacting with NPC with code D
                SceneManager.LoadScene("Menu");
            }
        }
    }
    // Populate the Equipment List using array data
    private void PopulateEquipmentList()
    {
        var sprites = Resources.Load<Sprite>("Equipment/1"); ;
        for (int i = 0; i < 15; i++)
        {
            sprites = Resources.Load<Sprite>("Equipment/" + (i + 1));
            equipmentList[i].eqImage = sprites;
            equipmentList[i].equipped = false;
            equipmentList[i].owned = false;
            equipmentList[i].Visability = Color.clear;

            equipmentList[i].name = equipmentStrings[i, 0];
            equipmentList[i].description = equipmentStrings[i, 1];
            equipmentList[i].attackBonus = equipmentStats[i, 0];
            equipmentList[i].defenseBonus = equipmentStats[i, 1];
            equipmentList[i].healBonus = equipmentStats[i, 2];
            equipmentList[i].Price = equipmentStats[i, 3];
        }

        //var sprites = Resources.Load<Sprite>("Equipment/1"); ;
        //for (int i = 0; i < 15; i++)
        //{
        //    sprites = Resources.Load<Sprite>("Equipment/" + (i + 1));
        //    equipmentList[i].eqImage = sprites;
        //    equipmentList[i].owned = false;
        //    equipmentList[i].Visability = Color.clear;
        //}
    }

    // Populate the NPC List using array data
    private void PopulateNPCList()
    {
        int i = 0;
        for (; i < 11; i++)
        {
            NPCList[i].name = NPCstringData[i, 0];
            NPCList[i].primaryName = NPCstringData[i, 1];
            NPCList[i].secondaryName = NPCstringData[i, 2];
            //Initialize max and current health as the same value
            NPCList[i].health = NPCintData[i, 0];
            NPCList[i].MAXhealth = NPCintData[i, 0];
            if (i < 4)
            {
                NPCList[i].primaryStat = NPCintData[i, 1];
                NPCList[i].secondaryStat = NPCintData[i, 2];
            }
            else
            {
                NPCList[i].enemyDamage = NPCintData[i, 1];
                NPCList[i].enemyDamageBonus = NPCintData[i, 2];
            }
            NPCList[i].runRange = NPCintData[i, 3];
        }

        NPCList[0].giftItemID = 11;
        NPCList[2].giftItemID = 3;
        NPCList[3].giftItemID = 7;
    }

    // Return the requested equipment data from list
    public static equipmentData getEquipment(int i)
    {
        return equipmentList[i];
    }

    // Set Color Visability of an item
    public static void setEquipmentColor(int i, Color color)
    {
        equipmentList[i].Visability = color;
    }

    // Set item to owned
    public static void setEquipmentOwned(int i)
    {
        equipmentList[i].owned = true;
    }

    // Use to set bounty item not owned after reedeeming money
    public static void setBountyNotOwned(int i)
    {
        equipmentList[i].owned = false;
    }

    // Return which item Ego has equipped in index i
    public static equipmentData getEquipped(int i)
    {
        return equippedItems[i];
    }

    // When an item is equipped place it in the list
    public static void setEquipment(int index, int item)
    {
        equippedItems[index] = equipmentList[item];
    }

    // Return name of NPC in List
    public static string getName(int idx){
        return NPCList[idx].name;
    }

    // Return which dialogue tree is being used
    public static Node[] getDialogueTree(int index){
        return DialogueTrees[index];
    }

    // Update health of Ego
    public static void UpdateHealth(int heal){
        health+=heal;
    }

    // Update Egos Primary item bonuses
    public static void UpdateEgosPrimary(equipmentData equip)
    {
        primaryBonus = equip.attackBonus;
        primarydefenseBonus = equip.defenseBonus;
        egoHeal = equip.healBonus;
    }

    // Update Egos Secondary item Bonuses
    public static void UpdateEgosSecondary(equipmentData equip)
    {
        secondaryBonus = equip.attackBonus;
        secondarydefenseBonus = equip.defenseBonus;
        egoHeal = equip.healBonus; 
    }

    // Update his defense bonus
    public static void UpdateEgosDefense(equipmentData equip)
    {
        defenseBonus = equip.defenseBonus;
    }

    // Return the amount of money Ego has
    public static int getMoney()
    {
        return money;
    }

    // an item has been bought
    public static void reduceMoney(int price)
    {
        money -= price;
    }

    public static void toggleEquipped(int i)
    {
        equipmentList[i].equipped = !equipmentList[i].equipped;
    }

    //
    public static void updateParty(int id){
        }
	public static int getEgoHealth(){
		return health;
	}
	public static int getEgoPrimary(){
		return primaryBonus;
	}
	public static int getEgoSecondary(){
		return secondaryBonus;
	}
	public static int getEgoDefense(){
		return defenseBonus;
	}
	public static int getEgoHeal(){
		return egoHeal;
	}
	public static PartySlot getParty(int index){
		return party [index];
	}
	public static NPCData getEnemy(int index){
		return NPCList [index];
	}
	public static int getEgoMaxAtk(){
		return AttackRangeMAX;
	}
    
    public static void PopulatePotentialNPCPartSlot(){
        party[0].isAssigned=false;
        party[1].isAssigned=false;
        potentialNPC[0].slotID = 0;
        potentialNPC[0].npc = NPCList[0];
        potentialNPC[1].slotID = 0;
        potentialNPC[1].npc = NPCList[1];
        potentialNPC[2].slotID = 0;
        potentialNPC[2].npc = NPCList[2];
        potentialNPC[3].slotID = 0;
        potentialNPC[3].npc = NPCList[3];
        
    }
}
