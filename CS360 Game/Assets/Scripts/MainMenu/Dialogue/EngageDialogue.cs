using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EngageDialogue : MonoBehaviour {
	TextMeshProUGUI npcName,npcResponse;
	Button choice1, choice2, cancel;
	GameObject trade;
	Text txt1, txt2;
	Node[] currentDialogue;
	int indexForNextOption1;
	int indexForNextOption2;
	Sprite npcImage;
	public static string textToScreen,temp1, temp2;
	Coroutine coroutine;
	bool isTyping;
	public int timesEncountered;
  
	public void Start() {
		//Get cancel button
		trade = GameObject.Find("Trade");
		trade.SetActive(false);
		cancel = GameObject.Find("Cancel").GetComponent<Button>();
		cancel.onClick.AddListener(cancelMenu);
		//Get NPCName Text Data
        npcName = GameObject.Find("NPC_Name").GetComponent<TextMeshProUGUI>();
		//update the image to current npc engaging with
		GameObject.Find("NPC_Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/"+GameInfo.getName(GameInfo.currentNPC));
		
		//Update the name of the npc to current npc engaging with
        npcName.text = GameInfo.getName(GameInfo.currentNPC);//
		if(npcName.text == "Anker"){
			trade.SetActive(true);
			trade.GetComponent<Button>().onClick.AddListener(beginTrade);
		}
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
		npcResponse.text = "";
		if(npcName.text=="Cynthia"){
		textToScreen = "You approach a small woman standing next to a garden." 
        +" Her hands are covered in dirt and her forehead in sweat. You can tell she's" 
        +" been working outside all day. She notices you walking up to her. She gives"
        +" you a kind smile. ";
		}
		if(npcName.text=="Anker"){
		textToScreen = "An old man stands before you, broken. Both in body and soul. The look in his eyes says he's given up a long time ago." 
        +" The room is filled with many rare items, and a few common. He's clearly an old adventurer." 
        +" \n\nYou wonder why he's selling his treasures. You also consider that it might be easier just to kill him and take all the loot for yourself.";
		}
		if(npcName.text=="Emrik"){
		textToScreen = "You walk into the gates of a supposed farm. The crop here is wilted and lifeless, and the person you assume is a farmer" 
        +" is standing near the gate looking down the road. It was as if he was expecting someone soon. He was tall and strong, but very unconcerned" 
        +" with his field. If this was the town's only source of food, they were in trouble.";
		}
		if(npcName.text=="Edward"){
		textToScreen = "The hospital is empty save for a doctor standing near empty beds." 
        +" He doesn't seem too concerned with you. He stands in silence, lost in his mind." 
        +" You wonder if you should talk with him at all. Would he even respond?";
		}
		txt1.text = "Talk";
		txt2.text = "Fight";

		//create the listeners and change the text based on which was clicked.
        choice1.onClick.AddListener(()=>clickedOption1(indexForNextOption1));
        choice2.onClick.AddListener(()=>clickedOption2(indexForNextOption2));
		
		coroutine = StartCoroutine(Example());
    }
	public void clickedOption1(int index){
		StopAllCoroutines();
		npcResponse.text="";
		
		if(isTyping){
			if(index==0){
				txt1.text = "Talk";
				txt2.text = "Fight";
			}
			else{
			txt1.text = temp1;
			txt2.text = temp2;
			}
			npcResponse.text = textToScreen;
			isTyping=false;
			return;
		}
		if(index==-1){
			txt1.text="Restart?";
			npcResponse.text="";
			txt2.text="";
			index=0;
			indexForNextOption1=0;
			indexForNextOption2=-1;
			textToScreen = currentDialogue[0].response;
			timesEncountered++;
			return;
		}
		if(index==0){
			index++;
		textToScreen = currentDialogue[index].response;

		//txt1.text = currentDialogue[index].option1;
		//txt2.text = currentDialogue[index].option2;
		indexForNextOption1 = currentDialogue[index].indexForOption1;
		indexForNextOption2 = currentDialogue[index].indexForOption2;
		index--;

		}
		if(index==3&&npcName.text=="Cynthia"){
			//heal Ego
			GameInfo.UpdateHealth(50);
			Debug.Log("Heal Ego");

		}
		textToScreen = currentDialogue[index].response;
		StartCoroutine(Example());
		temp1 = currentDialogue[index].option1;
		temp2 = currentDialogue[index].option2;
		indexForNextOption1 = currentDialogue[index].indexForOption1;
		indexForNextOption2 = currentDialogue[index].indexForOption2;
		
	}
	public void clickedOption2(int index){
		StopAllCoroutines();
		npcResponse.text="";
		
		if(isTyping){
			if(index==0){
				txt1.text = "Talk";
				txt2.text = "Fight";
			}
			else{
			txt1.text = temp1;
			txt2.text = temp2;
			}
			npcResponse.text = textToScreen;
			isTyping=false;
			return;
		}
		if(index==-1){
			txt1.text="Restart?";
			txt2.text="";
			npcResponse.text="";
			index=0;
			indexForNextOption1=0;
			indexForNextOption2=-1;
			textToScreen = currentDialogue[0].response;
			return;
		}
		if(index == 0){
			//this will be the fight option and will change scenes and pass information about who the enemy is
			Debug.Log("Fight Begins");
			//SceneManager.LoadScene("Combat");
			return;
		}
		if(index==3&&npcName.text=="Cynthia"){
			//heal Ego
			GameInfo.UpdateHealth(50);
			Debug.Log("Heal Ego");

		}
		textToScreen = currentDialogue[index].response;
		StartCoroutine(Example());
		temp1 = currentDialogue[index].option1;
		temp2 = currentDialogue[index].option2;
		indexForNextOption1 = currentDialogue[index].indexForOption1;
		indexForNextOption2 = currentDialogue[index].indexForOption2;

	}
	public void cancelMenu(){
		SceneManager.LoadScene(GameInfo.prevScene);
	}
	public void beginTrade(){
		//make all the left side of the menu interactable
		Debug.Log("BEGINNING TRADE");
	}

	public void Update(){

	}
IEnumerator Example()
    {
		isTyping=true;
		txt1.text="";
		txt2.text="";
		foreach (char letter in textToScreen.ToCharArray()) {
             npcResponse.text += letter;
             yield return new WaitForSeconds ((float).02);
         }
		 if(timesEncountered==0){
			 txt1.text = "Talk";
			txt2.text = "Fight";
		 }
		 else{
		 txt1.text = temp1;
		 txt2.text = temp2;
		 }
		 isTyping=false;
	}
}
