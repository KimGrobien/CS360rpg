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
                    SceneManager.LoadScene(1);
                    break;
                case "enter_town":
                    //Account for 4 alternate entry points to town
                    switch (SceneManager.GetActiveScene().name){
                        case "Castle":
                            GameInfo.prevPos = new Vector3(-8.07f, -4.72f, 0);
                            break;
                        case "Bounty":
                           GameInfo.prevPos = new Vector3(.36f, 6.84f, 0);
                            break;
                        case "Shop":
                            GameInfo.prevPos = new Vector3(-5.68f, -4.49f, 0);
                            break;
                        case "Hospital":
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
                    }
                    SceneManager.LoadScene("Castle");
                    break;
                case "enter_bounty":
                    GameInfo.prevPos.y *= -1;
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
                    GameInfo.prevPos = new Vector3(-.25f, -.43f, 0);
                    SceneManager.LoadScene("Castle");
                    break;
                case "exit_boss_room":
                    GameInfo.prevPos = new Vector3(-.3f, 3.02f, 0);
                    SceneManager.LoadScene("Castle Hall");
                    GameObject.Find("castle_door").GetComponent<Animator>().SetBool("playOpen", true);//Door should appear open
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
