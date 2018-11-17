using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class wall : MonoBehaviour {
	public bool interactable;
	public int id;
	private bool touching;
	void OnCollisionEnter2D(Collision2D other){
		touching = true;
		if(interactable){
			GameInfo.currentNPC = id;
		}
	}
	void OnCollisionExit2D(){
		touching = false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(touching){
			if(Input.GetKeyDown(KeyCode.I)){
				GameInfo.prevScene = SceneManager.GetActiveScene().buildIndex;
				SceneManager.LoadScene("Menu");
			}
		}
	}
}
