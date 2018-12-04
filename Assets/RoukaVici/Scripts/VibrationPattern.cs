using UnityEngine;
using System.Collections;
using System.IO;

public class VibrationPattern
{

	public string name;
	public float duration;
	public Motor[] motors = new Motor[(int)motorID.LAST_MOTOR];

	public string getName()
	{
		return this.name;
	}

	public float getduration()
	{
		return this.duration;
	}

	public Motor[] getMotor()
	{
		return this.motors;
	}

	public Motor getFingerById(int id)
	{
		Debug.Log (this.motors);
		return this.motors[id];
	}

	public void addFinger(Motor motor)
	{
		this.motors[motor.id] = new Motor();
		this.motors[motor.id].id = motor.id;
		this.motors[motor.id].pattern = motor.pattern;
	}
}
