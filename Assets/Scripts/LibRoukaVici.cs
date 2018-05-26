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
  unsafe private static extern int Write(char[] msg);
  [DllImport ("roukavici")]
  private static extern int Vibrate(char motor, char intensity);
  [DllImport ("roukavici")]
  unsafe private static extern int NewGroup(char[] name);
  [DllImport ("roukavici")]
  unsafe private static extern int AddToGroup(char[] name, char motor);
  [DllImport ("roukavici")]
  unsafe private static extern int RmFromGroup(char[] name, char motor);
  [DllImport ("roukavici")]
  unsafe private static extern void VibrateGroup(char[] name, char intensity);
  [DllImport ("roukavici")]
  unsafe private static extern int ChangeDeviceManager(char[] name);

  void Start() {
    /**
    * Automatically starts trying to find the device as well
    * Return codes:
    * 0: Success
    * 1: Lib initialized, but could not find device
    */
    //Must be checked someday
//    Debug.Log("Init Roukavici library");
    Debug.Log(InitRVici());
//    Debug.Log("Changing Device Manager to BTManager");
    FindDevice();
    Vibrate((char)0, (char)255);
//    Debug.Log("Init Roukavici OK");
  }

  void OnApplicationQuit() {
//    Debug.Log("Roukavici library shutting down");
    StopRVici();
//    Debug.Log("Roukavici library shutdown: OK");
  }
}
