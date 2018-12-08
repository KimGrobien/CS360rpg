using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bountyAnim : MonoBehaviour {

    //references to GameObjects in Unity
	private SpriteRenderer showMoney,interactAnim;
    //the names of those objects given in the unity editor for reusability
    public string nameOfShowMoney,nameOfDots;
	public int idx; //Idx of bounty
    //this is used to determine if the player is within the trigger area
    private bool inTrigger;
    /// <summary>
    /// Instantiate all references to GameObjects in unity
    /// </summary>
	void Start() {
				showMoney = GameObject.Find(nameOfShowMoney).GetComponent<SpriteRenderer>();
                interactAnim = GameObject.Find(nameOfDots).GetComponent<SpriteRenderer>();
	}
    /// <summary>
    /// Check to see if the Player owns the bounty item to see if an animation plays
    /// </summary>
	void OnTriggerEnter2D (Collider2D other) {
	    if(GameInfo.getEquipment(idx).owned){
			showMoney.enabled = true;
            interactAnim.enabled=true;
            inTrigger=true;
		}
	}
    /// <summary>
    /// Here we have to differentiate between which house we are interacting with
    /// So that when we redeem a bounty the item is removed from the inventory and
    /// the npc repopulates the bounty scene as well as adjusting the money accordingly
    /// </summary>
    private void Update(){
    
        if(inTrigger){//check if we are in the trigger
        // so that we can check if the item is owned and that the player presses the interact key
            if (GameInfo.getEquipment(idx).owned && Input.GetKeyDown(KeyCode.S))
            {
                //now go through a decision structure and determine which bounty house we are interacting with
                //then update money, bounty owned, and npc.dead and health
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
                //we turn the animations off
                showMoney.enabled = false;
                interactAnim.enabled=false;
            }
        }
    }
    /// <summary>
    /// When the player exits the trigger area the dots go away on the screen 
    /// as well as the animation for the door then set inTrigger as false so we don't listen for key presses
    /// </summary>
    void OnTriggerExit2D (Collider2D other){
		showMoney.enabled = false;
        inTrigger=false;
        interactAnim.enabled=false;
	}
}
