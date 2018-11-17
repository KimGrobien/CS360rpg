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
    public static int prevScene;
    public static int currentNPC;
    
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
    private static string[,] NPCstringData = new string[11, 3] { { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" } };
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
        Cynthia[0].response = "Pick an option";
        Cynthia[0].option1 = "This is a new Option, do you want to pick it?";
        Cynthia[0].option2 = "Way to go!";
        Cynthia[0].indexForOption1 = 1;
        Cynthia[0].indexForOption2 = 2;

        Cynthia[1].response = "Woah OPTIONS?! well pick 1 or 2.";
        Cynthia[1].option1 = "this one";
        Cynthia[1].option2 = "this two";
        Cynthia[1].indexForOption1 = 3;
        Cynthia[1].indexForOption2 = 4;

        Cynthia[2].response = "You picked the new option, huh?";
        Cynthia[2].option1 = "Now what?";
        Cynthia[2].option2 = "Yes, yes I did";
        Cynthia[2].indexForOption1 = 5;
        Cynthia[2].indexForOption2 = 6;
           
        Cynthia[3].response = "So you picked this one";
        Cynthia[3].option1 = "yes I did brother bear";
        Cynthia[3].option2 = "I didn't mean to";
        Cynthia[3].indexForOption1 = 7;
        Cynthia[3].indexForOption2 = 8;

        DialogueTrees[0] = Cynthia;
        Debug.Log("FINISHED SETTING");
        Debug.Log(DialogueTrees[0]);
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
    }
    public static string getName(int idx){
        //return NPCList[idx].name;
        return "Cynthia";
    }
    public static Node[] getDialogueTree(int index){
        Debug.Log(index);
        return DialogueTrees[index];
    }
    //return DialogueTrees[idx]
}
