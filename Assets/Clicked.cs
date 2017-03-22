using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicked : MonoBehaviour {
	public char motorNb;
	public string assignedKey;
	private bool vibrating = false;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (assignedKey))
		{
			vibrating = true;
			GetComponent<Renderer> ().material.color = Color.green;
			print ("Activating Motor " + motorNb);
			SendingToArduino.sendToArduino (motorNb, vibrating);
		} 
		else if (Input.GetKeyUp (assignedKey))
		{
			vibrating = false;
			GetComponent<Renderer> ().material.color = Color.red;
			print("Deactivating Motor " + motorNb);
			SendingToArduino.sendToArduino (motorNb, vibrating);
		}
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			vibrating = !vibrating;
			GetComponent<Renderer> ().material.color = (vibrating ? Color.green : Color.red);
			print ((vibrating ? "Activating Motor " : "Deactivating Motor ") + motorNb);
			SendingToArduino.sendToArduino (motorNb, vibrating);
		}
	}
}