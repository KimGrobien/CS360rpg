using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimations : MonoBehaviour {

	private Animator anim;
	void Start () {
		//Finds the animator component for the enemy
		anim = GameObject.Find("EnemyImage").GetComponent<Animator>();
		//Tells the animator which enemy is being fought which triggers the appropriate animations
		anim.SetInteger("id", GameInfo.currentNPC);
	}
}
