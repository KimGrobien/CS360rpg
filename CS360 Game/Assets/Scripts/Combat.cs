
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

//Comments below should describe the code decently
//CAPS = code to be added
//standard case = description of statement below

public class Combat : MonoBehaviour
{
	Canvas buttons, statusCanvas;
	Button primaryChoice;
	Button secondaryChoice;
	Button partyMemberChoice;
	Button run;
	TextMeshProUGUI status, currentNPCName, currentNPCHealth, enemyName, enemyHealth;
	private string hpTextPlayer;
	private string hpTextEnemy;
    private int activePlayer = 2;

    //Who we're fighting
    private int enemyID = GameInfo.currentNPC;

    //Id's of party members
    int partyMember1 = GameInfo.party[0].slotID;
    int partyMember2 = GameInfo.party[1].slotID;

    // Use this for initialization
    void Start()
	{
        // Find Main gameobjects
		buttons = GameObject.Find("Buttons").GetComponent<Canvas>();
		statusCanvas = GameObject.Find("StatusCanvas").GetComponent<Canvas>();

        // Find Combat Buttons
		primaryChoice = buttons.transform.Find("Primary").GetComponent<Button>();
		secondaryChoice = buttons.transform.Find("Secondary").GetComponent<Button>();
		partyMemberChoice = buttons.transform.Find("PartyMember").GetComponent<Button>();
		run = buttons.transform.Find("Run").GetComponent<Button>();

        // Set listeners to each button
		primaryChoice.onClick.AddListener(PrimaryAction);
		secondaryChoice.onClick.AddListener(SecondaryAction);
		partyMemberChoice.onClick.AddListener(SwitchPartyMember);
		run.onClick.AddListener(RunFromCombat);

        // Find text fields
		status = statusCanvas.transform.Find("Status").GetComponent<TextMeshProUGUI>();
		currentNPCHealth = statusCanvas.transform.Find("CurrentNPCHealth").GetComponent<TextMeshProUGUI>();
		currentNPCName = statusCanvas.transform.Find("CurrentNPCName").GetComponent<TextMeshProUGUI>();
		enemyHealth = statusCanvas.transform.Find("EnemyHealth").GetComponent<TextMeshProUGUI>();
		enemyName = statusCanvas.transform.Find("EnemyName").GetComponent<TextMeshProUGUI>();

        // Update all text field initially
        status.text = "Choose primary action, secondary action, switch party member, or run";
        UpdateEnemyHealthToScreen(GameInfo.getNPCHealth(enemyID));
		UpdateCurrentNPCHealthToScreen(GameInfo.getEgoCurrentHealth());
        enemyName.text = GameInfo.getName(enemyID);
        if (GameInfo.getEquipped(0).name != "")
        {
            primaryChoice.GetComponentInChildren<Text>().text = GameInfo.getEquipped(0).name;
        }
        else
        {
            primaryChoice.GetComponentInChildren<Text>().text = "Primary";
        }
        if (GameInfo.getEquipped(1).name != "")
        {
            secondaryChoice.GetComponentInChildren<Text>().text = GameInfo.getEquipped(1).name;
        }
        else
        {
            secondaryChoice.GetComponentInChildren<Text>().text = "Secondary";
        }

        // Set the art for the scene
        GameObject.Find("EnemyImage").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Combat/" + GameInfo.getName(GameInfo.currentNPC));
		GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Combat/Ego");
		GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Combat/ComScenes/" + GameInfo.getName(GameInfo.currentNPC));
	}

    // Call when enemy takes damage
	void UpdateEnemyHealthToScreen(int newHealth){	
		Debug.Log("TEST");	
		hpTextEnemy = "HP:" +newHealth+"/" + GameInfo.getEnemy(enemyID).MAXhealth;
		enemyHealth.text = hpTextEnemy;
	}

    // Call when party member takes damage or switching party members
	void UpdateCurrentNPCHealthToScreen(int newHealth){
		if(activePlayer<2){	
		hpTextPlayer = "HP:" +newHealth+"/" + GameInfo.getParty(activePlayer).npc.MAXhealth;
		currentNPCHealth.text = hpTextPlayer;
		}
		else{
		hpTextPlayer = "HP:" +newHealth+"/" + GameInfo.getEgoMaxHealth();
		currentNPCHealth.text = hpTextPlayer;
		}
	}

