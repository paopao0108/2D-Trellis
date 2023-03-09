using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviourPun
{
    void Start()
    {
    }

    void Update()
    {
        
    }

    [PunRPC]
    public void SendGameOver()
    {
        Debug.Log("接受到Ring的消息");
        transform.Find("GameOverPanel").gameObject.SetActive(true);
        BroadcastMessage("GameOver");
    }

}
