using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum motorID
{
	LEFT_PINKY = 0,
	LEFT_RING,
	LEFT_MIDDLE,
	LEFT_INDEX,
	LEFT_THUMB,
	RIGHT_THUMB,
	RIGHT_INDEX,
	RIGHT_MIDDLE,
	RIGHT_RING,
	RIGHT_PINKY,
	LAST_MOTOR
}

[Serializable]
public class Motor
{
	public int id;
	public List<int> pattern;

	public int GetId()
	{
		return this.id;
	}

	public List<int> GetPattern()
	{
		return this.pattern;
	}

	public int GetPatternById(int id)
	{
		return this.pattern[id];
	}
	
	public void SetId(int id)
	{
		this.id = id;
	}

	public void SetPattern(List<int> pattern)
	{
		this.pattern = pattern;
	}
}

