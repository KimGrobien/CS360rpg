using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class equipment : MonoBehaviour {

    public Image img;
    public bool inInventory;
       
	// Use this for initialization
	void Start () {
        inInventory = false;
        img.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
