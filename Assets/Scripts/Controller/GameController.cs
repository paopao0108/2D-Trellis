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
        Debug.Log("���ܵ�Ring����Ϣ");
        transform.Find("GameOverPanel").gameObject.SetActive(true);
        BroadcastMessage("GameOver", winner);
    }

}
