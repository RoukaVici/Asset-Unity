using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class PatternGenerator : MonoBehaviour {
	private static PatternGenerator _instance;
	public static PatternGenerator instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<PatternGenerator>();
			}
			return _instance;
		}
	}

	[SerializeField]
	private Transform Menu;

	[SerializeField]
	private GameObject slotPrefab;

	private List<VibrationStyle> vibrations = new List<VibrationStyle>();
	public GameObject[] slots;

	void Awake() {
		if (_instance == null)
			_instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	private void getFiles(string folder) {
		DirectoryInfo dir = new DirectoryInfo(folder);
		FileInfo[] info = dir.GetFiles("*.json");

		int i = 0;
		foreach (FileInfo f in info) {
			string data = File.ReadAllText(info[i].FullName);
			this.vibrations.Add(JsonUtility.FromJson<VibrationStyle> (data));
			int k = 2;
			while (k < 11) {
				string str = '{' + data.Split('{')[k];
				this.vibrations[i].addFinger(JsonUtility.FromJson<Fingers>(str.Remove(str.Length - 3)));
				k += 1;
			}
			i += 1;
		}
	}

	// Use this for initialization
	void Start () {
		this.getFiles("Patterns");

		int i = 0;
		foreach (VibrationStyle vs in this.vibrations) {
			GameObject button = Instantiate(slotPrefab);
			button.transform.localPosition = new Vector3(0, 170 - (i + 1) * button.GetComponent<RectTransform>().rect.height, 0);
			button.transform.SetParent(Menu, false);
			button.GetComponentInChildren<Text>().text = vs.getName();
			button.GetComponent<PatternData>().ID = i;
			
			//cubes[i].AddComponent<ActivatePattern>().addPatern(vs.getName(), vs.getFingers());

			i += 1;
		}
		//		this.vibrations = JsonUtility.FromJson<VibrationStyle>(data);
	}
}
