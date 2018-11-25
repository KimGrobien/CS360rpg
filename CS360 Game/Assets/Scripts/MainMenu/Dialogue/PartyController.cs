using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PartyController : MonoBehaviour {
	Button choice1, choice2, cancel;
	Text txt1, txt2;
	static string textToScreen,temp1, temp2;
	TextMeshProUGUI npcResponse;
	
	bool isTyping;
	PartySlot newPartyMember;


	
	// Use this for initialization
	void Start () {
		//Get cancel button
		cancel = GameObject.Find("Cancel").GetComponent<Button>();
		cancel.onClick.AddListener(cancelMenu);
		//update the image to current npc engaging with

		//get Response game object
		npcResponse = GameObject.Find("Response").GetComponent<TextMeshProUGUI>();
		//get choice button game objects
		choice1 = GameObject.Find("Choice1").GetComponent<Button>();
		choice2 = GameObject.Find("Choice2").GetComponent<Button>();
		//get the text for these objects	
		txt1 = choice1.GetComponentInChildren<Text>();
		txt2 = choice2.GetComponentInChildren<Text>();
		//assign base values


		//create the listeners and change the text based on which was clicked.
		
	}
	public void addToParty(PartySlot newPartyMember){
		this.newPartyMember = newPartyMember;
		
        choice1.onClick.AddListener(AddToSlot1);
        choice2.onClick.AddListener(AddToSlot2);
		cancel.onClick.AddListener(cancelMenu);
		textToScreen = "Which Slot would you like to add "+GameInfo.getName(GameInfo.currentNPC);
		temp1 = "Slot 1";
		temp2 = "Slot 2";
		StartCoroutine(type());


	}

	public void AddToSlot1(){
		StopAllCoroutines();
		if(isTyping){
			npcResponse.text = textToScreen;
			isTyping=false;
			return;
		}
		GameObject.Find("EgoPartyImage1").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/"+GameInfo.getName(GameInfo.currentNPC));
		GameInfo.party[0] = newPartyMember;
		afterAdding();
	}

	public void AddToSlot2(){
		StopAllCoroutines();
		if(isTyping){
			npcResponse.text = textToScreen;
			isTyping=false;
			return;
		}
		GameObject.Find("EgoPartyImage2").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/"+GameInfo.getName(GameInfo.currentNPC));
		GameInfo.party[1] = newPartyMember;
		afterAdding();
	}
	
	public void cancelMenu(){
		SceneManager.LoadScene(GameInfo.prevScene);
	}

	IEnumerator type()
    {
		isTyping=true;
		txt1.text="";
		txt2.text="";
		foreach (char letter in textToScreen.ToCharArray()) {
             npcResponse.text += letter;
             yield return new WaitForSeconds ((float).02);
         }
		 	txt1.text = temp1;
		 	txt2.text = temp2;
		 
		 isTyping=false;
	}

	public void afterAdding(){
		textToScreen = GameInfo.getName(GameInfo.currentNPC) + " is now added to your party!";
		temp1 = "Leave";
		temp2 = "";
		choice1.onClick.RemoveListener(AddToSlot1);
		choice2.onClick.RemoveListener(AddToSlot2);
		choice1.onClick.AddListener(cancelMenu);
		StartCoroutine(type());
	}
}

