using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SendingToArduino : MonoBehaviour {
	public static SerialPort sp = new SerialPort("COM4", 9600);

	// Use this for initialization
	void Start () {
		OpenConnection ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenConnection()
	{
		if (sp != null) {
			if (sp.IsOpen) {
				sp.Close ();
				print ("Closing port, because it was already in use.");
			} else {
				sp.Open ();
				sp.ReadTimeout = 16;
				print ("Port succesfully opened.");
			}
		}
		else if (sp.IsOpen)
			print ("Port is already in use.");
		else
			print ("Port is null");
	}

	void OnApplicationQuit()
	{
		sp.Close ();
	}

	public static void sendToArduino(char motorNb, bool vibrate)
	{
		print(motorNb);
		print((vibrate ? (char)1 : (char)0));
		sp.Write (new[] {motorNb, (vibrate ? (char)1 : (char)0)}, 0, 2);
	}
}
