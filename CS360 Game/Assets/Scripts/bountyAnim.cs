using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bountyAnim : MonoBehaviour {

	private SpriteRenderer showMoney;

	void Start() {
		showMoney = GameObject.Find("show_money").GetComponent<SpriteRenderer>();
	}
	void OnTriggerEnter2D (Collider2D other) {
        if(GameInfo.bountyOwed > 0){
			showMoney.enabled = true;
		}
	}

	void OnTriggerExit2D (Collider2D other){
		showMoney.enabled = false;
	}
}