	void PrimaryAction(){
        int dmg;

        System.Random rnd = new System.Random();
		if (activePlayer == 2){
            dmg = rnd.Next(2, 18) + GameInfo.getPrimaryAttackBonus();
            GameInfo.updateNPCHealth(enemyID, dmg);
            UpdateEnemyHealthToScreen(GameInfo.getNPCHealth(enemyID));
		}
        else
        {
			dmg = rnd.Next(0, GameInfo.getNPCPrimaryAttack(GameInfo.party[activePlayer].slotID));
            if(GameInfo.party[activePlayer].slotID != 0)
            {
                GameInfo.updateNPCHealth(enemyID, dmg);
                UpdateEnemyHealthToScreen(GameInfo.getNPCHealth(enemyID));
            }
		}

		ToggleButtons(false);
        if (GameInfo.getNPCHealth(enemyID) == 0)
        {
            GameInfo.setDead(enemyID);
            // Killed Recruitable
            if (enemyID < 4)
            {
                status.text = "You have killed " + GameInfo.getName(enemyID) + ". You check them for any items to take with you.";
            }

            // Killed Shadow
            else if (enemyID < 7)
            {
                status.text = "You have killed " + GameInfo.getName(enemyID) + ". You can feel the shadows enter your body";
            }

            //Killed bounty
            else if (enemyID > 7)
            {
                status.text = "You have killed " + GameInfo.getName(enemyID) + ". Retrieve your proof of kill to later redeem your reward";
            }

            //Killed Ozul
            else
            {
                status.text = "You have killed " + GameInfo.getName(enemyID) + ". What have you done!?";
                // Put it in a coroutine so that you can read the words at end
                ///!!!!!!!!!!!
                /// you win scene!!
                ///!!!!!!!!!!!
            }

            SceneManager.LoadScene(GameInfo.prevScene);
            //KilledEnemy();
            // Wait for a bit then return to overworld, add their object to your inventory?
        }
        else
        {
            status.text = GameInfo.getName(enemyID) + " has taken " + dmg + " damage! " + GameInfo.getName(enemyID) + " is making their move.";
            //Call Enemy Attacks function
            StartCoroutine(WaitAfterAttack());
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
			//if (GameInfo.getParty (activePlayer - 1).slotID == 0) {
			//	if (PartyOnecurrentHP < 0) {
			//		healhold = 1;
			//	} else if (PartyTwocurrentHP < 0) {
			//		PartyTwocurrentHP = 1;
			//	}
			//} else {
			//	damagehold = damageCalc.Next (playerMinAtkSecondary, playerMaxAtkSecondary);
			//}

	}

	void SwitchPartyMember(){
		Debug.Log("SWITCH");
		//active player is ego if activePlayer = 2

		if (activePlayer == 0) {
			//switch to next party member
			activePlayer = 1;
        }
		if (activePlayer == 1) {
			//switch active player to Ego
			activePlayer = 2;
            GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Combat/Ego");
            currentNPCName.text = "Ego";
            UpdateCurrentNPCHealthToScreen(GameInfo.getEgoCurrentHealth());
            status.text = "You have changed to Ego.";
            // Change actions in Buttons
            if (GameInfo.getEquipped(0).name != "")
            {
                primaryChoice.GetComponentInChildren<Text>().text = GameInfo.getEquipped(0).name;
            }
            else
            {
                primaryChoice.GetComponentInChildren<Text>().text = "Primary";
            }
            if (GameInfo.getEquipped(1).name != "")
            {
                secondaryChoice.GetComponentInChildren<Text>().text = GameInfo.getEquipped(1).name;
            }
            else
            {
                secondaryChoice.GetComponentInChildren<Text>().text = "Secondary";
            }
        }
		if (activePlayer == 2) {
			//switch active player
			activePlayer = 0;
		}

        //STATEMENT CHANGING SPRITE
        if (activePlayer < 2)
        {
            GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Combat/" + GameInfo.getName(GameInfo.party[activePlayer].slotID));
            currentNPCName.text = GameInfo.getName(GameInfo.party[activePlayer].slotID);
            UpdateCurrentNPCHealthToScreen(GameInfo.party[activePlayer].slotID);
            status.text = "You have changed to " + GameInfo.getName(GameInfo.party[activePlayer].slotID) + ".";
            // Change actions in Buttons
            primaryChoice.GetComponentInChildren<Text>().text = GameInfo.getPrimaryActionName(GameInfo.party[activePlayer].slotID);
            secondaryChoice.GetComponentInChildren<Text>().text = GameInfo.getSecondaryActionName(GameInfo.party[activePlayer].slotID);
        }


    }

    void ChangeNPCImage(){

	}

	void RunFromCombat(){
			Debug.Log("RUN");
        Debug.Log(GameInfo.prevScene);
        SceneManager.LoadScene (GameInfo.prevScene);
	}

	void ToggleButtons(bool val){
        primaryChoice.interactable = val;
		secondaryChoice.interactable = val;
		partyMemberChoice.interactable = val;
		run.interactable = val;
	}

    /// <summary>
    /// Call this when it is the enemy's turn
    /// If the party member is killed switch to next party member... check if that one is dead to
    /// if Ego is dead and none of the party slotID's equal 0 (cynthia) --- you lose
    /// </summary>
	void EnemyAttaks(){
        int dmg;
    
        System.Random rnd = new System.Random();
        System.Random EgoRnd = new System.Random();

        //if not Cynthia
        if (enemyID != 0)
        {
            dmg = rnd.Next(0, GameInfo.getNPCPrimaryAttack(enemyID));
        }
        else
        {
            dmg = rnd.Next(0, 1);
        }

        // If NOT EGO
        if (activePlayer < 2)
        {
            GameInfo.updateNPCHealth(GameInfo.party[activePlayer].slotID, dmg);
            UpdateCurrentNPCHealthToScreen(GameInfo.getNPCHealth(GameInfo.party[activePlayer].slotID));

            if (GameInfo.getNPCHealth(GameInfo.party[activePlayer].slotID) == 0)
            {
                status.text = GameInfo.getName(GameInfo.party[activePlayer].slotID) + " has been killed by " + GameInfo.getName(enemyID);
                GameInfo.setDead(GameInfo.party[activePlayer].slotID);
                // Check If anyone else is alive...
                SwitchPartyMember();
            }
            else
            {
                status.text = GameInfo.getName(GameInfo.party[activePlayer].slotID) + " has taken " + dmg + " damage! Make your move.";
                StartCoroutine(WaitAfterEnemy());
            }
        }
        // IS EGO
        else
        {
            // if Ego has extra line of defense, check how much defense he gets (from all three items)
            int defense = EgoRnd.Next(0, (GameInfo.getEgoDefense()));
            if (dmg - defense < 0)
            {
                dmg = 0;
            }
            else
            {
                dmg -= defense;
            }

            GameInfo.updateCurrentHealth(dmg);
            UpdateCurrentNPCHealthToScreen(GameInfo.getEgoCurrentHealth());

            if (GameInfo.getEgoCurrentHealth() == 0)
            {
                if(partyMember1 != 0 && partyMember2 != 0)
                {
                    status.text = "Ego has been killed by " + GameInfo.getName(enemyID) + " and you have no one to save you!";
                    //////!!!!!!!!!!
                    ///YOU LOSE SCENE!
                    //////!!!!!!!!!!
                }
                else
                {
                    status.text = "Ego has been killed by " + GameInfo.getName(enemyID) + "! But wait...";
                    // SWITCH Members
                    StartCoroutine(WaitAfterEnemy());
                }
                // Wait for a bit then return to overworld, add their object to your inventory?
            }
            else
            {
                status.text = "Ego has taken " + dmg + " damage! Make your move.";
                StartCoroutine(WaitAfterEnemy());
            }
        }
    }

	void EndGame(){

	}

	IEnumerator WaitAfterAttack(){
		yield return new WaitForSeconds(3);
        EnemyAttaks();
    }

    IEnumerator WaitAfterEnemy()
    {
        yield return new WaitForSeconds(3);
        ToggleButtons(true);
    }

    IEnumerator KilledEnemy()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(GameInfo.prevScene);
        SceneManager.LoadScene(GameInfo.prevScene);
    }
}
