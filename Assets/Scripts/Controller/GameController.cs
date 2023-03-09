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
        Debug.Log("接受到Ring的消息");
        transform.Find("GameOverPanel").gameObject.SetActive(true);
        BroadcastMessage("GameOver");
    }

}
