using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour {

	private Animator anim;

	void Start() {
		anim = GameObject.Find("castle_door").GetComponent<Animator>();
	}
	void OnTriggerEnter2D (Collider2D other) {
		anim.SetBool("playOpen", true);
		GameObject.Find("Player").GetComponent<PlayerController>().frozen = true;
		StartCoroutine(wait());
	}

	void OnTriggerExit2D (Collider2D other){
		
	}

	IEnumerator wait(){
		yield return new WaitForSeconds (2);
		GameObject.Find("Player").GetComponent<PlayerController>().frozen = false;
	}
}
