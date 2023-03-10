using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuPanel : MonoBehaviour
{
    private Button _enterBtn;
    private Button _selectBtn;
    private Button _exitBtn;
    private void Awake()
    {
        _enterBtn = GameObject.Find("EnterBtn").GetComponent<Button>();
        _selectBtn = GameObject.Find("SelectBtn").GetComponent<Button>();
        _exitBtn = GameObject.Find("ExitBtn").GetComponent<Button>();
    }
    
    public void OnEnterBtnClick()
    {
        gameObject.SetActive(false);
        //transform.Find("LoadingPanel").gameObject.SetActive(true);
        //Debug.Log(GetComponent<LoadingPanel>());
        
        //SendMessage("WaitforPlayerEnter");
    }
}
