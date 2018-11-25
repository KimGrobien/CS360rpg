using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupRockCreature : MonoBehaviour {

	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GameObject.Find("rock_creature").GetComponent<Animator>();
	}

	void OnCollisionEnter2D (Collision2D other) {
		anim.SetBool("popup", true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
