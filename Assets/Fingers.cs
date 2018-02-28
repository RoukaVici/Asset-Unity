using UnityEngine;
using System.Collections;

public class Fingers {
	
	public int id;
	public int[] pattern;

	public int getId() {
		return this.id;
	}

	public int[] getPattern() {
		return this.pattern;
	}
	
	public void setId(int id) {
		this.id = id;
	}

	public void setPattern(int[] pattern) {
		this.pattern = pattern;
	}
}

