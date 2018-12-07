using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bountyAnim : MonoBehaviour {

	private SpriteRenderer showMoney,interactAnim;
    public string nameOfShowMoney,nameOfDots;
	public int idx; //Idx of bounty
    private bool inTrigger;

	void Start() {
				showMoney = GameObject.Find(nameOfShowMoney).GetComponent<SpriteRenderer>();
                interactAnim = GameObject.Find(nameOfDots).GetComponent<SpriteRenderer>();
	}
	void OnTriggerEnter2D (Collider2D other) {
	    if(GameInfo.getEquipment(idx).owned){
			showMoney.enabled = true;
            interactAnim.enabled=true;
            inTrigger=true;
		}
	}

    private void Update(){
    
        if(inTrigger){
            if (GameInfo.getEquipment(idx).owned && Input.GetKeyDown(KeyCode.S))
            {
                if (idx == 14&&nameOfShowMoney=="show_money_0")
                {
                    GameInfo.updateNPCHealth(8, -GameInfo.getNPCMAXHealth(8));
                    GameInfo.setNotDead(8);
                    GameInfo.AddMoney(GameInfo.GetPrice(idx));
                    GameInfo.setBountyNotOwned(idx);
                }
                else if (idx == 12&&nameOfShowMoney=="show_money_1")
                {
                    GameInfo.updateNPCHealth(9, -GameInfo.getNPCMAXHealth(9));
                    GameInfo.setNotDead(9);
                    GameInfo.AddMoney(GameInfo.GetPrice(idx));
                    GameInfo.setBountyNotOwned(idx);
                }
                else
                {
                    GameInfo.updateNPCHealth(10, -GameInfo.getNPCMAXHealth(10));
                    GameInfo.setNotDead(10);
                    GameInfo.AddMoney(GameInfo.GetPrice(idx));
                    GameInfo.setBountyNotOwned(idx);
                }
                
                showMoney.enabled = false;
                interactAnim.enabled=false;
            }
        }
    }

    void OnTriggerExit2D (Collider2D other){
		showMoney.enabled = false;
        inTrigger=false;
        interactAnim.enabled=false;
	}
}
