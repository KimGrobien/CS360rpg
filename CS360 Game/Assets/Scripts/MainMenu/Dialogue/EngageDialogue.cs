using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EngageDialogue : MonoBehaviour {
	TextMeshProUGUI npcName;
	Button choice1, choice2;
	Text txt1, txt2;
  
	public void Start() {
        npcName = GameObject.Find("NPC_Name").GetComponent<TextMeshProUGUI>();
        npcName.text = "test"; //GameInfo.getName(GameInfo.currentNPC)
		
		choice1 = GameObject.Find("Choice1").GetComponent<Button>();
        choice1.onClick.AddListener(clicked);
		txt1 = choice1.GetComponentInChildren<Text>();
    }

	public void clicked(){
		txt1.text = GameInfo.currentNPC.ToString();
	}

}
