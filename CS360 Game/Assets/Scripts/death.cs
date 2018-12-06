using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour {

	public string nameOfNPCObject,nameOfDotsImage,nameWall;
	public int npcID;
	public bool automatic;
	private BoxCollider2D npcInteractBox,npcWallBox,npcDotsBox,wallBox;
	private SpriteRenderer npcImage;
	// Use this for initialization
	void Start () {
		npcInteractBox = GameObject.Find(nameOfNPCObject).GetComponent<BoxCollider2D>();
		if(!automatic){
			npcDotsBox = GameObject.Find(nameOfDotsImage).GetComponent<BoxCollider2D>();
			npcWallBox = GameObject.Find(nameWall).GetComponent<BoxCollider2D>();
		}
		npcImage = GameObject.Find(nameOfNPCObject).GetComponent<SpriteRenderer>();
		if(GameInfo.NPCList[npcID].dead){
		ToggleNPCObjects();
		}
	}
	void ToggleNPCObjects(){
		npcDotsBox.enabled=false;
		npcInteractBox.enabled=false;
		npcWallBox.enabled=false;
		npcImage.enabled=false;
	}
}
