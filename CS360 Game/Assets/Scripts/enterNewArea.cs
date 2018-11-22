using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterNewArea : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D other) {
            GameInfo.prevScene = SceneManager.GetActiveScene().buildIndex; 
            switch (this.name)
            {
                case "enter_start":
                    SceneManager.LoadScene(1);
                    break;
                case "enter_town":
                    SceneManager.LoadScene(2);
                    break;
                case "enter_castle":
                    SceneManager.LoadScene(3);
                    break;
                case "enter_boss_room":
                    SceneManager.LoadScene("Boss Room");
                    break;
                case "enter_bounty":
                    SceneManager.LoadScene(4);
                    break;
                case "enter_hospital":
                    SceneManager.LoadScene("Hospital");
                    break;
                case "enter_shop":
                    SceneManager.LoadScene("Shop");
                    break;
                case "black_entrance":
                    SceneManager.LoadScene("Castle Hall");
                    break;
                case "exit_castle":
                    SceneManager.LoadScene("Castle");
                    break;
                case "exit_boss_room":
                    SceneManager.LoadScene("Castle Hall");
                    break;
                case "exit_hospital":
                    SceneManager.LoadScene("Town");
                    break;
                case "exit_shop":
                    SceneManager.LoadScene("Town");
                    break;
            }
	}
}
