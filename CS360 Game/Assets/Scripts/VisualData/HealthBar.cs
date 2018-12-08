using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	//References to GameObjects within the scene
	Slider EgoHealthBarSlider;
	Text egoHealthText;
	
	//Public values so this could be reused in any other module that contains a Slider and Text object
	//These are used to find the slider and the text to overlay the slider within the scene
	public string SliderName,egoHealthTextName;

	/// <summary>
    /// Update occurs every frame of the gameplay so this is all the time
	/// The purpose here is to always display the correct information and the most current
	/// To the player so they can make decisions based on their health
    /// </summary>
	void Update () {
		//Find the objects in the scene based on given names
		EgoHealthBarSlider = GameObject.Find(SliderName).GetComponent<Slider>();
		egoHealthText = GameObject.Find(egoHealthTextName).GetComponent<Text>();

		//the .value of a slider object is the percentage value of how big the bar is
		//so here we calculate the percent value and set it so the player can see the information visually
		EgoHealthBarSlider.value = getHealthPercentage();

		//Get the current health and max health of ego and display it as a fraction for the player
		egoHealthText.text = GameInfo.getEgoCurrentHealth()+"/"+GameInfo.getEgoMaxHealth();
	}
	/// <summary>
    /// Since slider.value needs a float this is function returns that value for the bar to display correctly
    /// </summary>
	float getHealthPercentage(){
		return (float)GameInfo.getEgoCurrentHealth()/GameInfo.getEgoMaxHealth();
	}
}
