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

  void Start() {
    Debug.Log("Init Roukavici library...");
    Debug.Log("Init Roukavici OK" + (InitRVici() != 0 ? " but no device found." : "."));
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

  void OnApplicationQuit() {
    Debug.Log("Roukavici library shutting down...");
    StopRVici();
    Debug.Log("Roukavici library shutdown: OK.");
  }
}
