using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class wall : MonoBehaviour {
	public bool interactable;

	void OnCollisionEnter2D(Collision2D other){
		if(interactable){
			EditorUtility.DisplayDialog("Bounty Board",
                "Interacting with Bounty Board!", "OK", "Cancel");
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
