using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class ConnectionModel : MonoBehaviour
{   
    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom(string name)
    {
        PhotonNetwork.CreateRoom(name);
    }

    internal void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void RenameLocalPlayerTo(string newName){
        PhotonNetwork.LocalPlayer.NickName = newName;
    }
}
