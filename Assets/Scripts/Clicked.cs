using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class Clicked : MonoBehaviour {

	[SerializeField]
	private Color activatedColor;
	
	[SerializeField]
	private int motorNb;

	[SerializeField]
	private KeyCode assignedKey;
	//private string assignedKey;
	private bool vibrating = false;
	private Color currentColor;
	private float mTime = 0;
	private int currentStepIndex = 0;
	private Renderer rend;

	// Use this for initialization
	void Start ()
	{
		rend = GetComponent<Renderer>();
		currentColor = rend.material.color;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (assignedKey))
		{
			vibrating = true;
			rend.material.color = activatedColor;
			print("Activating Motor " + motorNb.ToString());
		} 
		else if (Input.GetKeyUp (assignedKey))
		{
			vibrating = false;
			mTime = 0;
			currentStepIndex = 0;
			rend.material.color = currentColor;
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
				currentStepIndex = (currentStepIndex == patterns[patternID].fingers[motorNb].pattern.Count - 1 ? 0 : ++currentStepIndex);
				mTime = 0;
				LibRoukaVici.Vibrate((char)motorNb, (char)(currentStep * 255 / 100));
			}
		}
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			vibrating = !vibrating;
			rend.material.color = (vibrating ? activatedColor : currentColor);
			print ((vibrating ? "Activating Motor " : "Deactivating Motor ") + motorNb.ToString());
		}
	}
}