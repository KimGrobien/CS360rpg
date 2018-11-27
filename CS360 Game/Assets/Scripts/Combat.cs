
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Comments below should describe the code decently
//CAPS = code to be added
//standard case = description of statement below

public class Combat : MonoBehaviour
{

	private int playerHp;
	private int playerAtkPrimary;
	private int playerAtkSecondary;
	private int playerMaxAtk;
	private int enemyHP;
	private int enemyAtk;
	private int activePlayer;
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

	public enum battleStates
	{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		LOSE,
		WIN,
		RUN
	}

	private battleStates currentState;

	// Use this for initialization
	void Start ()
	{
		playerHp = GameInfo.getEgoHealth();
		PlayerCurrentHP = playerHp;
		playerMaxAtk = GameInfo.getEgoMaxAtk();
		playerAtkPrimary = GameInfo.getEgoPrimary();
		playerAtkSecondary = GameInfo.getEgoSecondary();
		playerMinAtkPrimary = 2 + playerAtkPrimary;
		playerMinAtkSecondary = 2 + playerAtkSecondary;
		enemyHP = GameInfo.getEnemy(GameInfo.currentNPC).health;
		enemyAtk = GameInfo.getEnemy(GameInfo.currentNPC).enemyDamage;
		PartyOnecurrentHP = GameInfo.getParty(0).npc.health;
		PartyTwocurrentHP = GameInfo.getParty(1).npc.health;
		PartyOneAtkPrimary = GameInfo.getParty (0).npc.primaryStat;
		PartyTwoAtkPrimary = GameInfo.getParty (1).npc.primaryStat;
		PartyOneAtkSecondary = GameInfo.getParty (0).npc.secondaryStat;
		PartyTwoAtkSecondary = GameInfo.getParty (1).npc.secondaryStat;
		damagehold = 0;
		healhold = 0;
		currentState = battleStates.START;

	}
	// Update is called once per frame
	void Update ()
	{
		//logs which state is active for testing
		Debug.Log (currentState);
		//switch case to set state to specific combat state pending on circumstances of combat and break's infinitely until
		//player procs next state
		switch (currentState) {
		//START initializes the combat sequence
		case (battleStates.START):
               //SETUP BATTLE FUNCTION
			activePlayer = 0;
			break;
		//PLAYERCHOICE is the player's turn
		case (battleStates.PLAYERCHOICE):
			break;
		//ENEMYCHOICE is the enemy's turn
		case (battleStates.ENEMYCHOICE):
			break;
		//LOSE ends the game
		case (battleStates.LOSE):
			break;
		//WIN ends combat and returns scene to overworld
		case (battleStates.WIN):
			break;
		//RUN returns to overworld but does not remove the enemy from the overworld
		case (battleStates.RUN):
			break;
		}

	}

	void OnGUI ()
	{
		
		//NEXT STATE cycles the states of combat between player's turn and enemies turn. Basically a confirm button. 
		if (GUILayout.Button ("Confirm Choice")) {
			//if player's and enemy's are not 0
			if (PlayerCurrentHP != 0 && enemyHP != 0) {
				//if combat just started
				if (currentState == battleStates.START) {
					//begin player's turn
					currentState = battleStates.PLAYERCHOICE;
				}
                //if player's turn
                else if (currentState == battleStates.PLAYERCHOICE) {
					//begin enemy's turn
					if (start == false) {
						start = true;
					} else {
						if (damagehold > 0) {
							PlayerCurrentHP -= damagehold;
							damagehold = 0;
							healhold = 0;
							currentState = battleStates.ENEMYCHOICE;
						} else {
							if (healhold > 0) {
								PlayerCurrentHP += healhold;
								damagehold = 0;
								healhold = 0;
								currentState = battleStates.ENEMYCHOICE;
							}
						}
					}
				}
                //if enemy's turn
                else if (currentState == battleStates.ENEMYCHOICE) {
					//begin player's turn
					playerHp -= damagehold;
					damagehold = 0;
					healhold = 0;
					currentState = battleStates.PLAYERCHOICE;
				}
			}
            //if player's hp is 0
			else if (PlayerCurrentHP == 0){

				//Lose game and load title screen
				currentState = battleStates.LOSE;
				SceneManager.LoadScene("Title Screen", LoadSceneMode.Additive);
			}
            //if enemy's hp is 0
            else if (enemyHP == 0) {
				//win fight and load back into overworld
				currentState = battleStates.WIN;
				SceneManager.LoadScene (GameInfo.prevScene);
			}
		}
		//primary attack/action
		if (GUILayout.Button ("Primary Action")) {
			//IF STATEMENT TO CHECK IF CYNTHIA OR NOT
			//IF CYNTHIA, CYCLE PARTY FOR LOWEST HP PARTY MEMBER AND HEAL
			//NEXT STATE

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
				damagehold = damageCalc.Next (playerMinAtkPrimary, playerAtkPrimary);
			}

		}
		if (GUILayout.Button ("Secondary Action")) {
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
				damagehold = damageCalc.Next (playerMinAtkSecondary, playerAtkSecondary);
			}

		}
		//switch action does not take a turn or change the state in order to take effect. Swaps active character for next
		//party member
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
			if (activePlayer == 3) {
				//switch active player to ego
				activePlayer = 1;
				//STATEMENT CHANGING SPRITE
			}

		}
		//Run leaves combat and returns to overworld with the npc remaining in the overworld
		if (GUILayout.Button ("Run")) {
			currentState = battleStates.RUN;
			SceneManager.LoadScene (GameInfo.prevScene);
		}
	}
}
