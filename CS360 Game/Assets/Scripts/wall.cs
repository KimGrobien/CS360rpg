using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class wall : MonoBehaviour {
	public bool interactable;
	public int id;

	void OnCollisionEnter2D(Collision2D other){
		if(interactable){
			GameInfo.currentNPC = id;
			SceneManager.LoadScene(5);
		}
		
		/*if(interactable){
			EditorUtility.DisplayDialog("Interaction",
                "Interacting with Board!", "OK", "Cancel");
		}*/
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
