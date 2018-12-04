using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuitGame : MonoBehaviour {

public string buttonName;
Button quitButton;
void Start(){
	quitButton = GameObject.Find(buttonName).GetComponent<Button>();
	quitButton.onClick.AddListener(CloseGame);
}

void CloseGame(){
		Debug.Log("REACHED");
		Application.Quit();
}
}
