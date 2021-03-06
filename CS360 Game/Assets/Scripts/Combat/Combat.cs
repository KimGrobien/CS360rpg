﻿
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
    // Defining Class Variables
	Animator playerAnim;
	Canvas buttons, statusCanvas;
	Button primaryChoice, secondaryChoice, partyMemberChoice, run;
	TextMeshProUGUI status, currentNPCName, currentNPCHealth, enemyName, enemyHealth;
	private string hpTextPlayer;    // Compose the string to display current party member's health
	private string hpTextEnemy;     // Compose the string to display current enemy's member's health
    private int activePlayer = 2;   // Holds data to keep track of which party member is fighting, Ego = 2, party member 1 = 0, and party member 2 = 1
    private string endText;         // Holds string data for ending scene text
    Image EgoArmor, EgoItemUsed;    // Display the armor and item that Ego has equipped on his body

    //Who we're fighting
    private int enemyID = GameInfo.currentNPC;

    //Id's of party members
    int partyMember1 = GameInfo.party[0].slotID;
    int partyMember2 = GameInfo.party[1].slotID;
    /// <summary>
    /// Runs at start for initialization of all the buttons and images
    /// </summary>
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

        //FindEgosItems
        EgoArmor = buttons.transform.Find("EgoArmor").GetComponent<Image>();
        EgoItemUsed = buttons.transform.Find("EgoItemUsed").GetComponent<Image>();
        if (GameInfo.getEquipped(2).name != null)
        {
            EgoArmor.color = Color.clear;
            EgoArmor.sprite = GameInfo.getEquipped(2).ArmorFullImage;
            StartCoroutine(WaitToAddArmor());
        }
        else
        {
            EgoArmor.color = Color.clear;
        }

        EgoItemUsed.color = Color.clear;

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

    /// <summary>
    /// Call when enemy takes damage to display knew health
    /// </summary>
    /// <param name="newHealth">the new health of enemy after damage</param>
	void UpdateEnemyHealthToScreen(int newHealth){	
		hpTextEnemy = "HP:" +newHealth+"/" + GameInfo.getEnemy(enemyID).MAXhealth;
		enemyHealth.text = hpTextEnemy;
	}

    /// <summary>
    /// Call when party member takes damage or switching party members, to display new health ratio
    /// </summary>
    /// <param name="newHealth">new health of party member after damage or heal</param>
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

    /// <summary>
    /// Called on primary action button click, deals damage or heals depending on party member
    /// </summary>
	void PrimaryAction(){
        ToggleButtons(false);
        int dmg;

        System.Random rnd = new System.Random();
		if (activePlayer == 2){
            dmg = rnd.Next(2, 18) + GameInfo.getPrimaryAttackBonus();
            if (GameInfo.ReturnEquippedItem(0) >-1)
            {
                EgoItemUsed.sprite = GameInfo.getEquipped(0).eqImage;
                EgoItemUsed.color = Color.white;
            }
            
            // If ego is using potion
            if (GameInfo.getEquipped(0).healBonus > 0)
            {
                GameInfo.updateCurrentHealth(-(dmg));
                if (GameInfo.party[0].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[0].slotID, -dmg);
                }
                if (GameInfo.party[1].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[1].slotID, -dmg);
                }
                UpdateCurrentNPCHealthToScreen(GameInfo.getEgoCurrentHealth());
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
                    GameInfo.updateCurrentHealth(-dmg);
                }
                if (activePlayer == 1 && GameInfo.party[0].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[0].slotID, -dmg);
                }
                else if (GameInfo.party[1].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[1].slotID, -dmg);
                }
            }
		}

        if (GameInfo.getNPCHealth(enemyID) <= 0)
        {
            StartCoroutine(AttackAnimPlayer());
            GameInfo.setDead(enemyID);
            // Killed Recruitable
            if (enemyID < 4)
            {
                if (enemyID == 0)
                {
                    GameInfo.setEquipmentColor(11, Color.white);
                    GameInfo.setEquipmentOwned(11);
                }
                else if (enemyID == 2)
                {
                    GameInfo.setEquipmentColor(3, Color.white);
                    GameInfo.setEquipmentOwned(3);
                }
                else if (enemyID == 3)
                {
                    GameInfo.setEquipmentColor(7, Color.white);
                    GameInfo.setEquipmentOwned(7);
                }
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
                if (enemyID == 8)
                {
                    GameInfo.setEquipmentOwned(14);
                }
                else if (enemyID == 9)
                {
                    GameInfo.setEquipmentOwned(12);
                }
                else
                {
                    GameInfo.setEquipmentOwned(13);
                }
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
                StartCoroutine(AttackAnimPlayer());
                status.text = GameInfo.getName(enemyID) + " has taken " + dmg + " damage! " + GameInfo.getName(enemyID) + " is making their move.";
            }
            else
            {
                status.text = "Your party members have been healed! " + GameInfo.getName(enemyID) + " is making their move.";
                StartCoroutine(WaitAfterAttack());
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
                StartCoroutine(WaitAfterAttack());
                EgoItemUsed.color = Color.clear;
            }
            else
            {
                StartCoroutine(AttackAnimPlayer());
                status.text = GameInfo.getName(enemyID) + " has taken " + dmg + " damage! " + GameInfo.getName(enemyID) + " is making their move.";
            }
            //Call Enemy Attacks function
            StartCoroutine(WaitAfterAttack());
        }
	}

    /// <summary>
    /// This is called when the secondary action is pressed.
    /// </summary>
	void SecondaryAction(){
        ToggleButtons(false);
        int dmg;

        System.Random rnd = new System.Random();
        if (activePlayer == 2)
        {
            if (GameInfo.ReturnEquippedItem(1) > -1)
            {
                EgoItemUsed.sprite = GameInfo.getEquipped(1).eqImage;
                EgoItemUsed.color = Color.white;
            }
            dmg = rnd.Next(2, 18) + GameInfo.getEgoSecondary();
            // If ego is using potion
            if (GameInfo.getEquipped(1).healBonus > 0)
            {
                GameInfo.updateCurrentHealth(-dmg);
                if (GameInfo.party[0].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[0].slotID, -dmg);
                }
                if (GameInfo.party[1].slotID != -1)
                {
                    GameInfo.updateNPCHealth(GameInfo.party[1].slotID, -dmg);
                }
                UpdateCurrentNPCHealthToScreen(GameInfo.getEgoCurrentHealth());
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
            if (GameInfo.party[activePlayer].slotID == 1)
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

        if (GameInfo.getNPCHealth(enemyID) <= 0)
        {
            StartCoroutine(AttackAnimPlayer());
            GameInfo.setDead(enemyID);
            // Killed Recruitable
            if (enemyID < 4)
            {
                if (enemyID == 0)
                {
                    GameInfo.setEquipmentColor(11,Color.white);
                    GameInfo.setEquipmentOwned(11);
                }
                else if(enemyID == 2)
                {
                    GameInfo.setEquipmentColor(3, Color.white);
                    GameInfo.setEquipmentOwned(3);
                }
                else if (enemyID == 3)
                {
                    GameInfo.setEquipmentColor(7, Color.white);
                    GameInfo.setEquipmentOwned(7);
                }
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
                if (enemyID == 8)
                {
                    GameInfo.setEquipmentOwned(14);
                }
                else if (enemyID == 9)
                {
                    GameInfo.setEquipmentOwned(12);
                }
                else
                {
                    GameInfo.setEquipmentOwned(13);
                }

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
        }
        else if (activePlayer != 2)
        {
            if (GameInfo.party[activePlayer].slotID != 0)
            {
                StartCoroutine(AttackAnimPlayer());
                status.text = GameInfo.getName(enemyID) + " has taken " + dmg + " damage! " + GameInfo.getName(enemyID) + " is making their move.";
            }
            else
            {
                StartCoroutine(WaitAfterAttack());
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
                StartCoroutine(WaitAfterAttack());
                EgoItemUsed.color = Color.clear;
            }
            else
            {
                StartCoroutine(AttackAnimPlayer());
                status.text = GameInfo.getName(enemyID) + " has taken " + dmg + " damage! " + GameInfo.getName(enemyID) + " is making their move.";
            }
            StartCoroutine(WaitAfterAttack());
        }
    }

    /// <summary>
    /// This is called when the swtich button is pressed. determine if anyone in the party is alive or even been recruited
    /// </summary>
	void SwitchPartyMember(){
		//Debug.Log("SWITCH");
		playerAnim.Play("IDle", -1, 0f);//Set anim back to default
		//active player is ego if activePlayer = 2

		if (activePlayer == 0) {
			//if next slot is not empty and the next member is not dead
			if (GameInfo.getParty(1).slotID != -1 && !GameInfo.getParty(1).npc.dead)
            {
                EgoArmor.color = Color.clear;
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
                EgoArmor.color = Color.clear;
                activePlayer = 0;
                playerAnim.SetInteger("id", GameInfo.getParty(activePlayer).slotID);
            }
        }
		else if (activePlayer == 2) {
			//switch to member 0 if not dead or not empty
            //Debug.Log(GameInfo.getParty(0).npc.dead);

			if (GameInfo.getParty(0).slotID != -1 && !GameInfo.getParty(0).npc.dead)
            {
                EgoArmor.color = Color.clear;
                activePlayer = 0;
				playerAnim.SetInteger("id", GameInfo.getParty(activePlayer).slotID);
			}
            // else try and switch to next party member (if not dead and if not empty)
            else if (GameInfo.getParty(1).slotID != -1 && !GameInfo.getParty(1).npc.dead)
            {
                EgoArmor.color = Color.clear;
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

    /// <summary>
    /// Function called when Ego is to be switched to
    /// </summary>
	public void switchActiveToEgo(){
		activePlayer = 2;
        if (GameInfo.getEquipped(2).name != null)
        {
            EgoArmor.color = Color.clear;
            EgoArmor.sprite = GameInfo.getEquipped(2).ArmorFullImage;
            StartCoroutine(WaitToAddArmor());
        }
        else
        {
            EgoArmor.color = Color.clear;
        }
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

    /// <summary>
    /// Load previous scene you fraidy cat
    /// /// </summary>
	void RunFromCombat(){
        playMusic.StopMusic("battle");
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(GameInfo.prevScene));
        playMusic.PlayMusicBySceneName(sceneName);
        SceneManager.LoadScene(GameInfo.prevScene);
	}


    /// <summary>
    /// toggle all the combat buttons (disable and able the buttons when appropriate
    /// </summary>
    /// <param name="val"> This is either true or false </param>
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
			StartCoroutine(AttackAnimEnemy());
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

    /// <summary>
    /// After you attack wait a few seconds and let the enemy attack
    /// </summary>
    /// <returns></returns>
	IEnumerator WaitAfterAttack(){
		yield return new WaitForSeconds(6);
        EnemyAttaks();
    }

    /// <summary>
    /// If your party member dies wait and switch party members
    /// </summary>
    /// <returns></returns>
    IEnumerator AfterPartyMemberDies()
    {
        yield return new WaitForSeconds(3);
        SwitchPartyMember();
        ToggleButtons(true);
    }

    /// <summary>
    /// You killed the enemy, he disappears and exits the last scene
    /// </summary>
    /// <returns></returns>
    IEnumerator KilledEnemy()
    {
        SpriteRenderer sprender;
        sprender = GameObject.Find("EnemyImage").GetComponent<SpriteRenderer>();
        sprender.color = Color.grey;
        yield return new WaitForSeconds(0.4f);
        sprender.color = Color.clear;
        yield return new WaitForSeconds(0.4f);
        sprender.enabled = false;
        yield return new WaitForSeconds(4);
        playMusic.StopMusic("battle");
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(GameInfo.prevScene));
        playMusic.PlayMusicBySceneName(sceneName);
        SceneManager.LoadScene(GameInfo.prevScene);
    }

    /// <summary>
    /// The game is over you lost or won
    /// </summary>
    /// <returns></returns>
    IEnumerator GameEnds(){
        ToggleButtons(false);
        status.text = endText;
        yield return new WaitForSeconds(4);
        playMusic.StopMusic("battle");
		if (GameInfo.end){
            playMusic.PlayMusicBySceneName("You Win");
			SceneManager.LoadScene("You Win");
		}else{
            playMusic.PlayMusicBySceneName("You Lose");
			SceneManager.LoadScene("You Lose");
		}
    }

    /// <summary>
    /// Enemy is hit. he flashes.
    /// </summary>
    /// <returns></returns>
	IEnumerator AttackAnimPlayer ()
	{
		SpriteRenderer sprender;
		sprender = GameObject.Find ("EnemyImage").GetComponent<SpriteRenderer> ();
		AudioSource attackSound = GameObject.Find ("attackSound").GetComponent<AudioSource> ();
		attackSound.Play ();
		sprender.enabled = false;
		yield return new WaitForSeconds (0.2f);
		sprender.enabled = true;
		yield return new WaitForSeconds (0.2f);
		sprender.enabled = false;
		yield return new WaitForSeconds (0.2f);
		sprender.enabled = true;
		yield return new WaitForSeconds (0.2f);
		sprender.enabled = false;
		yield return new WaitForSeconds (0.2f);
		sprender.enabled = true;
		yield return new WaitForSeconds (0.2f);
        EgoItemUsed.color = Color.clear;
    }

    /// <summary>
    /// flash party member
    /// </summary>
    /// <returns></returns>
	IEnumerator AttackAnimEnemy ()
	{
		SpriteRenderer sprender;
		sprender = GameObject.Find ("Player").GetComponent<SpriteRenderer> ();
		AudioSource attackSound = GameObject.Find ("attackSound").GetComponent<AudioSource> ();
		attackSound.Play ();
		sprender.enabled = false;
        EgoArmor.enabled = false;
		yield return new WaitForSeconds (0.2f);
        EgoArmor.enabled = true;
        sprender.enabled = true;
		yield return new WaitForSeconds (0.2f);
        EgoArmor.enabled = false;
        sprender.enabled = false;
		yield return new WaitForSeconds (0.2f);
        EgoArmor.enabled = true;
        sprender.enabled = true;
		yield return new WaitForSeconds (0.2f);
        EgoArmor.enabled = false;
        sprender.enabled = false;
		yield return new WaitForSeconds (0.2f);
        EgoArmor.enabled = true;
        sprender.enabled = true;
		yield return new WaitForSeconds (0.2f);

	}

    /// <summary>
    /// delay adding armor
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitToAddArmor()
    {
        yield return new WaitForSeconds(.65f);
        EgoArmor.color = Color.white;
    }
}
