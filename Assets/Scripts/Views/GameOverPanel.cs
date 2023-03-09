using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameOverPanel : MonoBehaviourPun
{
    private PlayerType curClient; // ��ǰ�ͻ���

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
        Debug.Log("���յ�Canvas����Ϣ");
        // 1. �Ȼ�ȡ��ǰ�Ŀͻ���
        curClient = PhotonNetwork.IsMasterClient ? PlayerType.MasterPlayer : PlayerType.ClientPlayer;

        // 2. �жϵ�ǰ�ͻ�����winner��ͬ
        if (winner == curClient) 
        {
            ShowWin();
            GameController.Instance.PlaySound(winSound); // ���ųɹ���Ч
        }
        else
        {
            ShowLose();
            GameController.Instance.PlaySound(loseSound); // ����ʧ����Ч
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
        Debug.Log("�˳���Ϸ��");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    
    public void OnAgainButton() // ��ʱʵ��һ�˿ͻ��˵������һ�Σ����ͬʱ��ʼ
    {
        Debug.Log("����һ��"); // ������½��뷿�䣬��Ϸ���¿�ʼ
        photonView.RPC("ResetGame", RpcTarget.AllBuffered); // ���ͬʱ����ResetGame
    }

    [PunRPC]
    public void ResetGame()
    {
        // 1. ���grids����
        for (int i = 0; i < GridPanel.row; i++)
        {
            for (int j = 0; j < GridPanel.col; j++)
            {
                GridPanel.grids[j][i].clear();
            }
        }
        // 2. ����Բ������
        Number[] nums = FindObjectsOfType<Number>();
        foreach (var num in nums)
        {
            num.ResetNum();
        }
        // 3. ����Բ��
        Ring[] rings = FindObjectsOfType<Ring>();
        foreach (var ring in rings)
        {
            ring.ResetRing();
        }

        // 3. ����ҳ����ת
        SendMessageUpwards("GameAgain");
    }
}
