using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicked : MonoBehaviour {

	[SerializeField]
	private Color activatedColor;
	
	[SerializeField]
	private int motorNb;

	[SerializeField]
	private KeyCode assignedKey;
	//private string assignedKey;
	private bool vibrating = false;
	public Toggle emulatorEnabled;
	private Color currentColor;
	private float mTime = 0;
	private int currentStepIndex = 0;

	// Use this for initialization
	void Start ()
	{
		currentColor = GetComponent<Renderer>().material.color;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (assignedKey))
		{
			vibrating = true;
			GetComponent<Renderer> ().material.color = activatedColor;
			if (emulatorEnabled.isOn)
				print("Emulating activation of Motor " + motorNb.ToString());
			else
				print("Activating Motor " + motorNb.ToString());
		} 
		else if (Input.GetKeyUp (assignedKey))
		{
			vibrating = false;
			mTime = 0;
			currentStepIndex = 0;
			GetComponent<Renderer> ().material.color = currentColor;
			if (emulatorEnabled.isOn)
				print("Emulating deactivation of Motor " + motorNb.ToString());
			else
				print("Deactivating Motor " + motorNb.ToString());
		}
		if (vibrating && RoukaViciController.instance) 
		{
			mTime += Time.deltaTime;
			List<VibrationStyle> patterns = RoukaViciController.instance.vibrationPatterns;
			int patternID = RoukaViciController.instance.patternID;
			if (patterns.Count > 0 && mTime >= patterns[patternID].delay) {
				int currentStep = patterns[patternID].fingers[motorNb].pattern[currentStepIndex];
				Debug.Log("Vibrating finger ID: " + motorNb.ToString() + " Intensity of : " + currentStep);
				currentStepIndex = (currentStepIndex == patterns[patternID].fingers[motorNb].pattern.Length - 1 ? 0 : ++currentStepIndex);
				mTime = 0;
				// CALL ROUKAVICI LIB HERE
			}
		}
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			vibrating = !vibrating;
			GetComponent<Renderer> ().material.color = (vibrating ? activatedColor : currentColor);
			if (emulatorEnabled.isOn)
				print ((vibrating ? "Emulating activation of Motor " : "Emulating deactivation of Motor ") + motorNb.ToString());
			else
				print ((vibrating ? "Activating Motor " : "Deactivating Motor ") + motorNb.ToString());
		}
	}
}