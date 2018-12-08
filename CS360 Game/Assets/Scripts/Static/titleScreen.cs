using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class titleScreen : MonoBehaviour {
	//References to GameObjects within the scene
	Button begin, exit;
	TextMeshProUGUI warningText;
	Canvas canvas;

	//String data for the the easter egg typing
	string textForArray;
	//amount of time between each letter appearing on screen
    public float letterPause = 0.3f;

	/// <summary>
    /// The start function is called every time the script is loaded into a scene
	/// In this case it gets all the game objects from the scene and sets them
    /// </summary>
	void Start () {
		//instantiate references for manipulation
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		begin=	GameObject.Find("BeginButton").GetComponent<Button>();
		exit = GameObject.Find("Exit").GetComponent<Button>();
    	warningText = GameObject.Find("warning").GetComponent<TextMeshProUGUI>();

		//set the base text for the easter egg.
		textForArray = "I know you're there...";
		warningText.text = "";
		
		//create listeners for the buttons
		begin.onClick.AddListener(beginGame);
		exit.onClick.AddListener(endGame);

		//start the typing effect coroutine
   		StartCoroutine(egg());

    }

	/*
This is a coroutine and runnings alongside other functions.
It's use is for the typing effect
While waiting for the Player to choose an option this is running in the background
 */
    IEnumerator egg()
    {
        yield return new WaitForSeconds(30);//wait for 30 seconds

		//then start appending the temp text into the gameobject one character at a time
		foreach (char letter in textForArray.ToCharArray()) {
             warningText.text += letter;
			//is how the type effect is achieved,
			//between each iteration the game waits to do the next
             yield return new WaitForSeconds (letterPause);
         }
		//then wait again for 30 seconds
		yield return new WaitForSeconds(30);
		 string nextResponse = "\nThe longer you wait, \nthe shadows grow stronger...";
		 foreach (char letter in nextResponse.ToCharArray()) {
			 //print the text to the screen for the second part of the egg
             warningText.text += letter;
             yield return new WaitForSeconds (letterPause);
         }
    }
		
	
	/// <summary>
    /// When the Player selects this button the music changes and the scene changes
    /// </summary>
	public void beginGame(){
		playMusic.StopMusic("title");
        playMusic.PlayMusic("overworld");
		SceneManager.LoadScene(1);
	}

	/// <summary>
    /// When the Player selects this button the game application is closed
    /// </summary>
	public void endGame(){
		Application.Quit();

	}
	
}
