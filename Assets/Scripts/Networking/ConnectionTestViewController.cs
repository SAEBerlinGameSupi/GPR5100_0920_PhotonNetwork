using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionTestViewController : MonoBehaviour
{
    [SerializeField] ConnectionModel connectionModel;

    private void OnGUI()
    {
        GUILayout.Label("State: " + PhotonNetwork.NetworkClientState);

        switch (PhotonNetwork.NetworkClientState)
        {
            case ClientState.PeerCreated:
                if (GUILayout.Button("Connect"))
                {
                    connectionModel.ConnectToServer();
                }
                break;

            case ClientState.ConnectedToMasterServer:
                if (GUILayout.Button("Create Random"))
                {
                    connectionModel.CreateRandom();
                }
                if (GUILayout.Button("Join Random Random"))
                {
                    connectionModel.JoinRandomRoom();
                }
                break;

            case ClientState.Joined:
                var room = PhotonNetwork.CurrentRoom;

                GUILayout.Label(room.Name);
                GUILayout.Label("Players: " + room.PlayerCount);
                GUILayout.Label("-----");
                foreach (var pair in room.Players)
                {
                    GUILayout.Label(pair.Key +": " + pair.Value.NickName  + ("(MasterClient)"));
                }
                GUILayout.Label("-----");

                break;
        }


    }

}
