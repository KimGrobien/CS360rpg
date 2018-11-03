﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterNewArea : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D other) {
            sceneData.prevScene = SceneManager.GetActiveScene().buildIndex; 
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
                case "enter_bounty":
                    SceneManager.LoadScene(4);
                    break;
            }
	}
}
