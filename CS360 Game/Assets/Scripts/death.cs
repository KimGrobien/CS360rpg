using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour {

	string NPCObjectName;

	// Use this for initialization
	void Start () {
		//if the npc is killed they don't come back
	for(int i = 0;i<GameInfo.NPCList.Length;i++){
            if(GameInfo.NPCList[i].dead){
                if(GameInfo.NPCList[0].dead){
					GameObject.Find("cynthia").SetActive(false);
					GameObject.Find("InteractImage").SetActive(false);
                }
                if(GameInfo.NPCList[1].dead){
					GameObject.Find("Shopkeeper").SetActive(false);
					GameObject.Find("InteractImage").SetActive(false);                }
                if(GameInfo.NPCList[2].dead){
					GameObject.Find("edward").SetActive(false);
					GameObject.Find("InteractImage").SetActive(false);
                }
                if(GameInfo.NPCList[3].dead){
					GameObject.Find("Farmer").SetActive(false);
					GameObject.Find("InteractImage").SetActive(false);
                }
                if(GameInfo.NPCList[4].dead){
					GameObject.Find("berndy").SetActive(false);
					GameObject.Find("InteractImage").SetActive(false);
                }
                if(GameInfo.NPCList[5].dead){
					GameObject.Find("Modir").SetActive(false);            }
                if(GameInfo.NPCList[6].dead){
					GameObject.Find("Farenvir").SetActive(false);
                }
				//bounties will return if BOUNTY ITEM is not in inventory
				if(true){//TODO: check if item is in inventory
					if(GameInfo.NPCList[8].dead){
						GameObject.Find("Fox").SetActive(false);
						GameObject.Find("InteractImage").SetActive(false);
					}
					if(GameInfo.NPCList[9].dead){
						GameObject.Find("rock creature").SetActive(false);
						GameObject.Find("InteractImage2").SetActive(false);                }
					if(GameInfo.NPCList[10].dead){
						GameObject.Find("rabbit").SetActive(false);
						GameObject.Find("InteractImage1").SetActive(false);
					}
				}
				else{
					if(!GameInfo.NPCList[8].dead){
						GameObject.Find("fox").SetActive(true);
						GameObject.Find("InteractImage").SetActive(true);
					}
					if(!GameInfo.NPCList[9].dead){
						GameObject.Find("rock_creature").SetActive(true);
						GameObject.Find("InteractImage2").SetActive(true);                }
					if(!GameInfo.NPCList[10].dead){
						GameObject.Find("rabbit").SetActive(true);
						GameObject.Find("InteractImage1").SetActive(true);
					}

				}
            }
	 	}	
	}
	
}
