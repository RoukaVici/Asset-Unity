using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(MenuManager))]
public class PatternGenerator : MonoBehaviour
{
	private void getFiles(string folder)
	{
		DirectoryInfo dir = new DirectoryInfo(folder);
		FileInfo[] info = dir.GetFiles("*.json");

		foreach (FileInfo f in info) {
			string data = File.ReadAllText(f.FullName);
			string arrayData = data.Remove(1, data.IndexOf("\"fingers\"") - 1);
			VibrationStyle vs = JsonUtility.FromJson<VibrationStyle>(data);
			vs.fingers = JsonHelper.FromJson<Fingers>(arrayData);
			RoukaViciController.instance.vibrationPatterns.Add(vs);
		}
	}

	// Use this for initialization
	void Start ()
	{
		MenuManager menuManager = GetComponent<MenuManager>();
		if (RoukaViciController.instance.vibrationPatterns.Count == 0)
			this.getFiles("Patterns");

		int i = 0;
		foreach (VibrationStyle vs in RoukaViciController.instance.vibrationPatterns)
		{
			GameObject button = Instantiate(menuManager.slotPrefab);
			button.transform.SetParent(menuManager.scrollViewContent, false);
			button.transform.localPosition = new Vector3(0, 170 - (i + 1) * button.GetComponent<RectTransform>().rect.height, 0);
			button.GetComponentInChildren<Text>().text = vs.getName();
			PatternData data = button.GetComponent<PatternData>();
			data.ID = i;
			data.selectPattern.onClick.AddListener(delegate {RoukaViciController.instance.setVibrationPattern(data.ID);});
			data.editPattern.onClick.AddListener(delegate {menuManager.editPattern(data.ID);});
			data.removePattern.onClick.AddListener(delegate {menuManager.removePattern(data.ID);});
			RoukaViciController.instance.patternButtons.Add(button);
			i += 1;
		}
		RoukaViciController.instance.initializeUI();
	}
}
