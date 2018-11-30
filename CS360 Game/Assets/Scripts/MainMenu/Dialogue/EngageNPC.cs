using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EngageNPC : MonoBehaviour {

	TextMeshProUGUI npcName,npcResponse;
	Button choice1, choice2, cancel;
	GameObject trade;
	Text txt1, txt2;
	Node[] currentDialogue;
	int indexForNextOption1, indexForNextOption2;
	Sprite npcImage;
	string textToScreen,temp1, temp2;
	bool isTyping;
	int restarts;
	PartySlot PartyMember;
    InventoryController control;

    public void Start() {

        // Kurt added this so buttons are not interactable
        Button primaryButton = GameObject.Find("PrimaryB").GetComponent<Button>();
        Button secondaryButton = GameObject.Find("SecondaryB").GetComponent<Button>();
        Button defenseButton = GameObject.Find("DefenseB").GetComponent<Button>();
        Button buyButton = GameObject.Find("BuyB").GetComponent<Button>();

        // KURT MOVED THIS UP HERE
        if (GameInfo.party[0].isAssigned)
        {
            GameObject.Find("EgoPartyImage1").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/" + GameInfo.party[0].npc.name);
            GameObject.Find("PartyName0").GetComponent<TextMeshProUGUI>().text = GameInfo.party[0].npc.name;
        }
        if (GameInfo.party[1].isAssigned)
        {
            GameObject.Find("EgoPartyImage2").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/" + GameInfo.party[1].npc.name);
            GameObject.Find("PartyName1").GetComponent<TextMeshProUGUI>().text = GameInfo.party[1].npc.name;
        }
        ////////////////////////////

        if (GameInfo.currentNPC == -1){//Not interacting with NPC so dialogue should be invisible
            primaryButton.interactable = false;
            secondaryButton.interactable = false;
            buyButton.interactable = false;
            defenseButton.interactable = false;
            GameObject.Find("Inventory").SetActive(true);
            GameObject.Find("Dialogue").SetActive(false);
			return;
		}
		//Get button objects
		trade = GameObject.Find("Trade");
		choice1 = GameObject.Find("Choice1").GetComponent<Button>();
		choice2 = GameObject.Find("Choice2").GetComponent<Button>();
		trade.SetActive(false); //this one needs to be invisible at first, and set visible if the NPC is the shopkeeper
			if(GameInfo.currentNPC==1){
				trade.SetActive(true);
				trade.GetComponent<Button>().onClick.AddListener(beginTrade);
			}
		cancel = GameObject.Find("Cancel").GetComponent<Button>();
		cancel.onClick.AddListener(cancelMenu);

		//Get NPC Text Areas: Name and Response
        npcName = GameObject.Find("NPC_Name").GetComponent<TextMeshProUGUI>();
		npcResponse = GameObject.Find("Response").GetComponent<TextMeshProUGUI>();
		//get Player Choice text areas	
		txt1 = choice1.GetComponentInChildren<Text>();
		txt2 = choice2.GetComponentInChildren<Text>();

        primaryButton.interactable = false;
        secondaryButton.interactable = false;
        buyButton.interactable = false;
        defenseButton.interactable = false;

        //Set the name of the NPC
        if (GameInfo.currentNPC==-1){
			npcName.text = "";
		}
		else{
			npcName.text=GameInfo.getName(GameInfo.currentNPC);
		}
		//update the image to current npc engaging with
		if(GameInfo.currentNPC==-1){
			GameObject.Find("NPC_Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Empty");
		}
		else{
		GameObject.Find("NPC_Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/"+GameInfo.getName(GameInfo.currentNPC));
		}

		//find the tree that needs to be traversed
		if(GameInfo.currentNPC!=-1){
 			currentDialogue = GameInfo.getDialogueTree(GameInfo.currentNPC);
		}
		
		//assign base values
		textToScreen=LoadDialogue.SetBaseNPCResponses();
		setTextColor();

		//create the listeners and change the text based on which was clicked.
		if(GameInfo.currentNPC!=-1){
        choice1.onClick.AddListener(()=>clickedOption1(indexForNextOption1));
        choice2.onClick.AddListener(()=>clickedOption2(indexForNextOption2));
		
		
		++GameInfo.encountered[GameInfo.currentNPC];
		//check if npc is being encountered a lot
        if(GameInfo.encountered[GameInfo.currentNPC] > 3){
			textToScreen = LoadDialogue.setNPCResponseIfEncounteredAlot();
		}
		//check if npc is recruitable
		if(GameInfo.recruitable[GameInfo.currentNPC]){
			textToScreen = LoadDialogue.setNPCResponseIfRecruitable();
		}
		//check if npc is on the team
		if((GameInfo.party[0].npc.name==GameInfo.getName(GameInfo.currentNPC))||(GameInfo.party[1].npc.name==GameInfo.getName(GameInfo.currentNPC))){
			textToScreen = LoadDialogue.setNPCResponseIfOnTeam();
		}

		StartCoroutine(type());
		}
    }
	
	public void clickedOption1(int index){
		StopAllCoroutines();
		npcResponse.text="";
		if(GameInfo.recruitable[GameInfo.currentNPC]&&GameInfo.encountered[GameInfo.currentNPC]>3
		|| (GameInfo.party[0].npc.name==GameInfo.getName(GameInfo.currentNPC))||(GameInfo.party[1].npc.name==GameInfo.getName(GameInfo.currentNPC))){
			npcResponse.text = textToScreen;
			indexForNextOption1=-2;
			indexForNextOption2=0;
			choice1.onClick.RemoveAllListeners();
			choice1.onClick.AddListener(cancelMenu);
			txt1.text = "Leave";
			txt2.text = "Fight";
					return;
		}
		if(index==0){
				txt1.text = "Talk";
				txt2.text = "Fight";
			}
		if(index==-3){
			GameInfo.recruitable[GameInfo.currentNPC] = true;
		}
		if(index==-4){
			npcResponse.text = textToScreen;
			addToParty();
			indexForNextOption1=-2;
			indexForNextOption2=0;
			return;
		}
		
		if(GameInfo.recruitable[GameInfo.currentNPC]){
			npcResponse.text = textToScreen;
			indexForNextOption1=-4;
			indexForNextOption2=0;
			txt1.text = "Add to Party";
			txt2.text = "";
					return;
		}
		if(GameInfo.encountered[GameInfo.currentNPC]>3){
			npcResponse.text = textToScreen;
			indexForNextOption1=0;
			indexForNextOption2=0;
			txt1.text = "Talk";
			txt2.text = "Fight";
					return;
		}
		
		if(isTyping){
			if(checkIfInParty()){
			txt1.text = "Leave";
			txt2.text = "Fight";
			
			indexForNextOption1=-2;
			indexForNextOption2=0;
			choice1.onClick.RemoveAllListeners();
			choice1.onClick.AddListener(cancelMenu);
			}
			else if(indexForNextOption1==0&&indexForNextOption2==0){
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
			indexForNextOption2=0;
			textToScreen = currentDialogue[0].response;
			restarts++;
			return;
		}
		if(index==0){
			index++;
			textToScreen = currentDialogue[index].response;

			indexForNextOption1 = currentDialogue[index].indexForOption1;
			indexForNextOption2 = currentDialogue[index].indexForOption2;
			index--;

		}
		if((index==3 || index == 12)&& GameInfo.currentNPC==0){
			//heal Ego
			GameInfo.UpdateHealth(50);
			Debug.Log("Heal Ego");

		}
		if((index==-2)){
			//Load into the overworld
			SceneManager.LoadScene(GameInfo.prevScene);

		}
		textToScreen = currentDialogue[index].response;
		StartCoroutine(type());
		temp1 = currentDialogue[index].option1;
		temp2 = currentDialogue[index].option2;
		indexForNextOption1 = currentDialogue[index].indexForOption1;
		indexForNextOption2 = currentDialogue[index].indexForOption2;
		return;
	}

	public void clickedOption2(int index){
		StopAllCoroutines();
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
		if(index == 0){
			npcResponse.text = textToScreen;
			
			txt1.text = temp1;
			txt2.text = temp2;
			//this will be the fight option and will change scenes and pass information about who the enemy is
			Debug.Log("Fight Begins");
			SceneManager.LoadScene("Combat");
			return;
		}
	
		npcResponse.text="";
		if(index==-3){
			GameInfo.recruitable[GameInfo.currentNPC] = true;
		}
		if(index==-4){
			npcResponse.text = "I can go with you.";
			addToParty();
			indexForNextOption1=-2;
			indexForNextOption2=0;
			return;
		}
		if(GameInfo.recruitable[GameInfo.currentNPC]&&GameInfo.encountered[GameInfo.currentNPC]>3
		&& (GameInfo.party[0].npc.name==GameInfo.getName(GameInfo.currentNPC))||(GameInfo.party[1].npc.name==GameInfo.getName(GameInfo.currentNPC))){
			npcResponse.text = textToScreen;
			indexForNextOption1=-4;
			indexForNextOption2=0;
			txt1.text = "Add to Party";
			txt2.text = "Fight";
					return;
		}
		if(GameInfo.recruitable[GameInfo.currentNPC]){
			npcResponse.text = "I can go with you.";
			indexForNextOption1=-4;
			indexForNextOption2=0;
			txt1.text = "Add to Party";
			txt2.text = "";
					return;
		}
		if(GameInfo.encountered[GameInfo.currentNPC]>3){
			npcResponse.text = textToScreen;
			indexForNextOption1=0;
			indexForNextOption2=0;
			txt1.text = "Talk";
			txt2.text = "Fight";
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
			restarts++;
			return;
		}
		if(index==0){
			index++;
			textToScreen = currentDialogue[index].response;

			indexForNextOption1 = currentDialogue[index].indexForOption1;
			indexForNextOption2 = currentDialogue[index].indexForOption2;
			index--;

		}
		if((index==3 || index == 12)&& GameInfo.currentNPC==0){
			//heal Ego
			GameInfo.UpdateHealth(50);
			Debug.Log("Heal Ego");

		}
		if((index==-2)){
			//Load into the overworld
			SceneManager.LoadScene(GameInfo.prevScene);

		}
		textToScreen = currentDialogue[index].response;
		StartCoroutine(type());
		temp1 = currentDialogue[index].option1;
		temp2 = currentDialogue[index].option2;
		indexForNextOption1 = currentDialogue[index].indexForOption1;
		indexForNextOption2 = currentDialogue[index].indexForOption2;
		return;
	}

    public void cancelMenu(){
		SceneManager.LoadScene(GameInfo.prevScene);
	}

    public void beginTrade(){
		//make all the left side of the menu interactable
		Debug.Log("BEGINNING TRADE");
        //InventoryController.toggleBuyingMode();
	}

/*
This is a coroutine and runnings alongside other functions.
It's use is for the typing effect of the character response.
Basically it sets a variable that tells all of the program that it is typing.
Then sets the choices to blank text so the player cannot see what the new options are until
the NPC is finished responding.
There is the case that it is the first encounter, in which case the choices will be to talk or to fight, otherwise
the temp1 and temp2 are saved strings from the dialogue tree. When it is done it set's isTyping to false
 */
IEnumerator type()
    {

		isTyping=true;
		txt1.text="";
		txt2.text="";
		npcResponse.text="";
		foreach (char letter in textToScreen.ToCharArray()) {
             npcResponse.text += letter;
             yield return new WaitForSeconds ((float).01);
         }
		 if(checkIfInParty()){
			 
			txt1.text = "Leave";
			txt2.text = "Fight";
			
			choice1.onClick.RemoveAllListeners();
			choice1.onClick.AddListener(cancelMenu);
		 }
		 else if(indexForNextOption1==0&&indexForNextOption2==0){
			txt1.text = "Talk";
			txt2.text = "Fight";
		 }
		 else{
		 	txt1.text = temp1;
		 	txt2.text = temp2;
		 }
		 isTyping=false;
	}

	public bool checkIfInParty(){
		return(GameInfo.party[0].npc.name==GameInfo.getName(GameInfo.currentNPC))||(GameInfo.party[1].npc.name==GameInfo.getName(GameInfo.currentNPC));
	}

	public void setTextColor(){
		if(GameInfo.currentNPC==0){
			npcResponse.faceColor= new Color32(255, 240, 127,255);
		}
		if(GameInfo.currentNPC==1){
		npcResponse.faceColor= new Color32(183, 189, 255,255);
		}
		if(GameInfo.currentNPC==3){
			npcResponse.faceColor= new Color32(182, 255, 170,255);
		}
		if(GameInfo.currentNPC==2){		
			npcResponse.faceColor= new Color32(255, 84, 84,255);
		}
		if(GameInfo.currentNPC==4){		
			npcResponse.faceColor= new Color32(130,130,130,255);
		}
		if(GameInfo.currentNPC==5 || GameInfo.currentNPC==6){		
			npcResponse.faceColor= new Color32(130,130,130,255);
		}
	}

	public void addToParty(){
		StopAllCoroutines();
		PartyMember = GameInfo.potentialNPC[GameInfo.currentNPC];
		if((PartyMember.npc.name == GameInfo.party[0].npc.name) || (PartyMember.npc.name == GameInfo.party[1].npc.name)){
			alreadyAdded();
			return;
		}
		choice1.onClick.RemoveAllListeners();
		choice2.onClick.RemoveAllListeners();
        choice1.onClick.AddListener(AddToSlot1);
        choice2.onClick.AddListener(AddToSlot2);
		npcResponse.text = "Which Slot would you like to add "+GameInfo.getName(GameInfo.currentNPC)+"?";
		txt1.text = "Slot 1";
		txt2.text = "Slot 2";
	}

	public void alreadyAdded(){
		choice1.onClick.RemoveAllListeners();
		choice2.onClick.RemoveAllListeners();
        choice1.onClick.AddListener(cancelMenu);
        choice2.onClick.AddListener(doNothing);
		npcResponse.text = GameInfo.getName(GameInfo.currentNPC)+" is already a part of the team.";
		txt1.text = "Leave";
		txt2.text = "";
		return;
	}

	public void doNothing(){
		choice2.interactable=false;
	}

	public void AddToSlot1(){
	
		GameObject.Find("EgoPartyImage1").GetComponent<Image>().sprite =
		 Resources.Load<Sprite>("DialogueImages/"+GameInfo.getName(GameInfo.currentNPC));
        PartyMember.isAssigned=true;
		GameInfo.party[0] = PartyMember;
        GameObject.Find("PartyName0").GetComponent<TextMeshProUGUI>().text = GameInfo.party[0].npc.name;
        //Added to gift item - Kurt
        int giftID = GameInfo.party[0].npc.giftItemID;
        if(giftID > 0)
        {
            GameInfo.setEquipmentOwned(giftID);
            GameInfo.setEquipmentColor(giftID, Color.white);
            Button Gift = GameObject.Find("slot" + giftID).GetComponent<Button>();
            Gift.image.color = GameInfo.getEquipment(giftID).Visability;
        }
        afterAdding();
    }

	public void AddToSlot2(){

		GameObject.Find("EgoPartyImage2").GetComponent<Image>().sprite =
		 Resources.Load<Sprite>("DialogueImages/"+GameInfo.getName(GameInfo.currentNPC));
        PartyMember.isAssigned=true;
		GameInfo.party[1] = PartyMember;
        GameObject.Find("PartyName1").GetComponent<TextMeshProUGUI>().text = GameInfo.party[1].npc.name;
        //Added to gift item - Kurt
        int giftID = GameInfo.party[1].npc.giftItemID;
        if (giftID > 0)
        {
            GameInfo.setEquipmentOwned(giftID);
            GameInfo.setEquipmentColor(giftID, Color.white);
            Button Gift = GameObject.Find("slot" + giftID).GetComponent<Button>();
            Gift.image.color = GameInfo.getEquipment(giftID).Visability;
        }
        afterAdding();
	}
	
	public void afterAdding(){
		int i = 0;
		if(GameInfo.currentNPC==0){
			i=11;
		}
		if(GameInfo.currentNPC==2){
			i=3;
		}
		if(GameInfo.currentNPC==3){
			i=7;
		}
		npcResponse.text = GameInfo.getName(GameInfo.currentNPC) + " is now added to your party!"
		+" You received "+GameInfo.equipmentStrings[i, 0]+" as a gift!";
		txt1.text = "Leave";
		txt2.text = "";
		
		choice1.onClick.RemoveListener(AddToSlot1);
		choice2.onClick.RemoveListener(AddToSlot2);
		choice1.onClick.AddListener(cancelMenu);
	}
	public void displayAllText(){
		npcResponse.text = textToScreen;
		txt1.text = temp1;
		txt2.text = temp2;
	}
}

