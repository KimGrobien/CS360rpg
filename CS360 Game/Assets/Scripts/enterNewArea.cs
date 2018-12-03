using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterNewArea : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D other) {
            GameInfo.prevScene = SceneManager.GetActiveScene().buildIndex; 
            GameInfo.prevPos.x = -999;//-999 special value to signify ignore prevPos
            switch (this.name)
            {
                case "enter_start":
                    if(SceneManager.GetActiveScene().name == "Town"){//Different entry point if coming from town
                        GameInfo.prevPos = new Vector3(-1.55f, 6.76f, 0);
                    }
                    SceneManager.LoadScene(1);
                    break;
                case "enter_town":
                    //Account for 4 alternate entry points to town
                    switch (SceneManager.GetActiveScene().name){
                        case "Castle":
                            playMusic.StopMusic("cave");
                             playMusic.PlayMusic("overworld");
                            GameInfo.prevPos = new Vector3(-8.07f, -4.72f, 0);
                            break;
                        case "Bounty":
                           GameInfo.prevPos = new Vector3(.36f, 6.84f, 0);
                            break;
                        case "Shop":
                            playMusic.StopMusic("shop");
                             playMusic.PlayMusic("overworld");
                            GameInfo.prevPos = new Vector3(-5.68f, -4.49f, 0);
                            break;
                        case "Hospital":
                            playMusic.StopMusic("doctor");
                             playMusic.PlayMusic("overworld");
                            GameInfo.prevPos = new Vector3(-4.43f, -.79f, 0);
                            break;
                    }
                    SceneManager.LoadScene(2);
                    break;
                case "enter_boss_room":
                    SceneManager.LoadScene("Boss Room");
                    break;
                case "enter_castle_area":
                    if(SceneManager.GetActiveScene().name == "Castle Hall"){
                        GameInfo.prevPos = new Vector3(-.25f, -.43f, 0);
                        playMusic.StopMusic("cave");
                        playMusic.PlayMusic("overworld");
                    }
                    SceneManager.LoadScene("Castle");
                    break;
                case "enter_bounty":
                    GameInfo.prevPos.y *= -1;
                    SceneManager.LoadScene(4);
                    break;
                case "enter_hospital":
                    playMusic.StopMusic("overworld");
                    playMusic.PlayMusic("doctor");
                    SceneManager.LoadScene("Hospital");
                    break;
                case "enter_shop":
                    playMusic.StopMusic("overworld");
                    playMusic.PlayMusic("shop");
                    SceneManager.LoadScene("Shop");
                    break;
                case "black_entrance":
                    playMusic.StopMusic("overworld");
                    playMusic.PlayMusic("cave");
                    SceneManager.LoadScene("Castle Hall");
                    break;
                case "exit_castle":
                    playMusic.StopMusic("cave");
                    playMusic.PlayMusic("overworld");
                    GameInfo.prevPos = new Vector3(-.25f, -.43f, 0);
                    SceneManager.LoadScene("Castle");
                    break;
            }
	}
}
