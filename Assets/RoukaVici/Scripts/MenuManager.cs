using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PatternGenerator))]
public class MenuManager : MonoBehaviour
{
	private static MenuManager _instance;
	public static MenuManager instance
	{
		get {
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<MenuManager>();
			}
			return _instance;
		}
	}

	[SerializeField] KeyCode toggleKey;
	bool isVisible = false, isEditing = false;
	
	[SerializeField] Color selectedItemColor;
	Color currentItemColor;

	[SerializeField] GameObject patternListMenu;
	Animation patternListAnim;

	[SerializeField] GameObject patternEditorMenu;
	Animation patternEditorAnim;

	[SerializeField] PatternEditorData patternEditorData;
	public int patternNbLimit = 20;

	public GameObject slotPrefab;

	public RectTransform scrollViewContent;


	void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		patternListAnim = patternListMenu.GetComponent<Animation>();
		patternEditorAnim = patternEditorMenu.GetComponent<Animation>();
	}

	public void DisplayPatternList()
	{
		isEditing = false;
		patternListAnim.Play("SlideInLeft");
		patternEditorAnim.Play("SlideOutDown");
	}

	private void DisplayPatternEditorMenu()
	{
		isEditing = true;
		patternEditorAnim.Play("SlideInDown");
		patternListAnim.Play("SlideOutLeft");
	}

	public void AddPattern()
	{
		if (RoukaViciController.instance.patternButtons.Count >= patternNbLimit)
			return ;
		VibrationPattern vs = new VibrationPattern();
		vs.name = "My Pattern";
		vs.duration = 0.3f;
		for (int i = 0 ; i < vs.motors.Length ; ++i)
		{
			vs.motors[i] = new Motor();
			vs.motors[i].id = i;
			vs.motors[i].pattern = new List<int>();
			vs.motors[i].pattern.Add(50);
		}
		RoukaViciController.instance.vibrationPatterns.Add(vs);
		EditPattern(RoukaViciController.instance.vibrationPatterns.Count - 1);
	}

	public void RemovePattern(int id)
	{
		if (RoukaViciController.instance.patternButtons.Count <= 1)
			return;
		# if UNITY_STANDALONE_WIN
			if (File.Exists("Patterns\\" + RoukaViciController.instance.vibrationPatterns[id].name + ".json"))
				File.Delete("Patterns\\" + RoukaViciController.instance.vibrationPatterns[id].name + ".json");
		# else
			if (File.Exists("Patterns/" + RoukaViciController.instance.vibrationPatterns[id].name + ".json"))
				File.Delete("Patterns/" + RoukaViciController.instance.vibrationPatterns[id].name + ".json");
		# endif
		Destroy(RoukaViciController.instance.patternButtons[id]);
		RoukaViciController.instance.patternButtons.RemoveAt(id);
		RoukaViciController.instance.vibrationPatterns.RemoveAt(id);
		if (RoukaViciController.instance.patternID == id)
		{
			RoukaViciController.instance.patternID = 0;
			RoukaViciController.instance.patternButtons[0].GetComponent<PatternData>().background.color = selectedItemColor;
		}
		if (id != RoukaViciController.instance.patternButtons.Count)
			RearrangeButtons();
	}

	public void RearrangeButtons()
	{
		int i = 0;
		foreach (GameObject b in RoukaViciController.instance.patternButtons)
		{
			b.GetComponent<PatternData>().ID = i;
			++i;
		}
	}

	public void EditPattern(int editID)
	{
		DisplayPatternEditorMenu();
		patternEditorData.PrepareEditor(editID);
		patternEditorData.DisplayIteration();
	}

	public void InitializeUI()
	{
		PatternData data = RoukaViciController.instance.patternButtons[0].GetComponent<PatternData>();
		currentItemColor = data.background.color;
		data.background.color = selectedItemColor;
	}

	public void SelectPatternButton(int newID, int oldID)
	{
		RoukaViciController.instance.patternButtons[oldID].GetComponent<PatternData>().background.color = currentItemColor;
		RoukaViciController.instance.patternButtons[newID].GetComponent<PatternData>().background.color = selectedItemColor;
	}

	public void AddPatternSlot(VibrationPattern vs)
	{
		GameObject button = Instantiate(slotPrefab);
		button.transform.SetParent(scrollViewContent, true);
		button.GetComponentInChildren<Text>().text = vs.getName();
		PatternData data = button.GetComponent<PatternData>();
		data.selectPattern.onClick.AddListener(delegate {RoukaViciController.instance.setVibrationPattern(data.ID);});
		data.editPattern.onClick.AddListener(delegate {EditPattern(data.ID);});
		data.removePattern.onClick.AddListener(delegate {RemovePattern(data.ID);});
		RoukaViciController.instance.patternButtons.Add(button);
		RearrangeButtons();
	}

	private void ToggleUI()
	{
		if (isVisible)
		{
			// Display
			if (isEditing)
				patternEditorAnim.Play("SlideOutDown");
			else
				patternListAnim.Play("SlideOutLeft");
		}
		else
		{
			// Hide
			if (isEditing)
				patternEditorAnim.Play("SlideInDown");
			else
				patternListAnim.Play("SlideInLeft");
		}
		isVisible = !isVisible;
	}

	void Update()
	{
		if (Input.GetKeyDown(toggleKey))
		{
			Debug.Log("TOGGLE");
			ToggleUI();
		}
	}
}
