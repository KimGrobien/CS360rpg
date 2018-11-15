using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : MonoBehaviour {

	//veryglobal
	public static NPC[] NPCList;

	public static string CharacterName = "Ego";
	public static equipment[] equippedItems = new equipment[3];
	public static PartySlot[] party = new PartySlot[2];
	public static int attack;
	public static int defense;
	public static int money;
	public static bool isAlive;
	public static int health;


}
