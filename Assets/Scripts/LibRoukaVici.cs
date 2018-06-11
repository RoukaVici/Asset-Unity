using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class LibRoukaVici : MonoBehaviour {
    [DllImport ("roukavici")]
    private static extern int InitRVici();
    [DllImport ("roukavici")]
    private static extern int FindDevice();
    [DllImport ("roukavici")]
    private static extern int Status();
    [DllImport ("roukavici")]
    private static extern void StopRVici();
    [DllImport ("roukavici")]
    private static extern int Write(char[] msg);
    [DllImport ("roukavici")]
    private static extern int NewGroup(char[] name);
    [DllImport ("roukavici")]
    private static extern int AddToGroup(char[] name, char motor);
    [DllImport ("roukavici")]
    private static extern int RmFromGroup(char[] name, char motor);
    [DllImport ("roukavici")]
    private static extern void VibrateGroup(char[] name, char intensity);
    [DllImport ("roukavici")]
    private static extern int ChangeDeviceManager(char[] name);
    [DllImport ("roukavici")]
    public static extern int Vibrate(char motor, char intensity);

    # if UNITY_STANDALONE_WIN
        [DllImport ("roukavici")]
        private static extern void RegisterUnityDebugCallback(DebugCallback callback);

        [DllImport ("roukavici")]
        public static extern void SetLogMode(int mode);

        private delegate void DebugCallback(string message);

        private static void UnityDebugMethod(string message)
        {
         Debug.Log("roukavici.dll: " + message);
        }
    # endif

  	private static LibRoukaVici _instance;
    public static LibRoukaVici instance
	{
		get {
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

    void Start()
    {
        Debug.Log("Init Roukavici library...");
        Debug.Log("Init Roukavici OK" + (InitRVici() != 0 ? " but no device found." : "."));
        # if UNITY_STANDALONE_WIN
            RegisterUnityDebugCallback(new DebugCallback(UnityDebugMethod));
            SetLogMode(3);
        # endif
    }

    int Write(string name)
    {
        return Write(name.ToCharArray());
    }

    int NewGroup(string name)
    {
        return NewGroup(name.ToCharArray());
    }

    int AddToGroup(string name, char motor)
    {
        return AddToGroup(name.ToCharArray(), motor);
    }

    int RmFromGroup(string name, char motor)
    {
        return RmFromGroup(name.ToCharArray(), motor);
    }

    void VibrateGroup(string name, char intensity)
    {
        VibrateGroup(name.ToCharArray(), intensity);
    }

    int ChangeDeviceManager(string name)
    {
        return ChangeDeviceManager(name.ToCharArray());
    }

    void OnApplicationQuit()
    {
        Debug.Log("Roukavici library shutting down...");
        StopRVici();
        Debug.Log("Roukavici library shutdown: OK.");
  }
}
