using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public bool frozen; //Allows freezing movement for duration of certain animations
	private Animator anim;
	// Use this for initialization
	void Start () {
        moveSpeed = 2;
		if (GameInfo.prevPos.x != -999){//Ignore prevPos if -999
			this.transform.position = GameInfo.prevPos;
		}
		if(GameInfo.currentNPC >= 0){
			GameInfo.currentNPC = -1; //No longer interacting with NPC
		}
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!frozen){
			if (Input.GetAxisRaw("Horizontal") > 0.5f || (Input.GetAxisRaw("Horizontal") < -0.5f)) {
				transform.Translate (new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
			}
			if (Input.GetAxisRaw("Vertical") > 0.5f || (Input.GetAxisRaw("Vertical") < -0.5f)) {
				transform.Translate (new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
			}
			anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
			anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
		}
	}
}
