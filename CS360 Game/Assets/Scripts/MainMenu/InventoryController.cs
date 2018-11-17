using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {
    TextMeshProUGUI equipmentName;
    Button eq1, eq2, eq3, eq4;
    Text stat1, stat2, stat3;
    // Use this for initialization
    void Start () {
        eq1 = GameObject.Find("slot").GetComponent<Button>();
        eq1.onClick.AddListener(itemClicked);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void itemClicked()
    {
        equipmentName = GameObject.Find("EqName").GetComponent<TextMeshProUGUI>();
        equipmentName.text  = "Sheild";
    }
}
