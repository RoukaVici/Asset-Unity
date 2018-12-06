using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Replace an Image's Sprite when hovered
/// </summary>
[RequireComponent(typeof(Image))]
public class ReplaceImageOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] Sprite texture;
	Image image;
	Sprite previousTexture;
	Animation anim;

	void Start()
	{
		image = GetComponent<Image>();
		anim = GetComponent<Animation>();
		previousTexture = image.sprite;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
		if (anim)
			anim.Stop();
		image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
		image.sprite = texture;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
	{
		image.sprite = previousTexture;
	}
}
