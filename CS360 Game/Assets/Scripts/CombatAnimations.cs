﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimations : MonoBehaviour {

	private Animator anim;
	void Start () {
		anim = GameObject.Find("EnemyImage").GetComponent<Animator>();
		Debug.Log(anim);
		anim.SetInteger("id", GameInfo.currentNPC);
	}
}