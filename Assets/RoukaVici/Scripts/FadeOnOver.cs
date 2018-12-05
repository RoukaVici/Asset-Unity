using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FadeOnOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] Animation[] contentToFade;

    public void OnPointerEnter(PointerEventData eventData)
    {
		foreach (Animation anim in contentToFade)
		{
			if (anim.gameObject.GetComponent<Image>().color.a == 0)
				anim.Play("FadeIn");
		}
    }

    public void OnPointerExit(PointerEventData pointerEventData)
	{
		foreach (Animation anim in contentToFade)
			anim.Play("FadeOut");
	}
}
