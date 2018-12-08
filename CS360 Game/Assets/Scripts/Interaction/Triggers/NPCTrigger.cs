using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCTrigger : MonoBehaviour {
	// this is used to update GameInfo so we know which npc we're interacting with when the menu opens
	public int NPCID;
	// used in the update function to determine if we need to listen for key presses
	private bool inTrigger;
	//some enemy have to be encountered and cannot be passed
	// in those situations we'll call them automatic triggers
	public bool automaticTrigger;

	/// <summary>
    /// When the player enters the trigger area update GameInfo so we know who we're talking to
	/// and let Update() know the player is within the trigger area
    /// </summary>
	void OnTriggerEnter2D(Collider2D other){
		GameInfo.currentNPC = NPCID;
		if(other.gameObject.tag == "Player"){
				inTrigger = true;
			}
	}
	/// <summary>
    /// When the player exits the trigger area update GameInfo so we know we are not talking to anyone
	/// and let Update() know the player is outside the trigger area
    /// </summary>
	void OnTriggerExit2D(Collider2D other){
		GameInfo.currentNPC = -1;
		if(other.gameObject.tag == "Player"){
			inTrigger = false;
		}
			
	}
	/// <summary>
    /// While the player is within the trigger area we will either listen for a key press or
	/// if it as automatictrigger begin the interaction
    /// </summary>
	void Update(){
		if(inTrigger){
			// either way, by key press or automatic we go to the Menu with the dialogue side active
			if(Input.GetKeyDown(KeyCode.S)){
				goToMenu();
			}
			if(automaticTrigger){
				goToMenu();
			}
		}
		
	}
	/// <summary>
    /// Open Menu Scene, while remembering where the player previously was in GameInfo
    /// </summary>
	void goToMenu(){
		GameInfo.prevScene = SceneManager.GetActiveScene().buildIndex;
		GameInfo.prevPos = GameObject.Find("Player").GetComponent<SpriteRenderer>().transform.position;
		SceneManager.LoadScene("Menu");
	}
}
