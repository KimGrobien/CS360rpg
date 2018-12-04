using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	Slider HealthBarSlider;
	public string SliderName;
	// Use this for initialization
	void Update () {
		HealthBarSlider = GameObject.Find(SliderName).GetComponent<Slider>();
		HealthBarSlider.value = getHealthPercentage();
	}
	
	float getHealthPercentage(){
		return (float)GameInfo.getEgoCurrentHealth()/GameInfo.getEgoMaxHealth();
	}
}
