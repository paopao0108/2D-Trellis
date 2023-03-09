using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameOverPanel : MonoBehaviour
{
    private PlayerType curClient; // 当前客户端

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver(PlayerType winner)
    {
        Debug.Log("接收到Canvas的消息");
        //winner == PlayerType.ClientPlayer ? ShowWin() : ShowLose();
        // 1. 先获取当前的客户端
        curClient = PhotonNetwork.IsMasterClient ? PlayerType.MasterPlayer : PlayerType.ClientPlayer;

        // 2. 判断当前客户端与winner相同
        if (winner == curClient) ShowWin();
        else ShowLose();
        
    }

    public void ShowLose()
    {
        transform.Find("WinPanel").gameObject.SetActive(false);
        transform.Find("LosePanel").gameObject.SetActive(true);
    }

    public void ShowWin()
    {
        transform.Find("WinPanel").gameObject.SetActive(true);
        transform.Find("LosePanel").gameObject.SetActive(false);
    }
}
