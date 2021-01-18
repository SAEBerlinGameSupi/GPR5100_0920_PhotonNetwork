using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionTestViewController : MonoBehaviour
{
    [SerializeField] ConnectionModel connectionmodel;

    private string _nickName = "Flo";

    private void OnGUI()
    {
        GUILayout.Label("State: " + PhotonNetwork.NetworkClientState);

        switch (PhotonNetwork.NetworkClientState)
        {
            case ClientState.PeerCreated:
                if (GUILayout.Button("Connect"))
                {

                    connectionmodel.ConnectToServer();
                }
                break;

            case ClientState.ConnectedToMasterServer:
                if (GUILayout.Button("Create Random"))
                {
                    connectionmodel.CreateRoom("Test123");
                }
                break;
            case ClientState.Joined:
                var room = PhotonNetwork.CurrentRoom;

                GUILayout.Label(room.Name);
                GUILayout.Label("Players: " + room.PlayerCount);
                GUILayout.Label("-----");
                foreach (var pair in room.Players)
                {
                    GUILayout.Label(pair.Key + ": " + pair.Value.NickName + (pair.Value.IsMasterClient ? "(Masterclient)" : ""));
                }
                GUILayout.Label("-----");

                GUILayout.Label("LocalPlayer");
                GUILayout.Label("Name");
                _nickName = GUILayout.TextField(_nickName);
                connectionmodel.RenameLocalPlayerTo(_nickName);

                break;
        }
    }
}
