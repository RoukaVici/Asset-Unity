using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEditor;
using System.IO;

public class VibrationStyle {

	public string name;
	public float delay;
	public Fingers[] fingers;

	public string getName() {
		return this.name;
	}

	public float getDelay() {
		return this.delay;
	}

	public Fingers getFingerById(int id) {
		Debug.Log (this.fingers);
		return this.fingers [id];
	}

}
