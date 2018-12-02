using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playMusic : MonoBehaviour {

	private static AudioSource overworld;
	private static AudioSource battle;
	private static AudioSource shop;
	private static AudioSource title;
	private static AudioSource doctor;
	private static AudioSource cave;

	// Use this for initialization

	public static void PlayMusic(string id)
    {
		switch (id) {
			case "overworld":
				overworld.Play();
				break;
			case "shop":
				shop.Play();
				break;
			case "battle":
				battle.Play();
				break;
			case "title":
				title.Play();
				break;
			case "cave":
				cave.Play();
				break;
			case "doctor":
				doctor.Play();
				break;
		}
	}
 
    public static void StopMusic(string id)
    {
		switch (id) {
			case "overworld":
				overworld.Stop();
				break;
			case "shop":
				shop.Stop();
				break;
			case "title":
				title.Stop();
				break;
			case "cave":
				cave.Stop();
				break;
			case "doctor":
				doctor.Stop();
				break;
			case "battle":
				battle.Stop();
				break;
		}
    }
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
        overworld = GameObject.Find("Overworld").GetComponent<AudioSource>();
		battle = GameObject.Find("Battle").GetComponent<AudioSource>();
		shop = GameObject.Find("Shop").GetComponent<AudioSource>();
		doctor = GameObject.Find("Doctor").GetComponent<AudioSource>();
		title = GameObject.Find("Title").GetComponent<AudioSource>();
		cave = GameObject.Find("Cave").GetComponent<AudioSource>();
		PlayMusic("title");
	}
	
}
