using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ActivatePattern : MonoBehaviour {

	private bool vibrating;
	private string mName;
	private int counter;

	public static SerialPort sp = new SerialPort("COM4", 9600);


	public Fingers[] fingers = new Fingers[10];

	public int delay = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.vibrating) {
			this.counter = 0;
			this.delay = 0;
			return;
		}
		if (this.delay >= 100) {
			int i = 0;
			while (i < this.fingers.Length - 1) {
				Debug.Log ("sending " + this.fingers[i].pattern[this.counter] + " to " + this.fingers [i].id);
				i += 1;
			}
			this.counter += 1;
			this.delay = 0;
		}
		this.delay += 1;
			// CALL ROUKAVICI LIB HERE
			//		while (i < this.fingers.GetLength(1)) {
			//SendingToArduino.sendToArduino ((char)this.fingers[i].getId(), this.fingers[i].getPatternById(this.counter));
			// SendingToArduino.sendToArduino ((char)this.fingers[i].getId(), true);
//			i += 1;
//		}
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.vibrating = !this.vibrating;
			GetComponent<TextMesh> ().color = (vibrating ? Color.green : Color.red);
			Debug.Log ((this.vibrating ? "Activating pattern " : "Deactivating pattern ") + this.name);
			// CALL ROUKAVICI LIB HERE
//			SendingToArduino.sendToArduino ((char)motorNb, vibrating);
		}
	}

	public void addPatern(string name, Fingers[] fingers) {
		this.name = name;
		this.fingers = fingers;
	}
}
