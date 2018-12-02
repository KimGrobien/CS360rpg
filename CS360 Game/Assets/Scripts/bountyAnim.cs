using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bountyAnim : MonoBehaviour {

	private SpriteRenderer showMoney;
	public int idx; //Idx of bounty

	void Start() {
		switch (idx) {
			case 13:
				showMoney = GameObject.Find("show_money_0").GetComponent<SpriteRenderer>();
				break;
			case 12:
				showMoney = GameObject.Find("show_money_1").GetComponent<SpriteRenderer>();
				break;
			case 14:
				showMoney = GameObject.Find("show_money_2").GetComponent<SpriteRenderer>();
				break;
		}
	}
	void OnTriggerEnter2D (Collider2D other) {
	    if(GameInfo.getEquipment(idx).owned){
			showMoney.enabled = true;
		}
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameInfo.getEquipment(idx).owned && Input.GetKeyDown(KeyCode.S))
        {
            GameInfo.AddMoney(GameInfo.GetPrice(idx));
            GameInfo.setBountyNotOwned(idx);
            showMoney.enabled = false;
        }
    }

    void OnTriggerExit2D (Collider2D other){
		showMoney.enabled = false;
	}
}
