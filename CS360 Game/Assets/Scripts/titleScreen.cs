using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class titleScreen : MonoBehaviour {
	Button begin, exit;
	TextMeshProUGUI warningText;
	Canvas canvas;
	string textForArray;
	
    public float letterPause = 0.3f;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		begin=	GameObject.Find("BeginButton").GetComponent<Button>();
		exit = GameObject.Find("Exit").GetComponent<Button>();
    	warningText = GameObject.Find("warning").GetComponent<TextMeshProUGUI>();
		textForArray = "I know you're there...";
		warningText.text = "";
		begin.onClick.AddListener(beginGame);
		exit.onClick.AddListener(endGame);

   		StartCoroutine(egg());

    		}

    IEnumerator egg()
    {
        yield return new WaitForSeconds(30);

		foreach (char letter in textForArray.ToCharArray()) {
             warningText.text += letter;
             yield return new WaitForSeconds (letterPause);
         }

		yield return new WaitForSeconds(30);
		 string nextResponse = "\nThe longer you wait, \nthe shadows grow stronger...";
		 foreach (char letter in nextResponse.ToCharArray()) {
             warningText.text += letter;
             yield return new WaitForSeconds (letterPause);
         }
    }
		
	

	public void beginGame(){
		playMusic.StopMusic("title");
        playMusic.PlayMusic("overworld");
		SceneManager.LoadScene(1);
	}

	public void endGame(){
		Application.Quit();

	}
	
}
