using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour {

	private Animator anim;

	void Start() {
		//Finds the animator for the castle door
		anim = GameObject.Find("castle_door").GetComponent<Animator>();
	}
	void OnTriggerEnter2D (Collider2D other) {
		if (!anim.GetBool("playOpen")){//Only plays if it hasn't been played yet
			anim.SetBool("playOpen", true);
			//Freezes the player so he can't move while animation is playing
			GameObject.Find("Player").GetComponent<PlayerController>().frozen = true;
			StartCoroutine(wait());
		}
	}

	IEnumerator wait(){
		//Waits for 2 seconds then unfreezes player
		yield return new WaitForSeconds (2);
		GameObject.Find("Player").GetComponent<PlayerController>().frozen = false;
	}
}
