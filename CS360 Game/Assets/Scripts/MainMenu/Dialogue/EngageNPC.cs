using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EngageNPC : MonoBehaviour {

	//GameObject references in Unity editor
	TextMeshProUGUI npcName,npcResponse;
	Button choice1, choice2, cancel;
	GameObject trade;
	Text txt1, txt2;//the text objects associated with the choice1 and choice2 buttons
	Sprite npcImage;
	
    // Used to display details about NPC's when recruitting
    private TextMeshProUGUI NPCNameDisplay, NPCDetailsDisplay;
    private Image NPCimageDetails;

	//the currentDialogue tree that will be traversed
	Node[] currentDialogue;
	//the indexes for the options that come after each response
	int indexForNextOption1, indexForNextOption2;
	
	//temporary holders for the typing effect
	string textToScreen,temp1, temp2;
	
	//used for knowing when the typing affect is happening
	bool isTyping;

	//data to keep track of the number of restarts in a interaction
	int restarts;

	//if the NPC gets recruited this is the object that gets passed to the GameInfo.party
	PartySlot PartyMember;
	
	/// <summary>
    /// The start function is called every time the script is loaded into a scene
	/// In this case it gets all the game objects from the scenes and sets them based on
	/// Which NPC you're talking to, if you've encountered them a lot,
	/// if they're in the party, if they're recruitable, or if we're opening the menu
	/// while not interacting with an NPC
    /// </summary>
    public void Start() {

        // Kurt added this so buttons are not interactable
		// because we need to check if we are talking to the Shopkeeper
		// but we still need these references
        Button primaryButton = GameObject.Find("PrimaryB").GetComponent<Button>();
        Button secondaryButton = GameObject.Find("SecondaryB").GetComponent<Button>();
        Button defenseButton = GameObject.Find("DefenseB").GetComponent<Button>();
        Button buyButton = GameObject.Find("BuyB").GetComponent<Button>();

        // This decision structure checks which NPC's are on the team if any and updates the corresponding
		// slot images
        if (GameInfo.party[0].isAssigned)//checks party slot 1
        {
            GameObject.Find("EgoPartyImage1").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/" + GameInfo.party[0].npc.name);
            GameObject.Find("PartyName0").GetComponent<TextMeshProUGUI>().text = GameInfo.party[0].npc.name;
        }
        if (GameInfo.party[1].isAssigned)//checks party slot 2
        {
            GameObject.Find("EgoPartyImage2").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/" + GameInfo.party[1].npc.name);
            GameObject.Find("PartyName1").GetComponent<TextMeshProUGUI>().text = GameInfo.party[1].npc.name;
        }

		// now check if we are interacting with an NPC
        if (GameInfo.currentNPC == -1){//Not interacting with NPC so dialogue side should be invisible
            primaryButton.interactable = false;
            secondaryButton.interactable = false;
            buyButton.interactable = false;
            defenseButton.interactable = false;
            GameObject.Find("Inventory").SetActive(true);
            GameObject.Find("Dialogue").SetActive(false);
            return;
		}

		//Get button objects for the whole scene
		trade = GameObject.Find("Trade");
		choice1 = GameObject.Find("Choice1").GetComponent<Button>();
		choice2 = GameObject.Find("Choice2").GetComponent<Button>();
		trade.SetActive(false); //this one needs to be invisible at first, and set visible if the NPC is the shopkeeper
		if(GameInfo.currentNPC==1){
			trade.SetActive(true);
			trade.GetComponent<Button>().onClick.AddListener(beginTrade);
		}
		cancel = GameObject.Find("Cancel").GetComponent<Button>();
		//add listener for the cancel button
		cancel.onClick.AddListener(cancelMenu);

		//Get NPC Text Areas: Name and Response
        npcName = GameObject.Find("NPC_Name").GetComponent<TextMeshProUGUI>();
		npcResponse = GameObject.Find("Response").GetComponent<TextMeshProUGUI>();
		//get Player Choice text areas	
		txt1 = choice1.GetComponentInChildren<Text>();
		txt2 = choice2.GetComponentInChildren<Text>();

		//here we need to set the inventory buttons false again, even if we are interacting with an NPC
        primaryButton.interactable = false;
        secondaryButton.interactable = false;
        buyButton.interactable = false;
        defenseButton.interactable = false;

        //Set the name of the NPC if we are interacting with an NPC
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
		
		//assign base values for initial response and textColor
		textToScreen=LoadDialogue.SetBaseNPCResponses();
		setTextColor();

		//create the listeners for the buttons
		if(GameInfo.currentNPC!=-1){
        choice1.onClick.AddListener(()=>clickedOption1(indexForNextOption1));
        choice2.onClick.AddListener(()=>clickedOption2(indexForNextOption2));
		
		//this keeps track of how many times the player has interacted with the NPC
		++GameInfo.encountered[GameInfo.currentNPC];
		//check if npc is being encountered a lot
        if(GameInfo.encountered[GameInfo.currentNPC] > 3){
			textToScreen = LoadDialogue.setNPCResponseIfEncounteredAlot();
		}
		//check if npc is recruitable
		if(GameInfo.recruitable[GameInfo.currentNPC]){
			textToScreen = LoadDialogue.setNPCResponseIfRecruitable();
		}
		//check if npc is recruitable and encountered alot
		if((GameInfo.recruitable[GameInfo.currentNPC])&&(GameInfo.encountered[GameInfo.currentNPC] > 3)){
		//the response here will be similar if it is recruitable, but it's just to ensure nothing else can happen
			textToScreen = LoadDialogue.setNPCResponseIfRecruitable();
			choice1.onClick.RemoveAllListeners();
			txt1.text="Add to Party";
		}
		//check if npc is on the team
		if((GameInfo.party[0].npc.name==GameInfo.getName(GameInfo.currentNPC))||(GameInfo.party[1].npc.name==GameInfo.getName(GameInfo.currentNPC))){
			textToScreen = LoadDialogue.setNPCResponseIfOnTeam();
			GameObject.Find("Choice2").SetActive(false);
		}

		//start the typing effect for whichever textToScreen was decided on
		StartCoroutine(type());
		}
    }
	/// <summary>
    /// This is a listener for the top of the two buttons, which we will call option 1 from here on
    /// Once the button is clicked all Coroutines stop, which stops the animation 
	/// and the response, option1 text, and option2 text are displayed on the screen
    /// This involve a lot of checking of the index that is passed there are special indexes which are listed
	/// at the top of LoadDialogue.cs. The resultant should be the correct response and correct next options
	/// The tree traversal rule is also in LoadDialogue.cs.
    /// </summary>
	public void clickedOption1(int index){
		//First stop the Typing affect
		StopAllCoroutines();

		//turn the other button on because new choices are going to be displayed
		choice2.interactable=true;
		npcResponse.text="";
		
		//Here we check if the NPC is recruitable and encountered a lot or if the NPC is in the party 
		if(GameInfo.recruitable[GameInfo.currentNPC]&&GameInfo.encountered[GameInfo.currentNPC]>3
		|| (GameInfo.party[0].npc.name==GameInfo.getName(GameInfo.currentNPC))||(GameInfo.party[1].npc.name==GameInfo.getName(GameInfo.currentNPC))){
			//if true then fill that corresponding text in the response area
			npcResponse.text = textToScreen;
			
			//set the option1 to a special index to exit the menu
			//and the option2 to fight
			indexForNextOption1=-2;
			indexForNextOption2=0;

			//to ensure that option1 leaves the menu remove the old listener and add the new one
			choice1.onClick.RemoveAllListeners();
			choice1.onClick.AddListener(cancelMenu);

			//display the corresponding options
			txt1.text = "Leave";
			txt2.text = "Fight";
			return;//we return here because the rest of this listener does not need to be called
		}

		//we check if this is the first part of the interaction
		if(index==0){
			//these are the basic options
				txt1.text = "Talk";
				txt2.text = "Fight";
		}

		//we check if the Player has reached the option to add the NPC to the party
		if(index==-3){
			//the NPC responds saying that they can join the party
			npcResponse.text = "I can go with you.";
			//update GameInfo so when we interact with this NPC again we can check if we can add this NPC to party
			GameInfo.recruitable[GameInfo.currentNPC] = true;
		}

		//We check if we are changing Party Slots
		if(index==-4){
			npcResponse.text = "I can go with you.";
			//then follow the steps to add the NPC to the party
			addToParty();
			indexForNextOption1=-2;
			indexForNextOption2=0;
			return;
		}
		
		//We check if the NPC is recruitable
		if(GameInfo.recruitable[GameInfo.currentNPC]){
			//Set the response and the options
			npcResponse.text = "I can go with you.";
			indexForNextOption1=-4;
			indexForNextOption2=0;

			//we turn this off because option2 should do nothing, this is to ensure it does nothing
			choice2.interactable=false;
			//we add this listener so if pressed we add to the party by selecting option1
			choice1.onClick.AddListener(addToParty);
            //Here this bit of code is to visually display to the player what the NPC has to offer
            NPCNameDisplay = GameObject.Find("EqName").GetComponent<TextMeshProUGUI>();
            NPCNameDisplay.text = GameInfo.getName(GameInfo.currentNPC);
            NPCDetailsDisplay = GameObject.Find("EqInfo").GetComponent<TextMeshProUGUI>();
            NPCDetailsDisplay.text = GameInfo.getPrimaryActionName(GameInfo.currentNPC) + "\n" + GameInfo.getSecondaryActionName(GameInfo.currentNPC) + "\nCurrent Health: " + GameInfo.getNPCHealth(GameInfo.currentNPC);
            NPCimageDetails = GameObject.Find("EqImage").GetComponent<Image>();
            NPCimageDetails.overrideSprite = Resources.Load<Sprite>("DialogueImages/" + GameInfo.getName(GameInfo.currentNPC));
            NPCimageDetails.color = Color.white;
            NPCimageDetails.enabled = true;

			//then set the next choices
            txt1.text = "Add to Party";
			txt2.text = "";
					return;
		}
		//next we check if the NPC was encountered a lot
		if(GameInfo.encountered[GameInfo.currentNPC]>3){
			//the NPC doesn't have any real interest in talking to you if so
			//and the player will be stuck with the options to talk or to fight
			npcResponse.text = textToScreen;
			indexForNextOption1=0;
			indexForNextOption2=0;
			txt1.text = "Talk";
			txt2.text = "Fight";
					return;
		}
		
		//Here we check if the coroutine is still going
		if(isTyping){
			//then check if the NPC is in the party already
			if(checkIfInParty()){
			//we get limited options again if so, only able to leave or fight
			txt1.text = "Leave";
			txt2.text = "Fight";
			indexForNextOption1=-2;
			indexForNextOption2=0;
			choice1.onClick.RemoveAllListeners();
			choice1.onClick.AddListener(cancelMenu);
			}
			else if(indexForNextOption1==0&&indexForNextOption2==0){
			//then we check again if this is the begining of the interaction
			txt1.text = "Talk";
			txt2.text = "Fight";
			}
			else{
			//set the text from the temp values
			txt1.text = temp1;
			txt2.text = temp2;
			}
			//set text for the response
			npcResponse.text = textToScreen;
			isTyping=false;
			return;
		}
		//check if we need to restart the dialogue
		if(index==-1){
			//restart all values to 0
			//increase the amount of times we restarted by 1
			index=0;
			indexForNextOption1=0;
			indexForNextOption2=0;
			textToScreen = currentDialogue[0].response;
			restarts++;
		}
		//check if this is the first time interacting with the npc
		if(index==0){
			//here we increase the index by one for the next response so we can store those values
			index++;
			textToScreen = currentDialogue[index].response;

			indexForNextOption1 = currentDialogue[index].indexForOption1;
			indexForNextOption2 = currentDialogue[index].indexForOption2;
			index--;

		}
		//the conditions in this if statement are instances when an NPC heals the player
		if(((index==3 || index == 12)&& GameInfo.currentNPC==0)||((index==1||index==5)&&GameInfo.currentNPC==2)){
			//heal Ego
			GameInfo.updateCurrentHealth(-50);
		}

		//Check if we are leaving the scene
		if((index==-2)){
			//Load into the overworld
			SceneManager.LoadScene(GameInfo.prevScene);

		}
		//if all of that is bypassed then store the next options for the objects to display when the coroutine is finished
		textToScreen = currentDialogue[index].response;
		StartCoroutine(type());
		temp1 = currentDialogue[index].option1;
		temp2 = currentDialogue[index].option2;
		indexForNextOption1 = currentDialogue[index].indexForOption1;
		indexForNextOption2 = currentDialogue[index].indexForOption2;
		return;
	}


    /// <summary>
    /// This is a listener for the top of the two buttons, which we will call option 1 from here on
    /// Once the button is clicked all Coroutines stop, which stops the animation 
	/// and the response, option1 text, and option2 text are displayed on the screen
    /// This involve a lot of checking of the index that is passed there are special indexes which are listed
	/// at the top of LoadDialogue.cs. The resultant should be the correct response and correct next options
	/// The tree traversal rule is also in LoadDialogue.cs.
	/// This button also has the special property that it is always the Combat initiator
    /// </summary>
	public void clickedOption2(int index){
		//First stop the Typing affect
		StopAllCoroutines();
		
		//turn the other button on because new choices are going to be displayed
		choice2.interactable=true;
		
		//Here we check if the coroutine is still going
		if(isTyping){
			//we check to see if this is the first instance of encountering with this npc
			if(index==0){
				txt1.text = "Talk";
				txt2.text = "Fight";
			}
			else{
				//set the previous temps
			txt1.text = temp1;
			txt2.text = temp2;
			}
			//display the response
			npcResponse.text = textToScreen;
			isTyping=false;
			return;
		}
		//check if this is the first instance of encountering with this npc
		if(index == 0){
			//set the Text objects with the temp values
			npcResponse.text = textToScreen;
			
			txt1.text = temp1;
			txt2.text = temp2;
			//this will be the fight option and will change scenes and pass information about who the enemy is
			playMusic.StopAllMusic(); //Accounts for whatever scene you could be in
			playMusic.PlayMusicBySceneName("Combat");
			SceneManager.LoadScene("Combat");
			return;
		}
		//start the response blank
		npcResponse.text="";
		//if the player traverses and finds the path that makes the npc recruitable
		if(index==-3){
			//set that this npc is now recruitable
			npcResponse.text = "I can go with you.";
			GameInfo.recruitable[GameInfo.currentNPC] = true;
		}
		if(index==-4){
			//if we decided to add this npc to the party
			npcResponse.text = "I can go with you.";
			addToParty();
			indexForNextOption1=-2;
			indexForNextOption2=0;
			return;
		}
		
		//Here we check if the NPC is recruitable and encountered a lot or if the NPC is in the party 
		if(GameInfo.recruitable[GameInfo.currentNPC]&&GameInfo.encountered[GameInfo.currentNPC]>3
		&& (GameInfo.party[0].npc.name==GameInfo.getName(GameInfo.currentNPC))||(GameInfo.party[1].npc.name==GameInfo.getName(GameInfo.currentNPC))){
			npcResponse.text = "I can go with you.";
			indexForNextOption1=-4;
			indexForNextOption2=0;
			choice1.onClick.AddListener(addToParty);
            //Here this bit of code is to visually display to the player what the NPC has to offer
            NPCNameDisplay = GameObject.Find("EqName").GetComponent<TextMeshProUGUI>();
            NPCNameDisplay.text = GameInfo.getName(GameInfo.currentNPC);
            NPCDetailsDisplay = GameObject.Find("EqInfo").GetComponent<TextMeshProUGUI>();
            NPCDetailsDisplay.text = GameInfo.getPrimaryActionName(GameInfo.currentNPC) + "\n" + GameInfo.getSecondaryActionName(GameInfo.currentNPC) + "\nCurrent Health: " + GameInfo.getNPCHealth(GameInfo.currentNPC);
            NPCimageDetails = GameObject.Find("EqImage").GetComponent<Image>();
            NPCimageDetails.overrideSprite = Resources.Load<Sprite>("DialogueImages/" + GameInfo.getName(GameInfo.currentNPC));
            NPCimageDetails.color = Color.white;
            NPCimageDetails.enabled = true;

			//set the appropriate choices
            txt1.text = "Add to Party";
			txt2.text = "Fight";
					return;
		}
		
		//Here we check if the NPC is recruitable 
		if(GameInfo.recruitable[GameInfo.currentNPC]){
			npcResponse.text = "I can go with you.";
			indexForNextOption1=-4;
			indexForNextOption2=0;
			choice2.interactable=false;
            //Here this bit of code is to visually display to the player what the NPC has to offer
            NPCNameDisplay = GameObject.Find("EqName").GetComponent<TextMeshProUGUI>();
            NPCNameDisplay.text = GameInfo.getName(GameInfo.currentNPC);
            NPCDetailsDisplay = GameObject.Find("EqInfo").GetComponent<TextMeshProUGUI>();
            NPCDetailsDisplay.text = GameInfo.getPrimaryActionName(GameInfo.currentNPC) + "\n" + GameInfo.getSecondaryActionName(GameInfo.currentNPC) + "\nCurrent Health: " + GameInfo.getNPCHealth(GameInfo.currentNPC);
            NPCimageDetails = GameObject.Find("EqImage").GetComponent<Image>();
            NPCimageDetails.sprite = Resources.Load<Sprite>("DialogueImages/" + GameInfo.getName(GameInfo.currentNPC));
            NPCimageDetails.color = Color.white;
            NPCimageDetails.enabled = true;
			//set the appropriate choices
            txt1.text = "Add to Party";
			txt2.text = "";
					return;
		}
		//check if the npc has been encountered a lot
		if(GameInfo.encountered[GameInfo.currentNPC]>3){
			//the NPC doesn't have any real interest in talking to you if so
			//and the player will be stuck with the options to talk or to fight
			npcResponse.text = textToScreen;
			indexForNextOption1=0;
			indexForNextOption2=0;
			txt1.text = "Talk";
			txt2.text = "Fight";
					return;
		}
		//check if we are restarting
		if(index==-1){
			//set text all back to the base values
			choice2.interactable=false;
			
			index=0;
			indexForNextOption1=0;
			indexForNextOption2=-1;
			textToScreen = currentDialogue[0].response;
			restarts++;
			//return;
		}
		
		//check if this is the first time interacting with the npc
		if(index==0){
			//here we increase the index by one for the next response so we can store those values
			index++;
			textToScreen = currentDialogue[index].response;

			indexForNextOption1 = currentDialogue[index].indexForOption1;
			indexForNextOption2 = currentDialogue[index].indexForOption2;
			index--;

		}
		
		//the conditions in this if statement are instances when an NPC heals the player
		if((index==3 || index == 12)&& GameInfo.currentNPC==0){
			//heal Ego
			GameInfo.updateCurrentHealth(50);

		}
		//check if we are leaving the npc
		if((index==-2)){
			//Load into the overworld
			SceneManager.LoadScene(GameInfo.prevScene);

		}
		
		//if all of that is bypassed then store the next options for the objects to display when the coroutine is finished
		textToScreen = currentDialogue[index].response;
		StartCoroutine(type());
		temp1 = currentDialogue[index].option1;
		temp2 = currentDialogue[index].option2;
		indexForNextOption1 = currentDialogue[index].indexForOption1;
		indexForNextOption2 = currentDialogue[index].indexForOption2;
		return;
	}
	/// <summary>
    /// This listener takes the Player from the menu back to the scene they were just at
    /// </summary>
    public void cancelMenu(){
		SceneManager.LoadScene(GameInfo.prevScene);
	}
	/// <summary>
    /// This listener tchanges the text of the trade button to the approriate text
    /// </summary>
    public void beginTrade(){
        //make all the left side of the menu interactable
        if (GameInfo.buyingMode)
        {
            trade.GetComponentInChildren<Text>().text = "Exit Trade";
        }
        else
        {
            trade.GetComponentInChildren<Text>().text = "Start Trade";
        }
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
		//first we set all the text objects to empty so that we can place the text in there only when the typing is done
		isTyping=true;
		txt1.text="";
		txt2.text="";
		npcResponse.text="";
		//we need to turn the choice2 back on so the player can click it to stop the coroutine
		choice2.interactable=true;
		//create the typing affect
		foreach (char letter in textToScreen.ToCharArray()) {
             npcResponse.text += letter;
             yield return new WaitForSeconds ((float).01);
         }
		 //here we need to decide what the options are
		 //first we check if the NPC is in the party
		 if(checkIfInParty()){
			//set the choices
			txt1.text = "Leave";
			txt2.text = "Fight";
			//ensure the first choice exits the menu
			choice1.onClick.RemoveAllListeners();
			choice1.onClick.AddListener(cancelMenu);
		 }
		 //check if this is the first interaction
		 else if(indexForNextOption1==0&&indexForNextOption2==0){
			 //set the text
			txt1.text = "Talk";
			txt2.text = "Fight";
			choice2.interactable=true;
		 }
		 else{
			 //set the text
		 	txt1.text = temp1;
		 	txt2.text = temp2;
		 }
		 //check if the NPC is not in the party and is recruitable
		 if(GameInfo.recruitable[GameInfo.currentNPC]&&!checkIfInParty()){
			 //set text
			 txt1.text = "Will you come with me?";
			 txt2.text = "Leave";
		 }
		 
		 isTyping=false;
	}
	/// <summary>
    /// This just returns if the NPC is in the Party, for reusability
    /// </summary>
	public bool checkIfInParty(){
		return(GameInfo.party[0].npc.name==GameInfo.getName(GameInfo.currentNPC))||(GameInfo.party[1].npc.name==GameInfo.getName(GameInfo.currentNPC));
	}
	/// <summary>
    /// Every NPC besides the bounties have a specific text color associated with them
    /// </summary>
	public void setTextColor(){
		if(GameInfo.currentNPC==0){//Cynthia
			npcResponse.faceColor= new Color32(255, 240, 127,255);
		}
		if(GameInfo.currentNPC==1){//Anker
		npcResponse.faceColor= new Color32(183, 189, 255,255);
		}
		if(GameInfo.currentNPC==3){//Emrik
			npcResponse.faceColor= new Color32(182, 255, 170,255);
		}
		if(GameInfo.currentNPC==2){//Edward
			npcResponse.faceColor= new Color32(255, 84, 84,255);
		}
		if(GameInfo.currentNPC==4){//Berndy
			npcResponse.faceColor= new Color32(130,130,130,255);
		}
		if(GameInfo.currentNPC==5 || GameInfo.currentNPC==6){//Farenver or Modir		
			npcResponse.faceColor= new Color32(130,130,130,255);
		}
	}
	/// <summary>
    /// This listener changes the functionality of the 2 buttons, instead of choices they are now associated with slots in the party
    /// </summary>
	public void addToParty(){
		StopAllCoroutines();//stop the typing effect
		
		choice2.interactable=true;
		//create the new PartyMember object based on the currentNPC
		PartyMember = GameInfo.potentialNPC[GameInfo.currentNPC];
		//if the npc is already added to the team then  go into the function alreadyAdded
		if((PartyMember.npc.name == GameInfo.party[0].npc.name) || (PartyMember.npc.name == GameInfo.party[1].npc.name)){
			alreadyAdded();
			return;
		}
		//remove old listeners
		choice1.onClick.RemoveAllListeners();
		choice2.onClick.RemoveAllListeners();
		//add new listeners
        choice1.onClick.AddListener(AddToSlot1);
        choice2.onClick.AddListener(AddToSlot2);
		//set text
		npcResponse.text = "Which Slot would you like to add "+GameInfo.getName(GameInfo.currentNPC)+"?";
		txt1.text = "Slot 1";
		txt2.text = "Slot 2";
	}
	/// <summary>
    /// This is for if the NPC is already added to the party we should get far into dialogue just one response
    /// </summary>
	public void alreadyAdded(){
		//ensure there are no other listeners on the buttons
		choice1.onClick.RemoveAllListeners();
		choice2.onClick.RemoveAllListeners();
		//add our new listeners
        choice1.onClick.AddListener(cancelMenu);
        choice2.onClick.AddListener(doNothing);
		//set text
		npcResponse.text = LoadDialogue.setNPCResponseIfOnTeam();//GameInfo.getName(GameInfo.currentNPC)+" is already a part of the team.";
		txt1.text = "Leave";
		txt2.text = "";
		choice2.interactable=true;
		return;
	}
	/// <summary>
    /// makes the second choice do nothing
    /// </summary>
	public void doNothing(){
		choice2.interactable=false;
	}
	/// <summary>
    /// This listener is for adding the new NPC to the Party in GameInfo and also visually representing the change in the slots on the left side of the menu
    /// </summary>
	public void AddToSlot1(){
		//Get the image for the current NPC and set it to slot 1
		GameObject.Find("EgoPartyImage1").GetComponent<Image>().sprite =
		Resources.Load<Sprite>("DialogueImages/"+GameInfo.getName(GameInfo.currentNPC));
		//here we make sure that this assignment is saved for future checking
        PartyMember.isAssigned=true;
		//set the party member to slot 1 in GameInfo for use in combat
		GameInfo.party[0] = PartyMember;
		GameInfo.party[0].slotID = GameInfo.currentNPC;
		//set text for party slot
        GameObject.Find("PartyName0").GetComponent<TextMeshProUGUI>().text = GameInfo.party[0].npc.name;
        //Hide NPC Details
        NPCNameDisplay = GameObject.Find("EqName").GetComponent<TextMeshProUGUI>();
        NPCNameDisplay.text = "";
        NPCDetailsDisplay = GameObject.Find("EqInfo").GetComponent<TextMeshProUGUI>();
        NPCDetailsDisplay.text = "";
        NPCimageDetails = GameObject.Find("EqImage").GetComponent<Image>();
        NPCimageDetails.color = Color.clear;
        //Added to gift item
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
	/// <summary>
    /// This listener is for adding the new NPC to the Party in GameInfo and also visually representing the change in the slots on the left side of the menu
    /// </summary>
	public void AddToSlot2(){

		//Get the image for the current NPC and set it to slot 2
		GameObject.Find("EgoPartyImage2").GetComponent<Image>().sprite =
		 Resources.Load<Sprite>("DialogueImages/"+GameInfo.getName(GameInfo.currentNPC));
		 
		//here we make sure that this assignment is saved for future checking
        PartyMember.isAssigned=true;
		//set the party member to slot 1 in GameInfo for use in combat
		GameInfo.party[1] = PartyMember;
		GameInfo.party[1].slotID = GameInfo.currentNPC;
		//set text for party slot
        GameObject.Find("PartyName1").GetComponent<TextMeshProUGUI>().text = GameInfo.party[1].npc.name;
        //Hide NPC Details
        NPCNameDisplay = GameObject.Find("EqName").GetComponent<TextMeshProUGUI>();
        NPCNameDisplay.text = "";
        NPCDetailsDisplay = GameObject.Find("EqInfo").GetComponent<TextMeshProUGUI>();
        NPCDetailsDisplay.text = "";
        NPCimageDetails = GameObject.Find("EqImage").GetComponent<Image>();
        NPCimageDetails.color = Color.clear;
        //Added to gift item
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
		int i = 0;//this will be used to decide which gift we are receiving from the recruitted NPC
		if(GameInfo.currentNPC==0){//HealPotion
			i=11;
		}
		if(GameInfo.currentNPC==2){//Scaple
			i=3;
		}
		if(GameInfo.currentNPC==3){//Sickle
			i=7;
		}
		if(GameInfo.currentNPC!=1){//Anker does not give us a gift so the others need the last bit of text
		npcResponse.text = GameInfo.getName(GameInfo.currentNPC) + " is now added to your party!"
		+" You received "+GameInfo.equipmentStrings[i, 0]+" as a gift!";
		}
		else{
		//Anker gives us nothing so we just notify the player that a party memeber is added
		npcResponse.text = GameInfo.getName(GameInfo.currentNPC) + " is now added to your party!";
		}
		//set text
		txt1.text = "Leave";
		txt2.text = "";
		//remove listeners
		choice1.onClick.RemoveListener(AddToSlot1);
		choice2.onClick.RemoveListener(AddToSlot2);
		//then give the player only one option, to leave
		choice2.interactable=false;
		choice1.onClick.AddListener(cancelMenu);
	}
}

