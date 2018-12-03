
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
	Animator playerAnim;
	Canvas buttons, statusCanvas;
	Button primaryChoice, secondaryChoice, partyMemberChoice, run;
	TextMeshProUGUI status, currentNPCName, currentNPCHealth, enemyName, enemyHealth;
	private string hpTextPlayer;
	private string hpTextEnemy;
    private int activePlayer = 2;
    private string endText;

    //Who we're fighting
    private int enemyID = GameInfo.currentNPC;

    //Id's of party members
    int partyMember1 = GameInfo.party[0].slotID;
    int partyMember2 = GameInfo.party[1].slotID;

    // Use this for initialization
    void Start()
	{
		//Find Player Animator
		playerAnim = GameObject.Find("Player").GetComponent<Animator>();
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
        if (GameInfo.getEquipped(0).name != null)
        {
            primaryChoice.GetComponentInChildren<Text>().text = GameInfo.getEquipped(0).name;
        }
        else
        {
            primaryChoice.GetComponentInChildren<Text>().text = "Primary";
        }
        if (GameInfo.getEquipped(1).name != null)
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
            // If ego is using potion
            if(GameInfo.getEquipped(0).healBonus > 0)
            {
                GameInfo.updateCurrentHealth(-dmg / 2);
                if (GameInfo.party[0].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[0].slotID, -dmg / 2);
                }
                if (GameInfo.party[1].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[1].slotID, -dmg / 2);
                }
            }
            // No using potion
            else
            {
                GameInfo.updateNPCHealth(enemyID, dmg);
                UpdateEnemyHealthToScreen(GameInfo.getNPCHealth(enemyID));
            } 
		}
        else
        {
			dmg = rnd.Next(0, GameInfo.getNPCPrimaryAttack(GameInfo.party[activePlayer].slotID));
            // If not healing
            if(GameInfo.party[activePlayer].slotID != 0 && GameInfo.party[activePlayer].slotID != 2)
            {
                GameInfo.updateNPCHealth(enemyID, dmg);
                UpdateEnemyHealthToScreen(GameInfo.getNPCHealth(enemyID));
            }
            //Healing 
            else
            {
                if (GameInfo.isAlive)
                {
                    GameInfo.updateCurrentHealth(-dmg / 2);
                }
                if (activePlayer == 1 && GameInfo.party[0].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[0].slotID, -dmg / 2);
                }
                else if (GameInfo.party[1].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[1].slotID, -dmg / 2);
                }
            }
		}

		ToggleButtons(false);
        if (GameInfo.getNPCHealth(enemyID) <= 0)
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
                GameInfo.end = true;
                endText = "You have killed " + GameInfo.getName(enemyID) + ". What have you done!?";
                // Put it in a coroutine so that you can read the words at end... but that aint been working for me?
                StartCoroutine(GameEnds());
            }

            StartCoroutine(KilledEnemy());
            // Wait for a bit then return to overworld, add their object to your inventory?
        }
        //If not ego attacking
        else if (activePlayer != 2)
        {
            if (GameInfo.party[activePlayer].slotID != 0 && GameInfo.party[activePlayer].slotID != 2)
            {
                status.text = GameInfo.getName(enemyID) + " has taken " + dmg + " damage! " + GameInfo.getName(enemyID) + " is making their move.";
            }
            else
            {
                status.text = "Your party members have been healed! " + GameInfo.getName(enemyID) + " is making their move.";
            }
            //Call Enemy Attacks function
            StartCoroutine(WaitAfterAttack());
        }
        //if ego attacking/healing
        else
        {
            if (GameInfo.getEquipped(0).healBonus > 0)
            {
                status.text = "Ego has used heal. " + GameInfo.getName(enemyID) + " is making their move.";
            }
            else
            {
                status.text = GameInfo.getName(enemyID) + " has taken " + dmg + " damage! " + GameInfo.getName(enemyID) + " is making their move.";
            }
            //Call Enemy Attacks function
            StartCoroutine(WaitAfterAttack());
           
        }
	}

	void SecondaryAction(){
        int dmg;

        System.Random rnd = new System.Random();
        if (activePlayer == 2)
        {
            dmg = rnd.Next(2, 18) + GameInfo.getEgoSecondary();
            // If ego is using potion
            if (GameInfo.getEquipped(1).healBonus > 0)
            {
                GameInfo.updateCurrentHealth(-dmg / 2);
                if (GameInfo.party[0].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[0].slotID, -dmg / 2);
                }
                if (GameInfo.party[1].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[1].slotID, -dmg / 2);
                }
            }
            // No using potion
            else
            {
                GameInfo.updateNPCHealth(enemyID, dmg);
                UpdateEnemyHealthToScreen(GameInfo.getNPCHealth(enemyID));
            }
        }
        else
        {
            // If anker, its a fixed value
            if (GameInfo.party[1].slotID == 1)
            {
                dmg = GameInfo.getNPCSecondaryAttack(GameInfo.party[activePlayer].slotID);
            }
            // If not anker, range attack
            else
            {
                dmg = rnd.Next(0, GameInfo.getNPCSecondaryAttack(GameInfo.party[activePlayer].slotID));
            }

            //If not cynthia
            if (GameInfo.party[activePlayer].slotID != 0)
            {
                GameInfo.updateNPCHealth(enemyID, dmg);
                UpdateEnemyHealthToScreen(GameInfo.getNPCHealth(enemyID));
            }
            // Cynthia revives
            else
            {
                if (!GameInfo.isAlive)
                {
                    GameInfo.isAlive = true;
                    GameInfo.updateCurrentHealth(-GameInfo.getEgoMaxHealth());
                }
                else if (activePlayer == 1 && GameInfo.party[0].npc.dead)
                {
                    GameInfo.setNPCAlive(GameInfo.party[0].slotID);
                    GameInfo.setPartyMemberAlive(0);
                    GameInfo.updateNPCHealth(GameInfo.party[0].slotID, -GameInfo.party[0].npc.MAXhealth);
                }
                else if (activePlayer == 0 && GameInfo.party[1].npc.dead)
                {
                    GameInfo.setNPCAlive(GameInfo.party[1].slotID);
                    GameInfo.setPartyMemberAlive(1);
                    GameInfo.updateNPCHealth(GameInfo.party[1].slotID, -GameInfo.party[1].npc.MAXhealth);
                }
            }
        }

        ToggleButtons(false);
        if (GameInfo.getNPCHealth(enemyID) <= 0)
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
                GameInfo.setEquipmentOwned(3);
                GameInfo.setEquipmentOwned(7);
                GameInfo.setEquipmentOwned(11);

                status.text = "You have killed " + GameInfo.getName(enemyID) + ". Retrieve your proof of kill to later redeem your reward";
            }

            //Killed Ozul
            else
            {
                GameInfo.end = true;
                endText = "You have killed " + GameInfo.getName(enemyID) + ". What have you done!?";
                // Put it in a coroutine so that you can read the words at end... but that aint been working for me?
                StartCoroutine(GameEnds());
            }

            StartCoroutine(KilledEnemy());
            // Wait for a bit then return to overworld, add their object to your inventory?
        }
        else if (activePlayer != 2)
        {
            if (GameInfo.party[activePlayer].slotID != 0)
            {
                status.text = GameInfo.getName(enemyID) + " has taken " + dmg + " damage! " + GameInfo.getName(enemyID) + " is making their move.";
            }
            else
            {
                status.text = "You have attempted to revive a dead party member! " + GameInfo.getName(enemyID) + " is making their move.";
            }
            //Call Enemy Attacks function
            StartCoroutine(WaitAfterAttack());
        }
        else
        {
            if (GameInfo.getEquipped(1).healBonus > 0)
            {
                status.text = "Ego has used heal. " + GameInfo.getName(enemyID) + " is making their move.";
            }
            else
            {
                status.text = GameInfo.getName(enemyID) + " has taken " + dmg + " damage! " + GameInfo.getName(enemyID) + " is making their move.";
            }
            StartCoroutine(WaitAfterAttack());
        }
    }

	void SwitchPartyMember(){
		//Debug.Log("SWITCH");
		playerAnim.Play("IDle", -1, 0f);//Set anim back to default
		//active player is ego if activePlayer = 2

		if (activePlayer == 0) {
			//if next slot is not empty and the next member is not dead
			if (GameInfo.getParty(1).slotID != -1 && !GameInfo.getParty(1).npc.dead)
            {
				activePlayer = 1;
				playerAnim.SetInteger("id", GameInfo.getParty(activePlayer).slotID);
			}else if (GameInfo.isAlive){
				switchActiveToEgo();
			}
        }
		else if (activePlayer == 1)
        {
            //switch active player to Ego if he is alive
            if (GameInfo.isAlive)
            { 
                switchActiveToEgo();
            }
            //else switch to next player if Ego is dead and cynthia is in party
            else if(!GameInfo.getParty(0).npc.dead)
            {
                activePlayer = 0;
                playerAnim.SetInteger("id", GameInfo.getParty(activePlayer).slotID);
            }
        }
		else if (activePlayer == 2) {
			//switch to member 0 if not dead or not empty
            //Debug.Log(GameInfo.getParty(0).npc.dead);

			if (GameInfo.getParty(0).slotID != -1 && !GameInfo.getParty(0).npc.dead)
            {
				activePlayer = 0;
				playerAnim.SetInteger("id", GameInfo.getParty(activePlayer).slotID);
			}
            // else try and switch to next party member (if not dead and if not empty)
            else if (GameInfo.getParty(1).slotID != -1 && !GameInfo.getParty(1).npc.dead)
            {
				activePlayer = 1;
				playerAnim.SetInteger("id", GameInfo.getParty(activePlayer).slotID);
			}
		}

        //STATEMENT CHANGING SPRITE
        if (activePlayer < 2)
        {
            GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Combat/" + GameInfo.getName(GameInfo.party[activePlayer].slotID));
            currentNPCName.text = GameInfo.getName(GameInfo.party[activePlayer].slotID);
            UpdateCurrentNPCHealthToScreen(GameInfo.getNPCHealth(GameInfo.party[activePlayer].slotID));
            status.text = "You have changed to " + GameInfo.getName(GameInfo.party[activePlayer].slotID) + ".";
            // Change actions in Buttons
            primaryChoice.GetComponentInChildren<Text>().text = GameInfo.getPrimaryActionName(GameInfo.party[activePlayer].slotID);
            secondaryChoice.GetComponentInChildren<Text>().text = GameInfo.getSecondaryActionName(GameInfo.party[activePlayer].slotID);
        }
    }

	public void switchActiveToEgo(){
		activePlayer = 2;
			playerAnim.SetInteger("id", -1);
            GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Combat/Ego");
            currentNPCName.text = "Ego";
            UpdateCurrentNPCHealthToScreen(GameInfo.getEgoCurrentHealth());
            status.text = "You have changed to Ego.";
            // Change actions in Buttons
            if (GameInfo.getEquipped(0).name != null)
            {
                primaryChoice.GetComponentInChildren<Text>().text = GameInfo.getEquipped(0).name;
            }
            else
            {
                primaryChoice.GetComponentInChildren<Text>().text = "Primary";
            }
            if (GameInfo.getEquipped(1).name != null)
            {
                secondaryChoice.GetComponentInChildren<Text>().text = GameInfo.getEquipped(1).name;
            }
            else
            {
                secondaryChoice.GetComponentInChildren<Text>().text = "Secondary";
            }
	}

	void RunFromCombat(){
		//Debug.Log("RUN");
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
			if (enemyID < 4){//Recruitable, returned val from getNPC... is max val
				dmg = rnd.Next(0, GameInfo.getNPCPrimaryAttack(enemyID));
			}else{//Enemy, returned val from getNPC... is actual damage
				dmg = GameInfo.getNPCPrimaryAttack(enemyID);
			}
        }
        else
        {
			//Debug.Log("TWO");
            dmg = rnd.Next(0, 2);
        }

        // If NOT EGO
        if (activePlayer < 2)
        {
            GameInfo.updateNPCHealth(GameInfo.party[activePlayer].slotID, dmg);
            UpdateCurrentNPCHealthToScreen(GameInfo.getNPCHealth(GameInfo.party[activePlayer].slotID));

            if (GameInfo.getNPCHealth(GameInfo.party[activePlayer].slotID) == 0)
            {
                status.text = GameInfo.getName(GameInfo.party[activePlayer].slotID) + " has been killed by " + GameInfo.getName(enemyID) + ". Switch Party Member";
                GameInfo.setPartyMemberDead(activePlayer);
                GameInfo.setDead(GameInfo.party[activePlayer].slotID);
                // Check If anyone else is alive...
                StartCoroutine(AfterPartyMemberDies());
            }
            else
            {
                status.text = GameInfo.getName(GameInfo.party[activePlayer].slotID) + " has taken " + dmg + " damage! Make your move.";
                ToggleButtons(true);
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
                if((partyMember1 != 0 && partyMember2 != 0) || (partyMember1 == 0 && GameInfo.party[0].npc.dead) || (partyMember2 == 0 && GameInfo.party[1].npc.dead))
                {   
                    GameInfo.end = false;
                    GameInfo.isAlive = false;
                    endText = "Ego has been killed by " + GameInfo.getName(enemyID) + " and you have no one to save you!";
                    // Put it in a coroutine so that you can read the words at end... but that aint been working for me?
                    StartCoroutine(GameEnds());
                }
                else
                {
                    status.text = "Ego has been killed by " + GameInfo.getName(enemyID) + "! But wait...";
                    // SWITCH Members
                    GameInfo.isAlive = false;
                    StartCoroutine(AfterPartyMemberDies());
                }
                // Wait for a bit then return to overworld, add their object to your inventory?
            }
            else
            {
                status.text = "Ego has taken " + dmg + " damage! Make your move.";
                ToggleButtons(true);
            }
        }
    }


	IEnumerator WaitAfterAttack(){
		yield return new WaitForSeconds(3);
        EnemyAttaks();
    }

    IEnumerator AfterPartyMemberDies()
    {
        yield return new WaitForSeconds(3);
        SwitchPartyMember();
        ToggleButtons(true);
    }

    IEnumerator KilledEnemy()
    {
        Debug.Log(GameInfo.CheckIfDead(enemyID));
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(GameInfo.prevScene);
    }

    IEnumerator GameEnds(){
        status.text = endText;
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("End");
    }
}
