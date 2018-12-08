using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractImageAnimation : MonoBehaviour {
	//reference to gameobject in unity scene
	private SpriteRenderer dots;
	//name of the gameobject in unity set in unity editor
	public string dotsObjName;
	// Use this for initialization
	/// <summary>
    /// The start function is called every time the script is loaded into a scene
	/// In this case it gets all the game objects from the scenes and sets them based on
	/// the string given in the unity editor
    /// </summary>
	void Start(){
		dots = GameObject.Find(dotsObjName).GetComponent<SpriteRenderer>();
	}
	
	/// <summary>
    /// When the player enters the trigger area the dots appear on the screen
    /// </summary>
	void OnTriggerEnter2D (Collider2D other) {
		dots.enabled = true;
	}
	/// <summary>	
    /// When the player exits the trigger area the dots appear on the screen
    /// </summary>
	void OnTriggerExit2D (Collider2D other){
		dots.enabled = false;
	}
}
