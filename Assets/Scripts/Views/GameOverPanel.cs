using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameOverPanel : MonoBehaviour
{
    private PlayerType curClient; // ��ǰ�ͻ���

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver(PlayerType winner)
    {
        Debug.Log("���յ�Canvas����Ϣ");
        //winner == PlayerType.ClientPlayer ? ShowWin() : ShowLose();
        // 1. �Ȼ�ȡ��ǰ�Ŀͻ���
        curClient = PhotonNetwork.IsMasterClient ? PlayerType.MasterPlayer : PlayerType.ClientPlayer;

        // 2. �жϵ�ǰ�ͻ�����winner��ͬ
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
