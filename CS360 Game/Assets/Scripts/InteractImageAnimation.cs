using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractImageAnimation : MonoBehaviour {
	GameObject canvas,canvas2,canvas3;
	public bool one, two, three, isBounty;
	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("InteractImage");		
		GameObject.Find("box").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Empty");
		canvas3 = GameObject.Find("InteractImage");		
		GameObject.Find("box2").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Empty");
		if(isBounty){
		canvas2 = GameObject.Find("InteractImage1");		
		GameObject.Find("box1").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Empty");
		}
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if(one){
		GameObject.Find("box").GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractImage");
		}
		else if(two){
		GameObject.Find("box1").GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractImage");
		}
		else if(three){
		GameObject.Find("box2").GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractImage");
		}
		else{
		GameObject.Find("box").GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractImage");
		}
	}

	void OnTriggerExit2D (Collider2D other){
		if(one){
		GameObject.Find("box").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Empty");
		}
		else if(two){
		GameObject.Find("box1").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Empty");
		}
		else if(three){
		GameObject.Find("box2").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Empty");
		}
		else{
		GameObject.Find("box").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Empty");
		}

	}
}
