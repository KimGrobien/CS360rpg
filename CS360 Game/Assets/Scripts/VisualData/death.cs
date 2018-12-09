using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour {
	//public variables in unity are set in the Unity Editor
	//These are references to each indivudual gameobjects
	//doing it in this fashion allows the code to be reused on any NPC Object
	public string nameOfNPCObject,nameOfDotsImage,nameWall;
	public int npcID;
	public bool automatic;

	//references to gamobjects within the scene
	private BoxCollider2D npcInteractBox,npcWallBox;
	private SpriteRenderer npcImage,npcDotsBox;
	

	/// <summary>
    /// The start function is called every time the script is loaded into a scene
	/// In this case it gets all the game objects from the scene and sets them
	/// Then if any of the NPC objects are labeled as dead then their 
	/// corresponding objects are turned off
    /// </summary>
	void Start () {
		//find the trigger area for the object
		npcInteractBox = GameObject.Find(nameOfNPCObject).GetComponent<BoxCollider2D>();
		
		//if the Objects that need to be turned off are automatic then there will be less objects to get
		//if the objects that need to be turned off are not automatic there will be more
		if(!automatic){//check if the NPCTrigger is automatic
			npcDotsBox = GameObject.Find(nameOfDotsImage).GetComponent<SpriteRenderer>();
			npcWallBox = GameObject.Find(nameWall).GetComponent<BoxCollider2D>();
		}
		//get the sprite for the npc
		npcImage = GameObject.Find(nameOfNPCObject).GetComponent<SpriteRenderer>();
		if(GameInfo.NPCList[npcID].dead){//if the npc is marked as dead then the module toggles the enabled values
		TurnNPCObjectsOff();
		}
	}

	/// <summary>
    /// Turns specified gameobjects on or off depending on what they were previously set
    /// </summary>
	void TurnNPCObjectsOff(){
		if(!automatic){//check if the NPCTrigger is automatic
			npcDotsBox.enabled= false;
			npcWallBox.enabled=false;
		}
		npcInteractBox.enabled=false;
		npcImage.enabled=false;
	}
}
