using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Generates slots for the UI by reading json pattern files
/// </summary>
[RequireComponent(typeof(MenuManager))]
public class PatternGenerator : MonoBehaviour
{
    /// <summary>
    /// Read the content from a given folder
    /// </summary>
    /// <param name="folder">The folder path to read</param>
	private void GetFiles(string folder)
	{
		DirectoryInfo dir = new DirectoryInfo(folder);
		FileInfo[] info = dir.GetFiles("*.json");

        int patternNbLimit = 10;
		foreach (FileInfo f in info)
		{
            if (patternNbLimit-- < 0)
                break ;
			string data = File.ReadAllText(f.FullName);
			string arrayData = data.Remove(1, data.IndexOf("\"motors\"") - 1);
			VibrationPattern vs = JsonUtility.FromJson<VibrationPattern>(data);
			vs.motors = JsonHelper.FromJson<Motor>(arrayData);
			RoukaViciController.instance.vibrationPatterns.Add(vs);
		}
		if (RoukaViciController.instance.vibrationPatterns.Count == 0)
		{
            // If no file were found, create a default one
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
