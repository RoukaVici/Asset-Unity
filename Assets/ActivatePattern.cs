using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePattern : MonoBehaviour {

	private bool vibrating;
	private string name;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.vibrating = !this.vibrating;
			GetComponent<TextMesh> ().color = (vibrating ? Color.green : Color.red);
			Debug.Log ((this.vibrating ? "Activating pattern " : "Deactivating pattern ") + this.name);
//			SendingToArduino.sendToArduino ((char)motorNb, vibrating);
		}
	}

	public void addPatern(string name) {
		this.name = name;
	}
}
