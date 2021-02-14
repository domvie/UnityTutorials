using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyHUD : NetworkManager
{
    private NetworkManager networkManager;

    public void MyStartHost() {
        Debug.Log("Starting Host at " + Time.timeSinceLevelLoad);
        StartHost();
    }

    override public void OnStartHost() {
        Debug.Log("Host started at " + Time.timeSinceLevelLoad);
    }

  public override void OnStartClient(NetworkClient myClient)
  {
        Debug.Log(Time.timeSinceLevelLoad + " Client start requested");
        InvokeRepeating("PrintDots", 0f, 1f);
  }

  public override void OnClientConnect(NetworkConnection conn)
  {
    Debug.Log(Time.timeSinceLevelLoad + " Client is connected to IP: " + conn.address);
    CancelInvoke();
  }

  void PrintDots() {
      Debug.Log(".");
  }
}
