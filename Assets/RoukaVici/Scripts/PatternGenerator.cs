using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(MenuManager))]
public class PatternGenerator : MonoBehaviour
{
	private void GetFiles(string folder)
	{
		DirectoryInfo dir = new DirectoryInfo(folder);
		FileInfo[] info = dir.GetFiles("*.json");

		foreach (FileInfo f in info)
		{
			string data = File.ReadAllText(f.FullName);
			string arrayData = data.Remove(1, data.IndexOf("\"motors\"") - 1);
			VibrationPattern vs = JsonUtility.FromJson<VibrationPattern>(data);
			vs.motors = JsonHelper.FromJson<Motor>(arrayData);
			RoukaViciController.instance.vibrationPatterns.Add(vs);
		}
		if (RoukaViciController.instance.vibrationPatterns.Count == 0)
		{
			VibrationPattern vs = new VibrationPattern();
			vs.duration = 0.5f;
			vs.name = "Default";
			int i = 0;
			foreach (Motor f in vs.motors)
			{
				f.id = i++;
				f.pattern.Add(50);
			}
			RoukaViciController.instance.vibrationPatterns.Add(vs);
		}
	}

	void Start ()
	{
		this.GetFiles("Vibration Patterns");

		MenuManager menuManager = GetComponent<MenuManager>();
		float slotHeight = menuManager.slotPrefab.GetComponent<RectTransform>().rect.height;
		menuManager.scrollViewContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, slotHeight * (RoukaViciController.instance.vibrationPatterns.Count + 1));

		int i = 0;
		foreach (VibrationPattern vs in RoukaViciController.instance.vibrationPatterns)
		{
			GameObject button = Instantiate(menuManager.slotPrefab);
			button.transform.SetParent(menuManager.scrollViewContent, false);
			button.GetComponentInChildren<Text>().text = vs.getName();
			PatternData data = button.GetComponent<PatternData>();
			data.ID = i;
			data.selectPattern.onClick.AddListener(delegate {RoukaViciController.instance.setVibrationPattern(data.ID);});
			data.editPattern.onClick.AddListener(delegate {menuManager.EditPattern(data.ID);});
			data.removePattern.onClick.AddListener(delegate {menuManager.RemovePattern(data.ID);});
			RoukaViciController.instance.patternButtons.Add(button);
			i += 1;
		}
		menuManager.InitializeUI();
	}
}
