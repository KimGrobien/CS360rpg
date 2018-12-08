using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	/// <summary>
    /// Load Scene 1 which is the starting scene for the game
    /// </summary>
	public void PlayGame() {
        	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
