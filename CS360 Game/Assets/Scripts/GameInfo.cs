using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

// Structure layout for NPC Data, which will be used to define each NPC 
public struct NPCData{
    public string name;
	public string primaryName;
	public string secondaryName;
	public int health;
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
    public bool owned;
    public Color Visability;
}

// Start of Main Class which houses all of the games global information
public class GameInfo : MonoBehaviour
{
    //VeryGlobal Data

    // Ego's Data
    private static string CharacterName = "Ego";
    private static int money = 100;
    private static bool isAlive = true;
    private static int health = 100;
    private static PartySlot[] party = new PartySlot[2];
    private static equipment[] equippedItems = new equipment[3];

    // Data for overworld Navigation
    public static int prevScene = -1;
    public static Vector3 prevPos;
    public static int currentNPC = -1;
    public static int bountyOwed = 1;

    //Data for Combat
    //Ego wil always deal damage from 2 to 17 plus whatever bonus from the equipment
    private static int AttackRangeMIN = 2;
    private static int AttackRangeMAX = 17;
    private static int primaryBonus = 0;
    private static int secondaryBonus = 0;
    private static int primarydefenseBonus = 0;
    private static int secondarydefenseBonus = 0;
    private static int defenseBonus = 0;
    private static int egoHeal = 0;

    // For equipment
    public static bool buyingMode = false;
    // atk, defense, heal, price (buy or sell for bounty)
    private static Sprite[] equipmentSprites= new Sprite[15];
    private static int[,] equipmentStats = new int[15, 4] { { 5, 10, 0, 25 }, { 20, 20, 0, 40 }, { 50, 50, 0, 100 }, { 15, 0, 0, 0 }, { 10, 0, 0, 10 }, { 25, 0, 0, 20 }, { 50, 0, 0, 50 }, { 35, 0, 0, 0 }, { 0, 20, 0, 10 }, { 0, 30, 0, 20 }, { 0, 50, 0, 50 }, { 15, 0, 0, 0 }, { 0, 0, 10, 0 }, { 0, 0, 0, 5 }, { 0, 0, 0, 7 } };
    private static string[,] equipmentStrings = new string[15, 2] { { "Wooden Shield", "Low Protection\nLow Attack\nPrice: $25" }, { "Iron Shield", "Medium Protection\nMedium Attack\nPrice: $40" }, { "Spiked Shield", "High Protection\nHigh Attack\nPrice: $100" }, { "Scalpel", "Low Attack\nA Gift" }, { "Gila Dagger", "Low Attack\nPrice: $10" }, { "Sword", "Medium Attack\nPrice: $20" }, { "Fire Staff", "High Attack\nPrice: $50" }, { "Sickle", "Medium Attack\nA Gift" }, { "Leather Set", "Low Protection\nPrice: $10" }, { "Chainmail Set", "Medium Protection\nPrice: $20" }, { "Knight Set", "High Protection\nPrice: $50" }, { "Heal Spell", "Low Ability to Heal\nA Gift" }, { "Rock Hat", "Redeemable Bounty\nReward: $10" }, { "Rabbit Tail", "Redeemable Bounty\nReward: $5" }, { "Fox Fur", "Redeemable Bounty\nReward: $7" }, };
    private static equipmentData[] equipmentList = new equipmentData[15];

