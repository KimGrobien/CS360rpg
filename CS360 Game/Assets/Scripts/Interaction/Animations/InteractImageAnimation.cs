using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractImageAnimation : MonoBehaviour {
	private SpriteRenderer dots;
	public string dotsObjName;
	// Use this for initialization
	void Start(){
		dots = GameObject.Find(dotsObjName).GetComponent<SpriteRenderer>();
	}
		
	void OnTriggerEnter2D (Collider2D other) {
		dots.enabled = true;
	}

	void OnTriggerExit2D (Collider2D other){
		dots.enabled = false;
	}
}
