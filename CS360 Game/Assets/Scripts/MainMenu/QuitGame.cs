using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuitGame : MonoBehaviour {

//The name of the game object within the scene
public string buttonName;
//This is a reference to the game object within the scene.
Button quitButton;

/// <summary>
///This function is responsible for instantiating the button and the listener for the button
/// <summary>
void Start(){
	quitButton = GameObject.Find(buttonName).GetComponent<Button>();
	quitButton.onClick.AddListener(CloseGame);
}

/// <summary>
/// Closes the application
/// <summary>
void CloseGame(){
		Application.Quit();
}
}
