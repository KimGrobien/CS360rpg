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
    public bool dead;
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
    public Sprite ArmorFullImage;
    public bool equipped;
    public bool owned;
    public Color Visability;
}

// A structure defined to hold data about Ego's party members
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
    //private static string CharacterName = "Ego";
    private static int money = 100;
    public static bool isAlive = true;
    private static int MAXhealth = 100, currentHealth = 50;
    public static PartySlot[] party = new PartySlot[2];
    private static equipmentData[] equippedItems = new equipmentData[3];
    public static int[] equippedIndexes = new int[3];

    //Egos Data for Combat
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

    // For equipment
    public static bool buyingMode = false;
    private static Sprite[] equipmentSprites = new Sprite[15];
    // atk, defense, heal, price (buy or sell for bounty)
    private static int[,] equipmentStats = new int[15, 4] { { 5, 10, 0, 25 }, { 20, 20, 0, 40 }, { 50, 50, 0, 100 }, { 15, 0, 0, 0 }, { 10, 0, 0, 10 }, { 25, 0, 0, 20 }, { 50, 0, 0, 50 }, { 35, 0, 0, 0 }, { 0, 20, 0, 10 }, { 0, 30, 0, 20 }, { 0, 50, 0, 50 }, { 0, 0, 40, 0 }, { 0, 0, 0, 25 }, { 0, 0, 0, 5 }, { 0, 0, 0, 10 } };
    public static string[,] equipmentStrings = new string[15, 2] { { "Wooden Shield", "Low Protection\nLow Attack\nPrice: $25" }, { "Iron Shield", "Medium Protection\nMedium Attack\nPrice: $40" }, { "Spiked Shield", "High Protection\nHigh Attack\nPrice: $100" }, { "Scalpel", "Low Attack\nA Gift" }, { "Gila Dagger", "Low Attack\nPrice: $10" }, { "Sword", "Medium Attack\nPrice: $20" }, { "Fire Staff", "High Attack\nPrice: $50" }, { "Sickle", "Medium Attack\nA Gift" }, { "Leather Set", "Low Protection\nPrice: $10" }, { "Chainmail Set", "Medium Protection\nPrice: $20" }, { "Knight Set", "High Protection\nPrice: $50" }, { "Heal Spell", "Low Ability to Heal\nA Gift" }, { "Rock Hat", "Redeemable Bounty\nReward: $25" }, { "Rabbit Tail", "Redeemable Bounty\nReward: $5" }, { "Fox Fur", "Redeemable Bounty\nReward: $10" }, };
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
    private static int[,] NPCintData = new int[11, 4] { { 25, 50, 0, 1 }, { 185, 95, 35, 10 }, { 75, 25, 30, 6 }, { 35, 50, 20, 8 }, { 65, 15, 5, 5 }, { 100, 35, 10, 10 }, { 120, 25, 7, 7 }, { 250, 75, 10, 100 }, { 25, 5, 2, 3 }, { 50, 10, 15, 5 }, { 25, 3, 1, 1 } };
    public static Sprite[] NPCsprites = new Sprite[11];

    //PartySlot objects for dialogue use
    public static PartySlot[] potentialNPC = new PartySlot[4];

    //DialogueTrees
    public static Node[][] DialogueTrees = new Node[11][];
    public static int[] encountered = new int[11];
    public static bool[] recruitable = {false,false,false,false,false,false,false,false,false,false,false};

    //death conditions
    public static bool[] hasBountyInInventory = {false,false,false};
    
    public static bool end;//true for win game, false for die

    // Used to populate all the initial data of the game
    private void Start()
    {

        DontDestroyOnLoad(this.gameObject);
        //If you are initializing Arrays or data in this start function put it in this if statment so that it isnt reinizalized everytime you reload the start screen
        if (!Gameloaded)
        {
            Gameloaded = true;
            GameInfo.prevPos.x = -999; //-999 special value to signify ignore prevPos
            PopulateNPCList();
            LoadDialogue.createDialogueTrees();
            PopulateEquipmentList();
            PopulatePotentialNPCPartSlot();
            PopulateParty();
        }
        
    }

    //Check for Main Menu Cue key
    private void Update(){
        if (Input.GetKeyDown(KeyCode.D)){
            //Toggle Menu
            if (SceneManager.GetActiveScene().name == "Menu"){
                SceneManager.LoadScene(prevScene);
            }else if (SceneManager.GetActiveScene().name != "Combat") {//Can't open menu during combat
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
        var sprites = Resources.Load<Sprite>("Equipment/1");
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
         
        //TODO: Remove owned = true; for testing bountyAnims
        equipmentList[12].owned = false;
        equipmentList[13].owned = false;    
        equipmentList[14].owned = false;

        equipmentList[8].ArmorFullImage = Resources.Load<Sprite>("EgoArmor/0");
        equipmentList[9].ArmorFullImage = Resources.Load<Sprite>("EgoArmor/1");
        equipmentList[10].ArmorFullImage = Resources.Load<Sprite>("EgoArmor/2");

        equippedIndexes[0] = -1;
        equippedIndexes[1] = -1;
        equippedIndexes[2] = -1;
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
            NPCList[i].dead = false;
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

    // Use to set bounty item not owned after reedeeming money
    public static void setNotDead(int i)
    {
        NPCList[i].dead = false;
    }

    // Return owned status of item
    public static bool getOwnedStatus(int i)
    {
        return equipmentList[i].owned;
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

    // bounty has been redeemed
    public static void AddMoney(int price)
    {
        money += price;
    }

    // bounty has been redeemed
    public static int GetPrice(int index)
    {
        return equipmentList[index].Price;
    }

    // Toggle equipment owned or not
    public static void ToggleEquipped(int i)
    {
        if (i > -1)
        {
            equipmentList[i].equipped = !equipmentList[i].equipped;
        }
    }

    //Return index of item that is equipped at idx index
    public static int ReturnEquippedItem(int idx)
    {
        return equippedIndexes[idx];
    }

    //
    //public static void updateParty(int id){
    //}

    // Return Ego's Max health
	public static int getEgoMaxHealth(){
		return MAXhealth;
	}

    // Get Ego's current health
    public static int getEgoCurrentHealth(){
		return currentHealth;
	}

    // Update the health of the designated NPC
    public static void updateCurrentHealth(int damage){
        if (currentHealth - damage < 0)
        {
            currentHealth = 0;
        }
        else if (currentHealth - damage > MAXhealth)
        {
            currentHealth = MAXhealth;
        }
        else
        {
            currentHealth -= damage;
        }
    }

    // Get ego's primary attack bonus
	public static int getEgoPrimary(){
		return primaryBonus;
	}

    // Get Ego's Secondary attack bonus
	public static int getEgoSecondary(){
		return secondaryBonus;
	}

    // Get Ego's defense bonus, summation of all three
	public static int getEgoDefense(){
		return defenseBonus + primarydefenseBonus + secondarydefenseBonus;
	}

    // return Ego's heal ability
	public static int getEgoHeal(){
		return egoHeal;
	}

    // Return member of party
	public static PartySlot getParty(int index){
		return party [index];
	}

    // Return name of NPC's primary action
    public static string getPrimaryActionName(int idx)
    {
        if(NPCList[idx].primaryName != ""){
            return NPCList[idx].primaryName;
        }else{
            return "Primary";
        }
    }

    // Return name of NPC's secondary action
    public static string getSecondaryActionName(int idx)
    {
        if(NPCList[idx].secondaryName != ""){
            return NPCList[idx].secondaryName;
        }else{
            return "Secondary";
        }
    }
    
    // Return NPC data of the Enemy you are interacting with
    public static NPCData getEnemy(int index){
		return NPCList [index];
	}

    // Set partyMember to dead
    public static void setPartyMemberDead(int index)
    {
        party[index].npc.dead = true;
    }

    // Set Party member to alive
    public static void setPartyMemberAlive(int index)
    {
        party[index].npc.dead = false;
    }

    // Set NPC to alive
    public static void setNPCAlive(int index)
    {
        NPCList[index].dead = false;
    }

    // set NPC to dead
    public static void setDead(int index)
    {
        NPCList[index].dead = true;
    }

    // Check if NPC is dead
    public static bool CheckIfDead(int index)
    {
        return NPCList[index].dead;
    }

    // Populate
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

    // Populate party with dummy IDs
    public static void PopulateParty(){
        party[0].slotID = -1;
        party[1].slotID = -1;
    }

    // Update an NPC's heath, may be healing (if damage = negative value)
    public static void updateNPCHealth(int idx, int damage){
        if (NPCList[idx].health - damage < 0)
        {
            NPCList[idx].health = 0;
        }
        else if (NPCList[idx].health - damage > NPCList[idx].MAXhealth)
        {
            NPCList[idx].health = NPCList[idx].MAXhealth;
        }
        else
        {
            NPCList[idx].health -= damage;
        }
    }

    // Return health of desired NPC
    public static int getNPCHealth(int idx){
        return NPCList[idx].health;
    }

    // Return the MAX health of a desired NPC
    public static int getNPCMAXHealth(int idx)
    {
        return NPCList[idx].MAXhealth;
    }
    
    // Return Primary Bonus for Ego
    public static int getPrimaryAttackBonus(){
        return primaryBonus;
    }

    // Return attack Max for NPC
    public static int getNPCPrimaryAttack(int idx){
        if (idx < 4) {//Recruitable, so return max val
            return NPCList[idx].primaryStat;
        }else{//Enemy, return random num within attack range
             System.Random rnd = new System.Random();
             return rnd.Next(NPCList[idx].enemyDamageBonus, NPCList[idx].enemyDamage + NPCList[idx].enemyDamageBonus);
        }
        
    }

    //Return second attack number for NPC
    public static int getNPCSecondaryAttack(int idx)
    {
        return NPCList[idx].secondaryStat;
    }

}
