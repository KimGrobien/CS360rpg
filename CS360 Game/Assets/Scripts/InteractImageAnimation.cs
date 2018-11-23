using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InteractImageAnimation : MonoBehaviour {
	GameObject canvas;
	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("InteractImage");		
		GameObject.Find("box").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Empty");
		
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		
		GameObject.Find("box").GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractImage");
	}

	void OnTriggerExit2D (Collider2D other){
		GameObject.Find("box").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Empty");
	}
}
