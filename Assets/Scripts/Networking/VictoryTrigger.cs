using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VictoryTrigger : MonoBehaviourPun
{
    PlayerController[] _players;
    private int _finishedPlayers;

    public void Start()
    {
        _players = new PlayerController[10]; // need to count players
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("IsPlayer");
            _players[_finishedPlayers] = other.gameObject.GetComponent<PlayerController>();
            _finishedPlayers++;

            Victory();
        }
    }

    private void Victory()
    {
        photonView.RPC("RPC_Victory", RpcTarget.All);
        Debug.Log("Victory");
    }

    [PunRPC]
    private void RPC_Victory()
    {
        foreach(PlayerController playerController in _players)
        {
            if(playerController != null)
            {
                playerController.UpdateVictoryUI();
            }
           
        }
    }
}
