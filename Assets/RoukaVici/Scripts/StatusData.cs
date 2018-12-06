using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StatusData : MonoBehaviour
{
	enum ConnectionStatus {Connected, Connecting, Disconnected};
	ConnectionStatus current = ConnectionStatus.Disconnected;
	[SerializeField] GameObject connectedImage, errorImage, loadingImage;
	Animator connectedImageAnimator;
	Thread connection;
	bool threadWorking = false;
	void Start ()
	{
		connectedImage.SetActive(false);
		errorImage.SetActive(true);
		loadingImage.SetActive(false);

		connectedImageAnimator = loadingImage.GetComponent<Animator>();
	}
	
	void Update ()
	{
		if (current == ConnectionStatus.Connected)
		{
			connectedImage.SetActive(true);
			errorImage.SetActive(false);
			loadingImage.SetActive(false);
			connectedImageAnimator.StopPlayback();
			if (LibRoukaVici.Status() != 0)
				current = ConnectionStatus.Disconnected;
		}
		else if (current == ConnectionStatus.Disconnected)
		{
			connectedImage.SetActive(false);
			errorImage.SetActive(true);
			loadingImage.SetActive(false);
			connectedImageAnimator.StopPlayback();
		}
		else
		{
			connectedImage.SetActive(false);
			errorImage.SetActive(false);
			loadingImage.SetActive(true);
		}
	}

	private void ConnectionThread()
	{
		if (LibRoukaVici.TryBluetoothConnection() == 0)
		{
			current = ConnectionStatus.Connected;
			Debug.Log("RoukaVici is connected");
		}
		else
		{
			current = ConnectionStatus.Disconnected;
			Debug.Log("Something went wrong during connection");
		}
		threadWorking = false;
	}

	public void Connect()
	{
		if (threadWorking)
			return ;
		errorImage.SetActive(false);
		loadingImage.SetActive(true);

		connectedImageAnimator.Play("Rotation");
		connection = new Thread(ConnectionThread);
		threadWorking = true;
		current = ConnectionStatus.Connecting;
		connection.Start();
	}

	void OnDestroy()
	{
		if (threadWorking)
		{
			connection.Interrupt();
		}
	}
}
