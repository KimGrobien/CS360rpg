
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//Comments below should describe the code decently
//CAPS = code to be added
//standard case = description of statement below

public class Combat : MonoBehaviour
{
	Canvas buttons;
	Button primaryChoice;
	Button secondaryChoice;
	Button switchMember;
	Button run;
	private int playerHp;
	private int playerAtkPrimary;
	private int playerAtkSecondary;
	private int playerMaxAtkPrimary;
	private int playerMaxAtkSecondary;
	private int enemyHP;
	private int enemyMaxHP;
	private int enemyAtk;
	private int activePlayer=2;
	private int PlayerCurrentHP;
	private int PartyOnecurrentHP;
	private int PartyTwocurrentHP;
	private int PartyOneAtkPrimary;
	private int PartyTwoAtkSecondary;
	private int PartyOneAtkSecondary;
	private int PartyTwoAtkPrimary;
	private int playerMinAtkPrimary;
	private int playerMinAtkSecondary;
	private System.Random damageCalc = new System.Random();
	private bool start = false;
	private int damagehold;
	private int healhold;
	public TextMeshPro updaterText;
	private string text;
	private string hpTextPlayer;
	private string hpTextEnemy;

	//Who we're fighting
	private int enemyID = GameInfo.currentNPC;


	

	// Use this for initialization
	void Start()
	{
		buttons = GameObject.Find("Buttons").GetComponent<Canvas>();
		primaryChoice = buttons.transform.Find("Primary").GetComponent<Button>();
		secondaryChoice = buttons.transform.Find("Secondary").GetComponent<Button>();
		switchMember = buttons.transform.Find("Switch").GetComponent<Button>();
		run = buttons.transform.Find("Run").GetComponent<Button>();
		primaryChoice.onClick.AddListener(PrimaryAction);
		secondaryChoice.onClick.AddListener(SecondaryAction);
		switchMember.onClick.AddListener(SwitchPartyMember);
		run.onClick.AddListener(RunFromCombat);

		GameObject.Find ("Textupdater").GetComponent<TextMeshProUGUI> ().text = "Press the Confirm Button to Begin Combat";
		GameObject.Find ("Enemyname").GetComponent<TextMeshProUGUI> ().text = GameInfo.getEnemy(GameInfo.currentNPC).name;
		//updaterText = FindObjectOfType<TextMeshPro> ();
		//updaterText = GetComponent<TextMeshPro> ();
		//updaterText = gameObject.AddComponent<TextMeshPro>();
		PlayerCurrentHP = playerHp;
		playerAtkPrimary = GameInfo.getEgoPrimary();
		playerAtkSecondary = GameInfo.getEgoSecondary();
		playerMinAtkPrimary = 2 + playerAtkPrimary;
		playerMinAtkSecondary = 2 + playerAtkSecondary;
		playerMaxAtkPrimary = 17 + playerAtkPrimary;
		playerMaxAtkSecondary = 17 + playerAtkSecondary;
		enemyHP = GameInfo.getEnemy(GameInfo.currentNPC).health;
		enemyMaxHP = enemyHP;
		enemyAtk = GameInfo.getEnemy(GameInfo.currentNPC).enemyDamage;
		PartyOnecurrentHP = GameInfo.getParty(0).npc.health;
		PartyTwocurrentHP = GameInfo.getParty(1).npc.health;
		PartyOneAtkPrimary = GameInfo.getParty (0).npc.primaryStat;
		PartyTwoAtkPrimary = GameInfo.getParty (1).npc.primaryStat;
		PartyOneAtkSecondary = GameInfo.getParty (0).npc.secondaryStat;
		PartyTwoAtkSecondary = GameInfo.getParty (1).npc.secondaryStat;
		damagehold = 0;
		healhold = 0;


		UpdateEnemyHealthToScreen(GameInfo.getEnemy(enemyID).health);
		UpdateCurrentNPCHealthToScreen(GameInfo.getEgoCurrentHealth());

		GameObject.Find("Enemy").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Combat/" + GameInfo.getName(GameInfo.currentNPC));
		GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Combat/Ego");
		GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Combat/ComScenes/" + GameInfo.getName(GameInfo.currentNPC));
		//updaterText.text = "Press the Confirm Button to Begin Combat";
		

	}
	// Update is called once per frame
	
