using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	Slider EgoHealthBarSlider;
	Text egoHealthText;
	public string SliderName,egoHealthTextName;
	// Use this for initialization
	void Update () {
		EgoHealthBarSlider = GameObject.Find(SliderName).GetComponent<Slider>();
		egoHealthText = GameObject.Find(egoHealthTextName).GetComponent<Text>();
		EgoHealthBarSlider.value = getHealthPercentage();
		egoHealthText.text = GameInfo.getEgoCurrentHealth()+"/"+GameInfo.getEgoMaxHealth();
	}
	
	float getHealthPercentage(){
		return (float)GameInfo.getEgoCurrentHealth()/GameInfo.getEgoMaxHealth();
	}
}
