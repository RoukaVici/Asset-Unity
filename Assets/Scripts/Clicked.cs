using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicked : MonoBehaviour {
	[SerializeField]
	private Color activatedColor;
	public int motorNb;
	public string assignedKey;
	private bool vibrating = false;
	public Toggle emulatorEnabled;
	private Color currentColor;

	// Use this for initialization
	void Start () {
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
			{
				print("Emulating activation of Motor " + motorNb.ToString());
			}
			else
			{
				print("Activating Motor " + motorNb.ToString());
				//SendingToArduino.sendToArduino ((char)motorNb, vibrating);
			}
		} 
		else if (Input.GetKeyUp (assignedKey))
		{
			vibrating = false;
			GetComponent<Renderer> ().material.color = currentColor;
			if (emulatorEnabled.isOn)
			{
				print("Emulating deactivation of Motor " + motorNb.ToString());
			}
			else
			{
				print("Deactivating Motor " + motorNb.ToString());
				//SendingToArduino.sendToArduino ((char)motorNb, vibrating);
			}
		}
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			vibrating = !vibrating;
			GetComponent<Renderer> ().material.color = (vibrating ? Color.green : Color.red);
			if (emulatorEnabled.isOn)
			{
				print ((vibrating ? "Emulating activation of Motor " : "Emulating deactivation of Motor ") + motorNb.ToString());
			}
			else
			{
				print ((vibrating ? "Activating Motor " : "Deactivating Motor ") + motorNb.ToString());
				//SendingToArduino.sendToArduino ((char)motorNb, vibrating);
			}
		}
	}
}