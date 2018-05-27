using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class PatternGenerator : MonoBehaviour {

	[SerializeField]
	private Transform Menu;

	[SerializeField]
	private GameObject slotPrefab;

	private void getFiles(string folder) {
		DirectoryInfo dir = new DirectoryInfo(folder);
		FileInfo[] info = dir.GetFiles("*.json");

		int i = 0;
		foreach (FileInfo f in info) {
			string data = File.ReadAllText(info[i].FullName);
			RoukaViciController.instance.vibrationPatterns.Add(JsonUtility.FromJson<VibrationStyle> (data));
			int k = 2;
			while (k < 11) {
				string str = '{' + data.Split('{')[k];
				RoukaViciController.instance.vibrationPatterns[i].addFinger(JsonUtility.FromJson<Fingers>(str.Remove(str.Length - 3)));
				k += 1;
			}
			i += 1;
		}
	}

	// Use this for initialization
	void Start () {
		if (RoukaViciController.instance.vibrationPatterns.Count == 0)
			this.getFiles("Patterns");

		int i = 0;
		foreach (VibrationStyle vs in RoukaViciController.instance.vibrationPatterns) {
			GameObject button = Instantiate(slotPrefab);
			button.transform.localPosition = new Vector3(0, 170 - (i + 1) * button.GetComponent<RectTransform>().rect.height, 0);
			button.transform.SetParent(Menu, false);
			button.GetComponentInChildren<Text>().text = vs.getName();
			PatternData data = button.GetComponent<PatternData>();
			data.ID = i;
			data.selectPattern.onClick.AddListener(delegate {RoukaViciController.instance.setVibrationPattern(data.ID);});
			data.name = vs.getName();
			data.fingers = vs.getFingers();		
			RoukaViciController.instance.patternButtons.Add(button);
			i += 1;
		}
	}
}
