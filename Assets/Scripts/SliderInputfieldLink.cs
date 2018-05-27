using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderInputfieldLink : MonoBehaviour {
	[SerializeField]
	InputField inputFieldIntensity;

	[SerializeField]
	Slider sliderIntensity;

	void Start() {
		if (inputFieldIntensity && sliderIntensity) {
			inputFieldIntensity.onValueChanged.AddListener(delegate {InputFieldUpdate();});
			sliderIntensity.onValueChanged.AddListener(delegate {SliderUpdate();});
		}
	}

	private void SliderUpdate() {
		inputFieldIntensity.text = sliderIntensity.value.ToString();
	}

	private void InputFieldUpdate() {
		int value = 0;
		if (inputFieldIntensity.text.Length > 0 && inputFieldIntensity.text.ToCharArray()[0] == '-')
			inputFieldIntensity.text = "0";
		else if (inputFieldIntensity.text.Length > 0)
			value = int.Parse(inputFieldIntensity.text);
		sliderIntensity.value = Mathf.Clamp(value, 0, 100);
	}
}
