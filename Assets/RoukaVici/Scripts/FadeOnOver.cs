using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Activate a Fade animation whether you hover or unhover a UI element
/// </summary>
public class FadeOnOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] Animation[] contentToFade;

    /// <summary>
    /// Play the Fade In animation when mouse hovers the object
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
		foreach (Animation anim in contentToFade)
		{
			if (anim.gameObject.GetComponent<Image>().color.a == 0)
				anim.Play("FadeIn");
		}
    }

    /// <summary>
    /// Play the Fade Out animation when mouse unhovers the object
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerExit(PointerEventData pointerEventData)
	{
		foreach (Animation anim in contentToFade)
			anim.Play("FadeOut");
	}
}
