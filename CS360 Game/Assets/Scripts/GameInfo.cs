using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    //VeryGlobal
    public int prevScene;
    public int currentNPC;

    private NPC[] NPCList;

    private string CharacterName = "Ego";
    private equipment[] equippedItems = new equipment[3];
    private PartySlot[] party = new PartySlot[2];
    //Ego wil always deal damage from 2 to 17 plue whatever bonus from the equipment
    // Kurt will adjust the MIN and Max values based on Equipment
    // Christian will just generate a number between these two 
    private int AttackRangeMIN = 2;
    private int AttackRangeMAX = 17;
    private int Attack;

    private int money = 0;
    private bool isAlive = true;
    private int health = 100;

    // Name, Move One, Move Two
    private string[,] NPCstringData = new string[11, 3] { { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" }, { "Cynthia", "Heal", "Revive" } };
    // Heath, PrimaryStat, SecondaryStat, RunRange (FOR first 4 (0 to 3 ID indexes) Recruitable NPCs)
    // Heath, Set Attack, additional Attack Range to be added to Attack, RunRange (last 7(6 to 10 ID indexes) enemy NPCs)
    private int[,] NPCintData = new int[11, 4] { { 25, 50, 0, 1 }, { 150, 150, 35, 10 }, { 75, 25, 30, 6 }, { 35, 50, 20, 8 }, { 65, 15, 5, 5 }, { 100, 35, 10, 10 }, { 120, 25, 7, 7 }, { 250, 75, 10, 100 }, { 25, 5, 2, 3 }, { 50, 10, 10, 5 }, { 25, 3, 2, 1 } };
    public Sprite[] NPCsprites = new Sprite[11];

    // When created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
