using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public struct NPCData{
    public string name;
	public string primaryName;
	public string secondaryName;
	public int health;
	public int primaryStat;
	public int secondaryStat;
	public int runRange;
	public int enemyMINDamage;
	public int enemyMAXDamage;
}

public struct Node{
    public string option1;
    public string option2;
    public string response;
    public int indexForOption1;
    public int indexForOption2;
}

public class GameInfo : MonoBehaviour
{
    //VeryGlobal
    public static int prevScene = -1;
    public static int currentNPC;
    public static int bountyOwed = 1;
    
    public static NPCData[] NPCList;
    private static string CharacterName = "Ego";
    private static equipment[] equippedItems = new equipment[3];
    private static PartySlot[] party = new PartySlot[2];
    //Ego wil always deal damage from 2 to 17 plue whatever bonus from the equipment
    // Kurt will adjust the MIN and Max values based on Equipment
    // Christian will just generate a number between these two 
    private static int AttackRangeMIN = 2;
    private static int AttackRangeMAX = 17;
    private static int Attack;

    private static int money = 0;
    private static bool isAlive = true;
    private static int health = 100;

    // Name, Move One, Move Two
    /*NPC ID will be array Index 
    (RECRUITABLES) Cynthia = 0, Anker(ShopKeeper) = 1, Edward(Doctor) = 2, Emrick(Farmer) = 3, 
    (SHADOWS)      Berndy(Bunny) = 4, Modir(Mother) = 5, Farenvir(Father) = 6, Ozul(Antagonist) = 7,
    (BOUNTY)       Fox = 8, Rock Creature = 9, Rabbit = 10*/
    private static string[,] NPCstringData = new string[11, 3] { { "Cynthia", "Heal", "Revive" }, { "Anker", "Use Item", "Rage" }, { "Edward", "Heal", "Revive" }, {"Emrik", "Impale", "Kick" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" } };
    // Heath, PrimaryStat, SecondaryStat, RunRange (FOR first 4 (0 to 3 ID indexes) Recruitable NPCs)
    // Heath, Set Attack, additional Attack Range to be added to Attack, RunRange (last 7(6 to 10 ID indexes) enemy NPCs)
    private static int[,] NPCintData = new int[11, 4] { { 25, 50, 0, 1 }, { 150, 150, 35, 10 }, { 75, 25, 30, 6 }, { 35, 50, 20, 8 }, { 65, 15, 5, 5 }, { 100, 35, 10, 10 }, { 120, 25, 7, 7 }, { 250, 75, 10, 100 }, { 25, 5, 2, 3 }, { 50, 10, 10, 5 }, { 25, 3, 2, 1 } };
    public static Sprite[] NPCsprites = new Sprite[11];
    
    //DialogueTrees
    public static Node[][] DialogueTrees = new Node[4][];

    //Cynthia
    public static Node[] Cynthia = new Node[10];
    public static Node[] Emrik = new Node[10];
    public static Node[] Anker = new Node[10];
    public static Node[] Edward = new Node[10];


    

    private void Start()
    {
        
        DontDestroyOnLoad(this.gameObject);

        int i = 0;
        int j = 0;
        for (; i < 11; i++)
        {
            for(j=0;j<4;j++)
            {
                if(j<4){
                    //populate npcstringdata
                }
                //populate npcintdata
            }
        }
        LoadDialogueTrees();
        
    }
    public static string getName(int idx){
        //return NPCList[idx].name;
        return "Cynthia";
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
}
