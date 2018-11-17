using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
/*
	private int playerHp = Player.getHP();
	private int playerAtkPrimary = Player.getAtkPrimary();
	private int playerAtkSecondary = Player.getAtkSecondary();
	private int enemyHP = CurrentEnemy.getHP ();
	private int enemyAtk = CurrentEnemy.getAtk ();
	private int activePlayer;
	private int PlayerCurrentHP = Player.getHP();
	private int PartyOnecurrentHP = PartyOne.getHP();
	private int PartyTwocurrentHP = PartyTwo.getHP();

	public enum battleStates{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		LOSE,
		WIN,
		CALCMDAMAGE,
		RUN
	}
	private battleStates currentState;

	// Use this for initialization
	void Start () {
		currentState = battleStates.START;
	}
	// Update is called once per frame
	void Update () {
		Debug.Log (currentState);
		switch (currentState) {
		case(battleStates.START):
			//SETUP BATTLE FUNCTION
			activePlayer = 1;
			break;

		case(battleStates.PLAYERCHOICE):
			break;

		case(battleStates.ENEMYCHOICE):
			break;

		case(battleStates.LOSE):
			break;

		case(battleStates.WIN):
			break;
		
		case(battleStates.CALCMDAMAGE):
			break;

		case(battleStates.RUN):
			break;
		}

	}

	void OnGUI(){
		if(GUILayout.Button("NEXT STATE")){
			if(PlayerCurrentHP != 0 && enemyHP != 0){
			if (currentState == battleStates.START) {
				currentState = battleStates.PLAYERCHOICE;
			} else if (currentState == battleStates.PLAYERCHOICE) {
				currentState = battleStates.ENEMYCHOICE;
			} else if (currentState == battleStates.ENEMYCHOICE) {
				currentState = battleStates.PLAYERCHOICE;
				}
			} else if(PlayerCurrentHP == 0){
				currentState = battleStates.LOSE;
			}
			else if(enemyHP == 0){
				currentState = battleStates.WIN;
			}
	
		if (GUILayout.Button ("Primary Action")) {
				enemyHP = enemyHP - playerAtkPrimary; 
		}
		if (GUILayout.Button ("Secondary Action")) {
				enemyHP = enemyHP - playerAtkSecondary; 
		}
		if (GUILayout.Button ("Switch")) {
			if(activePlayer == 1){
				activePlayer = 2;
				PlayerCurrentHP = partyOne.getHP ();
				playerAtk = partyOne.getAtk ();
			}
			if(activePlayer == 2){
				activePlayer = 3;
				PlayerCurrentHP = partyTwo.getHP ();
				playerAtk = partyTwo.getAtk ();
			}

		}
		if (GUILayout.Button ("Run")) {
				currentState = battleStates.RUN;
				//SceneManager.LoadScene(int sceneID); Load the Overworld
		}
	}
*/
}