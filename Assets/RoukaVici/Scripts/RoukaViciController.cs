using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoukaViciController : MonoBehaviour
{
	private static RoukaViciController _instance;
	public static RoukaViciController instance
	{
		get {
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<RoukaViciController>();
			}
			return _instance;
		}
	}

	public List<VibrationPattern> vibrationPatterns = new List<VibrationPattern>();
	public List<GameObject> patternButtons = new List<GameObject>();
	public int patternID = 0;

	void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public void setVibrationPattern(int newID)
	{
		newID = newID < 0 ? 0 : newID;
		if (newID == patternID)
			return ;
		if (MenuManager.instance != null)
			MenuManager.instance.SelectPatternButton(newID, patternID);
		patternID = newID;
	}
}
