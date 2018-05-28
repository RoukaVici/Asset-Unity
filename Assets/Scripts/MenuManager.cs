using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject PatternList;

	[SerializeField]
	private GameObject PatternEditorMenu;

	[SerializeField]
	private PatternEditorData patternEditorData;

	public GameObject slotPrefab;

	public RectTransform scrollViewContent;

	void Start()
	{
		displayPatternList();
	}

	public void displayPatternList()
	{
		if (PatternList)
			PatternList.SetActive(true);
		if (PatternEditorMenu)
			PatternEditorMenu.SetActive(false);
	}

	private void displayPatternEditorMenu()
	{
		if (PatternList)
			PatternList.SetActive(false);
		if (PatternEditorMenu)
			PatternEditorMenu.SetActive(true);
	}

	public void addPattern()
	{
		VibrationStyle vs = new VibrationStyle();
		vs.name = "My Pattern";
		vs.delay = 0.3f;
		for (int i = 0 ; i < vs.fingers.Length ; ++i)
		{
			vs.fingers[i] = new Fingers();
			vs.fingers[i].id = i;
			vs.fingers[i].pattern = new List<int>();
			vs.fingers[i].pattern.Add(50);
		}
		RoukaViciController.instance.vibrationPatterns.Add(vs);
		editPattern(RoukaViciController.instance.vibrationPatterns.Count - 1);
	}

	public void removePattern(int id)
	{
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
			RoukaViciController.instance.patternButtons[0].GetComponent<PatternData>().background.color = RoukaViciController.instance.selectedItemColor;
		}
		if (id != RoukaViciController.instance.patternButtons.Count)
			rearrangeButtons();
	}

	private void rearrangeButtons()
	{
		int i = 0;
		foreach (GameObject b in RoukaViciController.instance.patternButtons)
		{
			b.transform.localPosition = new Vector3(0, 170 - (i + 1) * b.GetComponent<RectTransform>().rect.height, 0);
			b.GetComponent<PatternData>().ID = i;
			++i;
		}
	}

	public void editPattern(int editID)
	{
		displayPatternEditorMenu();
		patternEditorData.prepareEditor(editID);
		patternEditorData.displayIteration();
	}

	public void addPatternSlot(VibrationStyle vs)
	{
		GameObject button = Instantiate(slotPrefab);
		button.transform.localPosition = new Vector3(0, 170 - RoukaViciController.instance.vibrationPatterns.Count * button.GetComponent<RectTransform>().rect.height, 0);
		button.transform.SetParent(scrollViewContent, false);
		button.GetComponentInChildren<Text>().text = vs.getName();
		PatternData data = button.GetComponent<PatternData>();
		data.ID = RoukaViciController.instance.vibrationPatterns.Count - 1;
		data.selectPattern.onClick.AddListener(delegate {RoukaViciController.instance.setVibrationPattern(data.ID);});
		data.editPattern.onClick.AddListener(delegate {editPattern(data.ID);});
		data.removePattern.onClick.AddListener(delegate {removePattern(data.ID);});
		RoukaViciController.instance.patternButtons.Add(button);
	}
}
