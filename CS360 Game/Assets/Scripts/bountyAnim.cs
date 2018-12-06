using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bountyAnim : MonoBehaviour {

	private SpriteRenderer showMoney, interactAnim;
	public int idx; //Idx of bounty
    private bool inTrigger;

	void Start() {
		switch (idx) {
			case 13:
				showMoney = GameObject.Find("show_money_0").GetComponent<SpriteRenderer>();
                interactAnim = GameObject.Find("show_dots_0").GetComponent<SpriteRenderer>();
				break;
			case 12:
				showMoney = GameObject.Find("show_money_1").GetComponent<SpriteRenderer>();
                interactAnim = GameObject.Find("show_dots_1").GetComponent<SpriteRenderer>();
				break;
			case 14:
				showMoney = GameObject.Find("show_money_2").GetComponent<SpriteRenderer>();
                interactAnim = GameObject.Find("show_dots_2").GetComponent<SpriteRenderer>();
				break;
		}
	}
	void OnTriggerEnter2D (Collider2D other) {
	    if(GameInfo.getEquipment(idx).owned){
			showMoney.enabled = true;
            interactAnim.enabled=true;
            inTrigger=true;
		}
	}

    private void Update()
    {
        if (GameInfo.getEquipment(idx).owned && Input.GetKeyDown(KeyCode.S))
        {
            if (idx == 14)
            {
                GameInfo.updateNPCHealth(8, -GameInfo.getNPCMAXHealth(8));
                GameInfo.setNotDead(8);
            }
            else if (idx == 12)
            {
                GameInfo.updateNPCHealth(9, -GameInfo.getNPCMAXHealth(9));
                GameInfo.setNotDead(9);
            }
            else
            {
                GameInfo.updateNPCHealth(10, -GameInfo.getNPCMAXHealth(10));
                GameInfo.setNotDead(10);
            }
            GameInfo.AddMoney(GameInfo.GetPrice(idx));
            GameInfo.setBountyNotOwned(idx);
            showMoney.enabled = false;
            interactAnim.enabled=false;
        }
    }

    void OnTriggerExit2D (Collider2D other){
		showMoney.enabled = false;
        inTrigger=false;
            interactAnim.enabled=false;
	}
}
