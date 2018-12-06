using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles interaction such as collision, clicks and key presses
/// </summary>
[RequireComponent(typeof(Renderer), typeof(Collider))]
public class MotorActivator : MonoBehaviour
{

	[SerializeField]
	private Color activatedColor;
	
	[SerializeField]
	private motorID motor;

	[SerializeField]
	private KeyCode assignedKey;
	//private string assignedKey;
	private bool vibrating = false;
	private Color currentColor;
	private float mTime = 0;
	private int currentStepIndex = 0;
	private Renderer rend;

	void Start ()
	{
		rend = GetComponent<Renderer>();
		currentColor = rend.material.color;
	}

	void Update ()
	{
		if (Input.GetKeyDown (assignedKey))
		{
            // Key pressed
			vibrating = true;
			rend.material.color = activatedColor;
			print("Activating Motor " + motor.ToString());
		} 
		else if (Input.GetKeyUp (assignedKey))
		{
            // Key released
			vibrating = false;
			mTime = 0;
			currentStepIndex = 0;
			rend.material.color = currentColor;
			print("Deactivating Motor " + motor.ToString());
			LibRoukaVici.Vibrate((char)motor, (char)0);
		}
		if (vibrating && RoukaViciController.instance)
		{
            // Handle vibration
			mTime += Time.deltaTime;
			List<VibrationPattern> patterns = RoukaViciController.instance.vibrationPatterns;
			int patternID = RoukaViciController.instance.patternID;
			if (patterns.Count > 0 && mTime >= patterns[patternID].duration) {
				if (currentStepIndex >= patterns[patternID].motors[(int)motor].pattern.Count)
					currentStepIndex = 0;
				int currentStep = patterns[patternID].motors[(int)motor].pattern[currentStepIndex];
				Debug.Log("Vibrating finger ID: " + motor.ToString() + " Intensity of : " + currentStep);
				currentStepIndex = (currentStepIndex == patterns[patternID].motors[(int)motor].pattern.Count - 1 ? 0 : ++currentStepIndex);
				mTime = 0;
				LibRoukaVici.Vibrate((char)motor, (char)(currentStep * 255 / 100));
			}
		}
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
            // Start Vibrating
			vibrating = !vibrating;
			rend.material.color = (vibrating ? activatedColor : currentColor);
			print ((vibrating ? "Activating Motor " : "Deactivating Motor ") + motor.ToString());
		}
	}

	void OnTriggerEnter(Collider other)
    {
		if (vibrating)
			return ;
        // Start vibrating
		print("Activating Motor " + motor.ToString());
		rend.material.color = activatedColor;
       	vibrating = true;
    }

	void OnTriggerExit(Collider other)
	{
        // Stio vibrating
		vibrating = false;
		rend.material.color = currentColor;
		print("Deactivating Motor " + motor.ToString());
		LibRoukaVici.Vibrate((char)motor, (char)0);
	}
}