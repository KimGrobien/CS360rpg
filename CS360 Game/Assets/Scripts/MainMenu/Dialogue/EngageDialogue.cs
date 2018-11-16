using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EngageDialogue : MonoBehaviour {
	TextMeshProUGUI npcName;
	Button choice1, choice2;
	Text txt1, txt2;
	Node[] currentDialogue;
  
	public void Start() {
        npcName = GameObject.Find("NPC_Name").GetComponent<TextMeshProUGUI>();
        npcName.text = GameInfo.getName(GameInfo.currentNPC);//"test"; //GameInfo.getName(GameInfo.currentNPC)
		
		choice1 = GameObject.Find("Choice1").GetComponent<Button>();
        choice1.onClick.AddListener(clicked);
		choice2 = GameObject.Find("Choice2").GetComponent<Button>();
        choice2.onClick.AddListener(clicked);
		txt1 = choice1.GetComponentInChildren<Text>();
		txt2 = choice2.GetComponentInChildren<Text>();
		currentDialogue = GameInfo.getDialogueTree(GameInfo.currentNPC);
		txt1.text = currentDialogue[0].option1;
		txt2.text = currentDialogue[0].option2;
    }

	public void clicked(){
	}

}
