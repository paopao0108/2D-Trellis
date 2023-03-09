using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameController : MonoBehaviourPun
{
    public static PlayerType winner;
    void Start()
    {
    }

    void Update()
    {
        
    }

    [PunRPC]
    public void SendGameOver(PlayerType winner)
    {
        Debug.Log("接受到Ring的消息");
        transform.Find("GameOverPanel").gameObject.SetActive(true);
        BroadcastMessage("GameOver", winner);
    }

}
