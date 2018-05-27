using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
	[SerializeField]
	private GameObject PatternList;

	[SerializeField]
	private GameObject PatternEditorMenu;

	void Start() {
		displayPatternList();
	}

	public void displayPatternList() {
		if (PatternList)
			PatternList.SetActive(true);
		if (PatternEditorMenu)
			PatternEditorMenu.SetActive(false);
	}

	public void displayPatternEditorMenu() {
		if (PatternList)
			PatternList.SetActive(false);
		if (PatternEditorMenu)
			PatternEditorMenu.SetActive(true);
	}

	public void addPattern() {

	}
}
