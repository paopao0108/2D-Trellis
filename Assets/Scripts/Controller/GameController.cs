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
        Debug.Log("���ܵ�Ring����Ϣ");
        transform.Find("GameOverPanel").gameObject.SetActive(true);
        BroadcastMessage("GameOver");
    }

}
