using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void SendGameOver()
    {
        Debug.Log("���ܵ�Ring����Ϣ");
        transform.Find("GameOverPanel").gameObject.SetActive(true);
        BroadcastMessage("GameOver");
    }

}
