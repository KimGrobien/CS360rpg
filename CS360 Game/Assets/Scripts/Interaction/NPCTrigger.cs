using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCTrigger : MonoBehaviour {

	public int NPCID;
	private bool inTrigger;
	public bool automaticTrigger;

	void OnTriggerEnter2D(Collider2D other){
		GameInfo.currentNPC = NPCID;
		if(other.gameObject.tag == "Player"){
				inTrigger = true;
			}
	}

	void OnTriggerExit2D(Collider2D other){
		GameInfo.currentNPC = -1;
		if(other.gameObject.tag == "Player"){
			inTrigger = false;
		}
			
	}
	
	void Update(){
		if(inTrigger){
			if(Input.GetKeyDown(KeyCode.S)){
					GameInfo.prevScene = SceneManager.GetActiveScene().buildIndex;
					GameInfo.prevPos = GameObject.Find("Player").GetComponent<SpriteRenderer>().transform.position;
					SceneManager.LoadScene("Menu");
			}
			if(automaticTrigger){
					GameInfo.prevScene = SceneManager.GetActiveScene().buildIndex;
					GameInfo.prevPos = GameObject.Find("Player").GetComponent<SpriteRenderer>().transform.position;
					SceneManager.LoadScene("Menu");
			}
		}
		
	}
}
