using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds references to every required for the other components.
/// This script will make the gameobject persistent through scenes.
/// </summary>
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

    /// <summary>
    /// Selects the pattern to use for the next vibrations
    /// </summary>
    /// <param name="newID">The ID of the newly selected pattern</param>
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
