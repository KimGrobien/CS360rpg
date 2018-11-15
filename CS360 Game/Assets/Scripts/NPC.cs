using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	private int name;
	string primaryName;
	string secondaryName;
	int primaryAttackBNS;
	int primaryDefenseBNS;
	int primaryHealthBNS;
	int secondaryAttackBNS;
	int secondaryDefenseBNS;
	int secondaryHealthBNS;

	public static string getNPCPrimaryName(int NPCID){
		return GameInformation.NPCList[NPCID].primaryName;
	}

}
