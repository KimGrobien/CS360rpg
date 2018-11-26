using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bountyAnim : MonoBehaviour {

	private SpriteRenderer showMoney;
	public int idx; //Idx of bounty

	void Start() {
		switch (idx) {
			case 0:
				showMoney = GameObject.Find("show_money_0").GetComponent<SpriteRenderer>();
				break;
			case 1:
				showMoney = GameObject.Find("show_money_1").GetComponent<SpriteRenderer>();
				break;
			case 2:
				showMoney = GameObject.Find("show_money_1").GetComponent<SpriteRenderer>();
				break;
		}
	}
	void OnTriggerEnter2D (Collider2D other) {
	    if(GameInfo.bountiesOwed[idx]){
			showMoney.enabled = true;
		}
	}

	void OnTriggerExit2D (Collider2D other){
		showMoney.enabled = false;
	}
}
