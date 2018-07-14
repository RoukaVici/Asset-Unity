using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoukaViciController : MonoBehaviour
{
	public Color selectedItemColor;
	private Color currentItemColor;
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

	public List<VibrationStyle> vibrationPatterns = new List<VibrationStyle>();
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
		patternButtons[patternID].GetComponent<PatternData>().background.color = currentItemColor;
		patternID = newID;
		patternButtons[patternID].GetComponent<PatternData>().background.color = selectedItemColor;
	}

	public void initializeUI()
	{
		PatternData data = patternButtons[patternID].GetComponent<PatternData>();
		currentItemColor = data.background.color;
		data.background.color = selectedItemColor;
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
