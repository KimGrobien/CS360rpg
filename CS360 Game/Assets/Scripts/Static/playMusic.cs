using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playMusic : MonoBehaviour {
	// AudioSources for each music track played during game. Static so that they maintain position in between scenes
	private static AudioSource overworld;
	private static AudioSource battle;
	private static AudioSource shop;
	private static AudioSource title;
	private static AudioSource doctor;
	private static AudioSource cave;
	private static AudioSource lose;
	private static AudioSource win;

	// Use this for initialization

	// PlayMusic plays the specified audio track
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
			case "lose":
				lose.Play();
				break;
			case "win":
				win.Play();
				break;
		}
	}
 // StopMusic stops the specified audio track
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
			case "lose":
				lose.Stop();
				break;
			case "win":
				win.Stop();
				break;
		}
    }
// StopAllMusic stops all the audio tracks
	public static void StopAllMusic(){
		overworld.Stop();
		shop.Stop();
		title.Stop();
		cave.Stop();
		doctor.Stop();
		battle.Stop();
		lose.Stop();
		win.Stop();
	}
	//PlayMusicBySceneName plays the track appropriate to a specified scene name
	public static void PlayMusicBySceneName(string name){
		switch (name){
			case "Start": case "Town": case "Castle": case "Bounty":
				overworld.Play();
				break;
			case "Castle Hall": case "Boss Room":
				cave.Play();
				break;
			case "Shop":
				shop.Play();
				break;
			case "Doctor":
				doctor.Play();
				break;
			case "Title":
				title.Play();
				break;
			case "Combat":
				battle.Play();
				break;
			case "You Lose":
				lose.Play();
				break;
			case "You Win":
				win.Play();
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
		lose = GameObject.Find("Lose").GetComponent<AudioSource>();
		win = GameObject.Find("Win").GetComponent<AudioSource>();
		PlayMusic("title");
	}
	
}
