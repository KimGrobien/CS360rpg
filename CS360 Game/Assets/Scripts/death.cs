using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour {

	public string nameOfNPCObject;
	public string nameOfDotsImage;
	public int npcID;
	// Use this for initialization
	void Start () {
		if(GameInfo.NPCList[npcID].dead){
			
					GameObject.Find(nameOfNPCObject).SetActive(false);
					GameObject.Find(nameOfDotsImage).SetActive(false);
		}

	}
	
}