    //Data for NPC interaction/Combat
    public static NPCData[] NPCList = new NPCData[11];
    //NPC Data
        // Name, Move One, Move Two
        /*NPC ID will be array Index 
        (RECRUITABLES) Cynthia = 0, Anker(ShopKeeper) = 1, Edward(Doctor) = 2, Emrick(Farmer) = 3, 
        (SHADOWS)      Berndy(Bunny) = 4, Modir(Mother) = 5, Farenvir(Father) = 6, Ozul(Antagonist) = 7,
        (BOUNTY)       Fox = 8, Rock Creature = 9, Rabbit = 10*/
    private static string[,] NPCstringData = new string[11, 3] { { "Cynthia", "Heal", "Revive" }, { "Anker", "Use Item", "Rage" }, { "Edward", "Heal", "Revive" }, {"Emrik", "Impale", "Kick" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" } };
    /* Heath, PrimaryStat, SecondaryStat, RunRange (FOR first 4 (0 to 3 ID indexes) Recruitable NPCs)
        Heath, Set Attack, additional Attack Range to be added to Attack, RunRange (last 7(4 to 10 ID indexes) enemy NPCs)*/
    private static int[,] NPCintData = new int[11, 4] { { 25, 50, 0, 1 }, { 150, 150, 35, 10 }, { 75, 25, 30, 6 }, { 35, 50, 20, 8 }, { 65, 15, 5, 5 }, { 100, 35, 10, 10 }, { 120, 25, 7, 7 }, { 250, 75, 10, 100 }, { 25, 5, 2, 3 }, { 50, 10, 10, 5 }, { 25, 3, 1, 1 } };
    public static Sprite[] NPCsprites = new Sprite[11];
    
    //DialogueTrees
    public static Node[][] DialogueTrees = new Node[4][];

    //Cynthia
    public static Node[] Cynthia = new Node[10];
    public static Node[] Emrik = new Node[10];
    public static Node[] Anker = new Node[10];
    public static Node[] Edward = new Node[10];

    // Used to populate all the initial data of the game
    private void Start()
    {
        
        DontDestroyOnLoad(this.gameObject);
        PopulateNPCList();
        LoadDialogueTrees();
        PopulateEquipmentList();
    }

    // Populate the Equipment List using array data
    private void PopulateEquipmentList()
    {
        for (int i = 0; i < 15; i++)
        {
            equipmentList[i].name = equipmentStrings[i, 0];
            equipmentList[i].description = equipmentStrings[i, 1];

            equipmentList[i].attackBonus = equipmentStats[i, 0];
            equipmentList[i].defenseBonus = equipmentStats[i, 1];
            equipmentList[i].healBonus = equipmentStats[i, 2];
            equipmentList[i].Price = equipmentStats[i, 3];
        }

        var sprites = Resources.Load<Sprite>("Equipment/1"); ;
        for (int i = 0; i < 15; i++)
        {
            sprites = Resources.Load<Sprite>("Equipment/" + (i+1));
            equipmentList[i].eqImage = sprites;
            equipmentList[i].owned = false;
            equipmentList[i].Visability = Color.clear;
        }
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
            NPCList[i].health = NPCintData[i, 0];
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
    }

    // Return the requested equipment data from list
    public static equipmentData getEquipment(int i)
    {
        return equipmentList[i];
    }

    public static void setEquipmentColor(int i,Color color)
    {
        equipmentList[i].Visability = color;
    }

    public static void setEquipmentOwned(int i)
    {
        equipmentList[i].owned = true;
    }

    public static string getName(int idx){
        return NPCList[idx].name;
        //return "Cynthia";
    }

    public static Node[] getDialogueTree(int index){
        return DialogueTrees[index];
    }

    public static Sprite GetImageOfCurrentNPC(int index){
        //string characterName = getName(index);
        //return Resources.Load<Sprite>("Art/DialogueImages/"+characterName);
        return Resources.Load<Sprite>("Art/DialogueImages/Ego");
    }    

    private static void LoadDialogueTrees(){
        //Cynthia
        //currentIndex will lead to currentIndex+3 as option1 and currentIndex+4 as option2
       //firstEncounter
        Cynthia[0].response = "You approach a small woman standing next to a garden." 
        +" Her hands are covered in dirt and her forehead in sweat. You can tell she's" 
        +" been working outside all day. She notices you walking up to her. She gives"
        +" you a kind smile. \n\nShe says, \"You\'re new, aren\'t you? I haven\'t seen you around here at least. Is there something you need traveler?\"";
        Cynthia[0].option1 = "I'm hurt can you help me?";
        Cynthia[0].option2 = "Where is the nearest town?";
        Cynthia[0].indexForOption1 = 1;
        Cynthia[0].indexForOption2 = 2;
        //Response to index 0 option1
        Cynthia[1].response = "She looks concerned, but hesitant to help."
        +" You think maybe she's just being defensive, a good trait to have"
        +" in this world, but you are really hurt."
        +"\n\nShe says, \"What happened? Were you traveling alone?\"";
        Cynthia[1].option1 = "I don't remember what happened."
        +" I woke up next to a dismembered horse and destroyed carraige."
        +" I think someone robbed me. If you can\'t help me. "
        +"Do you know someone who can?";
        Cynthia[1].option2 = "this two";
        Cynthia[1].indexForOption1 = 3;
        Cynthia[1].indexForOption2 = 4;
        //response to index 0 option2
        Cynthia[2].response = "She says, \"You can find a small village just up the road."
        +"Though they may not be much help to you.\"";
        Cynthia[2].option1 = "Why is that?";
        Cynthia[2].option2 = "As long as they have a doctor then I don't care!";
        Cynthia[2].indexForOption1 = 5;
        Cynthia[2].indexForOption2 = 6;
        //response to index 1 option1
        Cynthia[3].response = "Your story seems to have struck a chord with her."
        +" Her guarded dimeanor seems to soften. "
        +"She says, \"I think I can help. Hold on.\"\nShe raises her hands in your direction. "
        +"A bright light emits from her palms. You feel a warmth in your chest and start feeling a little better.";
        Cynthia[3].option1 = "That was amazing! Thank you so much for helping me. "
        +"I don't know a lot of strangers that would be willing to help.";
        Cynthia[3].option2 = "";
        Cynthia[3].indexForOption1 = 7;
        Cynthia[3].indexForOption2 = 8;

        DialogueTrees[0] = Cynthia;
    }

    public static void UpdateHealth(int heal){
        health+=heal;
    }

    public static void UpdateEgosPrimary(equipmentData equip)
    {
        primaryBonus = equip.attackBonus;
        primarydefenseBonus = equip.defenseBonus;
        egoHeal = equip.healBonus;
    }

    public static void UpdateEgosSecondary(equipmentData equip)
    {
        secondaryBonus = equip.attackBonus;
        secondarydefenseBonus = equip.defenseBonus;
        egoHeal = equip.healBonus; 
    }

    public static void UpdateEgosDefense(equipmentData equip)
    {
        defenseBonus = equip.defenseBonus;
    }

    public static int getMoney()
    {
        return money;
    }

    public static void reduceMoney(int price)
    {
        money -= price;
    }
}
