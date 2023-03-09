using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameController : MonoBehaviourPun
{
    private static GameController _instance;

    public static GameController Instance => _instance;

    public static PlayerType winner;
    public AudioSource dropSound;
    public AudioSource bgSound;

    private void Awake()
    {
        _instance = this;
        PlaySound(bgSound);
    }
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
        transform.Find("GameOverPanel").gameObject.SetActive(true); // ��ʾgameoverҳ��
        BroadcastMessage("GameOver", winner);
        PauseSound(bgSound); // ��ͣ��������
    }

    public void GameAgain()
    {
        Debug.Log("���ܵ� Again ����Ϣ");
        transform.Find("GameOverPanel").gameObject.SetActive(false); // ����gameoverҳ��
        UnPauseSound(bgSound); // ���ű�������
    }

    public void PlaySound(AudioSource audio)
    {
        if (!audio) return;
        audio.Play();
    }

    public void PauseSound(AudioSource audio)
    {
        if (!audio) return;
        audio.Pause();
    }

    public void UnPauseSound(AudioSource audio)
    {
        if (!audio) return;
        audio.UnPause();
    }
}
