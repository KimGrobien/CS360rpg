using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private Animator anim;
	// Use this for initialization
	private bool destroyed = false; //Needed since OnDestroy is getting called multiple times due to Unity bug
	void Start () {
		if(GameInfo.currentNPC >= 0){//If interacting with NPC, set position based on before menu scene
			this.transform.position = GameInfo.prevPos;
			GameInfo.currentNPC = -1;
		}else{
			if((SceneManager.GetActiveScene().name == "Castle") && (GameInfo.prevScene == 6)){
			this.transform.position = new Vector3(-.25f, -.43f, 0);//Entering castle scene from inside castle
			}
			if (SceneManager.GetActiveScene().name == "Town"){
				//Town has several entry points so we must set Ego's position in the town scene based on where he entered 
				switch (GameInfo.prevScene)
				{
					case 0://Leaving title screen, initial start of game
						this.transform.position = new Vector3(-1.57f, 1.06f, 0);//Set position to path leaving castle
						break;
					case 3://Entering town from castle
						this.transform.position = new Vector3(-8.07f, -4.72f, 0);//Set position to path leaving castle
						break;
					case 4://Entering town from bounty area
						this.transform.position = new Vector3(.36f, 6.84f, 0);//Set position to path leaving bounty
						break;
					case 7: //Entering town from hospital
						this.transform.position = new Vector3(-4.43f, -.79f, 0);//Set position to path leaving hospital
						break;
					case 8: //Entering town from shop
						this.transform.position = new Vector3(6.17f,5.32f, 0);//Set position to path leaving shop
						break;
				}
			}
		}
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
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
