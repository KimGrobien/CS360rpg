using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class wall : MonoBehaviour {
	public bool interactable;
	public string popupName;
	public int id;
	private bool touching;
	private Canvas popup;
	private Button cancel;

	// Use this for initialization
	void Start () {
		if(popupName != ""){
			popup = GameObject.Find(popupName).GetComponent<Canvas>();
			cancel = popup.GetComponentInChildren<Button>();
			cancel.onClick.AddListener(closePopup);
		}
	}
	void OnCollisionEnter2D(Collision2D other){
		touching = true;
		if(interactable){
			GameInfo.currentNPC = id;
			GameInfo.prevPos = other.transform.position;
		}
		if(popupName != ""){
			popup.enabled = true;
		}
	}
	void OnCollisionExit2D(){
		touching = false;
		GameInfo.currentNPC = -1; //No longer within range of NPC
	}
	
	// Update is called once per frame
	void Update () {
		if(touching && interactable){
			if(Input.GetKeyDown(KeyCode.I)){
				GameInfo.prevScene = SceneManager.GetActiveScene().buildIndex;
				SceneManager.LoadScene("Menu");
			}
		}
	}

	private void closePopup(){
		Debug.Log("REACHED");
		popup.enabled = false;
	}
}
