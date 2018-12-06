using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper != null ? wrapper.motors : null;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.motors = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.motors = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] motors;
    }
	
}

/// <summary>
/// A container that holds information regarding UI
/// elements required to edit a VibrationPattern object
/// </summary>
public class PatternEditorData : MonoBehaviour
{
	public int currentPatternID = 0;

	[SerializeField]
	private Dropdown dropdownIteration;

	[SerializeField]
	private InputField inputFieldPatternName;

	[SerializeField]
	private InputField inputFieldPatternDuration;

	[SerializeField]
	private SliderInputfieldLink[] motors = new SliderInputfieldLink[(int)motorID.LAST_MOTOR];

	[SerializeField]
	public VibrationPattern pattern;

    /// <summary>
    /// Updates every values to the first iteration's selected pattern
    /// </summary>
    /// <param name="editID">The ID of the pattern being edited</param>
	public void PrepareEditor(int editID)
	{
		dropdownIteration.value = 0;
		pattern = new VibrationPattern();
		pattern.name = RoukaViciController.instance.vibrationPatterns[editID].name;
		pattern.duration = RoukaViciController.instance.vibrationPatterns[editID].duration;
		int j = 0;
		foreach (Motor f in RoukaViciController.instance.vibrationPatterns[editID].motors)
		{
			pattern.motors[j] = new Motor();
			pattern.motors[j].pattern = new List<int>(f.pattern);
			pattern.motors[j].id = j;
			++j;
		}
		currentPatternID = editID;
		dropdownIteration.options.Clear();
		inputFieldPatternName.text = pattern.name;
		inputFieldPatternDuration.text = pattern.duration.ToString();
		int count = pattern.motors[0].pattern.Count;
		for (int i = 0 ; i < count ; ++i)
		{
			dropdownIteration.options.Add(new Dropdown.OptionData("Iteration " + (i + 1)));
		}
	}

    /// <summary>
    /// Update the values to the selected iteration
    /// </summary>
	public void DisplayIteration()
	{
		for (int i = 0 ; i < (int)motorID.LAST_MOTOR ; ++i)
		{
			motors[i].SetValues(pattern.motors[i].pattern[dropdownIteration.value]);
		}
	}

    /// <summary>
    /// Updates the json file with the new content.
    /// Delete the previous json file if names were changed
    /// </summary>
	public void SavePattern()
	{
		string oldFile = RoukaViciController.instance.vibrationPatterns[currentPatternID].name;
		RoukaViciController.instance.vibrationPatterns[currentPatternID].name = pattern.name;
		RoukaViciController.instance.vibrationPatterns[currentPatternID].duration = pattern.duration;
		RoukaViciController.instance.vibrationPatterns[currentPatternID].motors = pattern.motors;

		string data = JsonUtility.ToJson(pattern, true);
		int index = data.IndexOf("\"motors\"");
		data = data.Remove(index);

		string arrayData = JsonHelper.ToJson(pattern.motors, true);
		arrayData = arrayData.Remove(arrayData.IndexOf('{'), 1);
		data += arrayData;
		# if UNITY_STANDALONE_WIN
			if (oldFile != pattern.name && File.Exists("Vibration Patterns\\" + oldFile + ".json"))
				File.Delete("Vibration Patterns\\" + oldFile + ".json");
			File.WriteAllText("Vibration Patterns\\" + pattern.name + ".json", data);
		# else
			if (oldFile != pattern.name && File.Exists("Vibration Patterns/" + oldFile + ".json"))
				File.Delete("Vibration Patterns/" + oldFile + ".json");
			File.WriteAllText("Vibration Patterns/" + pattern.name + ".json", data);
		# endif

		MenuManager.instance.AddPatternSlot(RoukaViciController.instance.vibrationPatterns[currentPatternID]);
		PatternData d = RoukaViciController.instance.patternButtons[currentPatternID].GetComponent<PatternData>();
		d.patternName.text = pattern.name;
		MenuManager.instance.DisplayPatternList();
	}

	public void CancelEdit()
	{
		if (RoukaViciController.instance.vibrationPatterns.Count > RoukaViciController.instance.patternButtons.Count)
			RoukaViciController.instance.vibrationPatterns.RemoveAt(RoukaViciController.instance.vibrationPatterns.Count - 1);
		MenuManager.instance.DisplayPatternList();
	}

	public void UpdateName()
	{
		pattern.name = inputFieldPatternName.text;
	}

	public void Updateduration()
	{
		float v;
		if (inputFieldPatternDuration.text.ToCharArray()[0] == '-')
		{
			v = 0;
			inputFieldPatternDuration.text = "0";
		}
		else
			v = float.Parse(inputFieldPatternDuration.text);
		pattern.duration = v;
	}

	public void AddIteration() 
	{
		foreach(Motor f in pattern.motors)
		{
			f.pattern.Insert(dropdownIteration.value + 1, 50);
		}
		dropdownIteration.options.Add(new Dropdown.OptionData("Iteration " + (dropdownIteration.options.Count + 1)));
		dropdownIteration.value += 1;
		DisplayIteration();
	}

	public void RemoveIteration()
	{
		if (pattern.motors[0].pattern.Count == 1)
			return ;
		foreach (Motor f in pattern.motors)
		{
			f.pattern.RemoveAt(dropdownIteration.value);
		}
		dropdownIteration.options.RemoveAt(dropdownIteration.value);
		for (int i = dropdownIteration.value ; i < dropdownIteration.options.Count ; ++i)
			dropdownIteration.options[i].text = "Iteration " + (i + 1);
		if (dropdownIteration.value >= dropdownIteration.options.Count)
			dropdownIteration.value -= 1;
		DisplayIteration();
	}

	public int GetIteration()
	{
		return dropdownIteration.value;
	}
}
