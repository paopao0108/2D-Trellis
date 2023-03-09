using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameOverPanel : MonoBehaviour
{
    private PlayerType curClient; // 当前客户端

    public AudioSource winSound;
    public AudioSource loseSound;

    void Start()
    {

    }

    void Update()
    {

    }

    public void GameOver(PlayerType winner)
    {
        Debug.Log("接收到Canvas的消息");
        // 1. 先获取当前的客户端
        curClient = PhotonNetwork.IsMasterClient ? PlayerType.MasterPlayer : PlayerType.ClientPlayer;

        // 2. 判断当前客户端与winner相同
        if (winner == curClient) 
        {
            ShowWin();
            GameController.Instance.PlaySound(winSound); // 播放成功音效
        }
        else
        {
            ShowLose();
            GameController.Instance.PlaySound(loseSound); // 播放失败音效
        }

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

    public void OnExitButton()
    {
        Debug.Log("退出游戏！");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void OnAgainButton()
    {
        Debug.Log("再来一次"); // 玩家重新进入房间，游戏重新开始
    }
}
