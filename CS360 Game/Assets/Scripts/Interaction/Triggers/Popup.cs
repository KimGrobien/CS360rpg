using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour {

	public string popupName;
	private Canvas popup;
	private Button cancel;
	private bool inTrigger;

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other) {
			popup = GameObject.Find(popupName).GetComponent<Canvas>();
			cancel = popup.GetComponentInChildren<Button>();
			cancel.onClick.AddListener(closePopup);
			if(other.gameObject.tag == "Player"){
			inTrigger = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			inTrigger = false;
		}
	}

	void Update(){
		if(inTrigger){
			if(Input.GetKeyDown(KeyCode.S)){
				TogglePopup();
			}
		}
	}

	private void TogglePopup(){
		popup.enabled = !popup.enabled;
	}
	private void closePopup(){
		popup.enabled = false;
	}
}
