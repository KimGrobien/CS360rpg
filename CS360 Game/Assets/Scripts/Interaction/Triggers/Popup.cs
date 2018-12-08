using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour {

	//references for gameobjects in unity
	private Canvas popup;
	private Button cancel;
	//name of the gameobjects given in the unity editor
	public string popupName;
	//so we know if we are within the trigger
	private bool inTrigger;

	/// <summary>
    /// When the player enters the trigger area find the objects that we need to enable
	/// and let Update() know the player is within the trigger area
    /// </summary>
	void OnTriggerEnter2D (Collider2D other) {
			popup = GameObject.Find(popupName).GetComponent<Canvas>();
			//the cancel button is for the top right button to close the popup
			cancel = popup.GetComponentInChildren<Button>();
			cancel.onClick.AddListener(closePopup);
			if(other.gameObject.tag == "Player"){
			inTrigger = true;
		}
	}
	/// <summary>
    /// When the player exits the trigger area
    /// </summary>
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			inTrigger = false;
		}
	}
	/// <summary>
    /// When the player is within the trigger area we listen for the key press
	/// then toggle the popup being on screen or not
    /// </summary>
	void Update(){
		if(inTrigger){
			if(Input.GetKeyDown(KeyCode.S)){
				TogglePopup();
			}
		}
	}
	/// <summary>
    /// turn the gameobject on and off
    /// </summary>
	private void TogglePopup(){
		popup.enabled = !popup.enabled;
	}
	/// <summary>
    /// Listener for the button to close the popup
    /// </summary>
	private void closePopup(){
		popup.enabled = false;
	}
}
