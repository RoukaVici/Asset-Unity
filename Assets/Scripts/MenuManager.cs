using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
	[SerializeField]
	private GameObject MainMenu;

	[SerializeField]
	private GameObject PatternManagerMenu;

	void Start() {
		displayMainMenu();
	}

	public void displayMainMenu() {
		if (MainMenu)
			MainMenu.SetActive(true);
		if (PatternManagerMenu)
			PatternManagerMenu.SetActive(false);
	}

	public void displayPatternManagerMenu() {
		if (MainMenu)
			MainMenu.SetActive(false);
		if (PatternManagerMenu)
			PatternManagerMenu.SetActive(true);
	}
}
