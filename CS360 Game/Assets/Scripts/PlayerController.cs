using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private Animator anim;
	// Use this for initialization
	void Start () {
		switch (sceneData.prevScene)
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
