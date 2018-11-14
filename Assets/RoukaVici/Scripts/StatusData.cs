using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusData : MonoBehaviour
{
	[SerializeField] GameObject connectedImage, errorImage, loadingImage;
	Animator connectedImageAnimator;
	void Start ()
	{
		connectedImage.SetActive(false);
		errorImage.SetActive(true);
		loadingImage.SetActive(false);

		connectedImageAnimator = loadingImage.GetComponent<Animator>();
	}
	
	void Update ()
	{
		
	}

	private IEnumerator ConnectionCoroutine()
	{
		if (LibRoukaVici.TryBluetoothConnection() == 0)
		{
			connectedImage.SetActive(true);
			Debug.Log("RoukaVici is connected");
		}
		else
		{
			errorImage.SetActive(true);
			Debug.Log("Something went wrong during connection");
		}

		connectedImageAnimator.StopPlayback();
		loadingImage.SetActive(false);
		yield return null;
	}

	public void Connect()
	{
		errorImage.SetActive(false);
		loadingImage.SetActive(true);

		connectedImageAnimator.Play("Rotation");
		StartCoroutine(ConnectionCoroutine());
	}
}
