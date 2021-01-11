using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class ConnectionModel : MonoBehaviour
{
    
    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRandom()
    {
        PhotonNetwork.CreateRoom(null);
    }

    internal void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
}
