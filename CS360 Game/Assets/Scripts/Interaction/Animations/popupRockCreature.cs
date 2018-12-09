using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupRockCreature : MonoBehaviour {

	private Animator anim;
	// Use this for initialization
	void Start () {
		//Gets the animator for the rock creature
		anim = GameObject.Find("rock creature").GetComponent<Animator>();
	}

	void OnTriggerEnter2D () {
		//When within the area, play the popup animation
		anim.SetBool("popup", true);
	}
}
