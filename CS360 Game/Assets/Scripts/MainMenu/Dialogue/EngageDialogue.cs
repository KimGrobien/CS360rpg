using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EngageDialogue : MonoBehaviour {

	public int NPCID;
	Button btn;
	Text txt;
  
	public void Start() {
		//GameObject.Find("TestButton").GetComponentInChildren<Text>().text = "something";
		
		btn = gameObject.GetComponent<Button>();
		txt = btn.GetComponentInChildren<Text>();
		txt.text = "something";
		btn.onClick.AddListener(clicked);
    }

	public void clicked(){
		txt.text = "something else";
	}

}
