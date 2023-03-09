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
        Debug.Log("接受到Ring的消息");
        transform.Find("GameOverPanel").gameObject.SetActive(true); // 显示gameover页面
        BroadcastMessage("GameOver", winner);
        PauseSound(bgSound); // 暂停背景音乐
    }

    public void GameAgain()
    {
        Debug.Log("接受到 Again 的消息");
        transform.Find("GameOverPanel").gameObject.SetActive(false); // 隐藏gameover页面
        UnPauseSound(bgSound); // 播放背景音乐
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
