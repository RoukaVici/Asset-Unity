using UnityEngine;
using System.Collections;

public class Patterns {

	private Fingers[] fingers;
	private int delay;
	private string name;

	public Fingers[] getFingers() {
		return this.fingers;
	}

	public Fingers getFinger(int id) {
		return this.fingers[id];
	}

	public void setFingers(Fingers[] fingers) {
		this.fingers = fingers;
	}

	public int getDelay() {
		return this.delay;
	}

	public string getName() {
		return this.name;
	}
}