	void UpdateEnemyHealthToScreen(int newHealth){		
		hpTextEnemy = "HP:" +newHealth+"/" + GameInfo.getEnemy(enemyID).MAXhealth;
		GameObject.Find ("EnemyHP").GetComponent<TextMeshProUGUI> ().text = hpTextEnemy;
	}
	void UpdateCurrentNPCHealthToScreen(int newHealth){
		if(activePlayer<2){	
		hpTextPlayer = "HP:" +newHealth+"/" + GameInfo.getParty(activePlayer).npc.MAXhealth;
		GameObject.Find ("PlayerHP").GetComponent<TextMeshProUGUI> ().text = hpTextPlayer;
		}
		else{
			
		hpTextPlayer = "HP:" +newHealth+"/" + GameInfo.getEgoMaxHealth();
		GameObject.Find ("EnemyHP").GetComponent<TextMeshProUGUI> ().text = hpTextPlayer;
		}
	}

	void PrimaryAction(){
		Debug.Log("PRIMARY");
		/*
		REMOVE BUTTONS
		1. Calculate Damage to Enemy (Different Case for Heal ID's 0 and 2)
		2. Wait. Update Text
		3. Calculate Damage to be Received
		4. Wait. UpdateText
		BRING BACK BUTTONS
		 */
		if (activePlayer > 0) {
				if (GameInfo.getParty (activePlayer - 1).slotID != 0 && GameInfo.getParty (activePlayer - 1).slotID != 2) {
					damagehold = damageCalc.Next (playerMinAtkPrimary, playerAtkPrimary);
				} else if (GameInfo.getParty (activePlayer - 1).slotID == 0 || GameInfo.getParty (activePlayer - 1).slotID == 2) {
					if (playerHp - PlayerCurrentHP >= 50) {
						healhold = 50;
					} else {
						healhold = playerHp;
					}
				}
			} else if(activePlayer == 0){
				damagehold = damageCalc.Next (playerMinAtkPrimary, playerMaxAtkPrimary);
			}

	}
	void SecondaryAction(){
		Debug.Log("SECONDARY");
		/*
		REMOVE BUTTONS
		1. Calculate Damage to Enemy (Different Case for Revive ID 0)
		2. Wait. Update Text
		3. Calculate Damage to be Received
		4. Wait. UpdateText
		BRING BACK BUTTONS
		 */
		//IF STATEMENT TO CHECK IF CYNTHIA OR DOC OR NOT
			//IF CYNTHIA OR DOC, CYCLE PARTY FOR LOWEST HP PARTY MEMBER AND HEAL
			//NEXT STATE
			if (GameInfo.getParty (activePlayer - 1).slotID == 0) {
				if (PartyOnecurrentHP < 0) {
					healhold = 1;
				} else if (PartyTwocurrentHP < 0) {
					PartyTwocurrentHP = 1;
				}
			} else {
				damagehold = damageCalc.Next (playerMinAtkSecondary, playerMaxAtkSecondary);
			}

	}
	void SwitchPartyMember(){
		Debug.Log("SWITCH");
		if (GUILayout.Button ("Switch")) {
			//active player is ego if activePlayer = 1
			if (activePlayer == 0) {
				//switch to next party member
				activePlayer = 1;
				//STATEMENT CHANGING CHARACTER SPRITE
			}
			//second party member is 2
			if (activePlayer == 1) {
				//switch active player to next
				activePlayer = 2;
				//STATEMENT CHANGING SPRITE
			}
			//third party member is 3
			if (activePlayer == 2) {
				//switch active player to ego
				activePlayer = 0;
				//STATEMENT CHANGING SPRITE
			}

		}

	}
	void ChangeNPCImage(){

	}
	void RunFromCombat(){
			Debug.Log("RUN");
			currentState = battleStates.RUN;
			SceneManager.LoadScene (GameInfo.prevScene);
	}

	void RemoveButtonsFromScreen(){

	}

	void EnemyChoice(){

	}

	void EndGame(){

	}


	// IEnumerator Damage(){
	// 	/* 
	// 	1. Tell the player the damage to which enemy
	// 	2. Update the text
	// 	3. Tell the player the damage to be received
	// 	4. Update that text
	// 	5. Add Buttons to screen
	// 	*/
	// }

	// IEnumerator Switch(){

	// }


}
