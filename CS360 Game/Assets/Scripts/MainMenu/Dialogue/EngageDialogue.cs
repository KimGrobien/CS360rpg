using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EngageDialogue : MonoBehaviour {
	TextMeshProUGUI npcName,npcResponse;
	Button choice1, choice2, cancel;
	Text txt1, txt2;
	Node[] currentDialogue;
	int indexForNextOption1;
	int indexForNextOption2;
	Sprite npcImage;
  
	public void Start() {
		//Get cancel button
		cancel = GameObject.Find("Cancel").GetComponent<Button>();
		cancel.onClick.AddListener(cancelMenu);
		//Get NPCName Text Data
        npcName = GameObject.Find("NPC_Name").GetComponent<TextMeshProUGUI>();
		//update the image to current npc engaging with
		GameObject.Find("NPC_Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/"+GameInfo.getName(GameInfo.currentNPC));
		
		//Update the name of the npc to current npc engaging with
        npcName.text = GameInfo.getName(GameInfo.currentNPC);//"test"; //GameInfo.getName(GameInfo.currentNPC)
		//get Response game object
		npcResponse = GameObject.Find("Response").GetComponent<TextMeshProUGUI>();
		//get choice button game objects
		choice1 = GameObject.Find("Choice1").GetComponent<Button>();
		choice2 = GameObject.Find("Choice2").GetComponent<Button>();
		//get the text for these objects	
		txt1 = choice1.GetComponentInChildren<Text>();
		txt2 = choice2.GetComponentInChildren<Text>();
		//find the tree that needs to be traversed
		currentDialogue = GameInfo.getDialogueTree(GameInfo.currentNPC);
		//assign base values
		npcResponse.text = currentDialogue[0].response;
		txt1.text = currentDialogue[0].option1;
		txt2.text = currentDialogue[0].option2;
		
		indexForNextOption1 = currentDialogue[0].indexForOption1;
		indexForNextOption2 = currentDialogue[0].indexForOption2;
        choice1.onClick.AddListener(()=>clickedOption1(indexForNextOption1));
        choice2.onClick.AddListener(()=>clickedOption2(indexForNextOption2));
    }
	public void clickedOption1(int index){
		Debug.Log(index);
		if(index==0){
			index++;
		
		npcResponse.text = currentDialogue[index].response;
		txt1.text = currentDialogue[index].option1;
		txt2.text = currentDialogue[index].option2;
		indexForNextOption1 = currentDialogue[index].indexForOption1;
		indexForNextOption2 = currentDialogue[index].indexForOption2;
		index--;

		}
		if(index==3&&npcName.text=="Cynthia"){
			//heal Ego
			GameInfo.UpdateHealth(50);

		}
		npcResponse.text = currentDialogue[index].response;
		txt1.text = currentDialogue[index].option1;
		txt2.text = currentDialogue[index].option2;
		indexForNextOption1 = currentDialogue[index].indexForOption1;
		indexForNextOption2 = currentDialogue[index].indexForOption2;
	}
	public void clickedOption2(int index){
		npcResponse.text = currentDialogue[index].response;
		txt1.text = currentDialogue[index].option1;
		txt2.text = currentDialogue[index].option2;
		indexForNextOption1 = currentDialogue[index].indexForOption1;
		indexForNextOption2 = currentDialogue[index].indexForOption2;


	}
	public void cancelMenu(){
		SceneManager.LoadScene(GameInfo.prevScene);
	}
	public void Update(){

	}

}
