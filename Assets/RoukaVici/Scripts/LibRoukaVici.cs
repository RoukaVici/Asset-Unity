using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// A library of static functions calling RoukaVici's functions
/// </summary>
public class LibRoukaVici : MonoBehaviour
{
    [DllImport ("roukavici")]
    public static extern int InitRVici();
    [DllImport ("roukavici")]
    public static extern int FindDevice();
    [DllImport ("roukavici")]
    public static extern int Status();
    [DllImport ("roukavici")]
    public static extern void StopRVici();
    [DllImport ("roukavici")]
    public static extern int Write(char[] msg);
    [DllImport ("roukavici")]
    public static extern int NewGroup(char[] name);
    [DllImport ("roukavici")]
    public static extern int AddToGroup(char[] name, char motor);
    [DllImport ("roukavici")]
    public static extern int RmFromGroup(char[] name, char motor);
    [DllImport ("roukavici")]
    public static extern void VibrateGroup(char[] name, char intensity);
    [DllImport ("roukavici")]
    public static extern int ChangeDeviceManager(int dm);
    [DllImport ("roukavici")]
    public static extern int Vibrate(char motor, char intensity);
    [DllImport ("roukavici")]
    public static extern void SetLogMode(int mode);

    [DllImport ("roukavici")]
    private static extern void RegisterUnityDebugCallback(UnityDebugCallback callback);
    private delegate void UnityDebugCallback(string message);
    private static void UnityDebugMethod(string message)
    {
        Debug.Log("libroukavici: " + message);
    }
  	private static LibRoukaVici _instance;
    public static LibRoukaVici instance
	{
		get
        {
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<LibRoukaVici>();
			}
			return _instance;
		}
	}

    void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

    void CallbackDebug(char[] str)
    {
        Debug.Log(str);
    }

    void Start()
    {
        Debug.Log("Init Roukavici library...");
        Debug.Log("Init Roukavici OK" + (InitRVici() != 0 ? " but no device found." : "."));
    }

    void OnApplicationQuit()
    {
        Debug.Log("Roukavici library shutting down...");
        StopRVici();
        Debug.Log("Roukavici library shutdown: OK.");
    }

    /// <summary>
    /// Change the device manager to 2 (Bluetooth) and looks for devices
    /// </summary>
    /// <returns></returns>
    public static int TryBluetoothConnection()
    {
        ChangeDeviceManager(2);
	    return(FindDevice());
    }
}
