using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    private TMP_Text numText;

    public int num = 5; // Ĭ������
    private void Awake()
    {
        numText = GetComponent<TMP_Text>();
        Debug.Log("�����ı���" + numText.text);
    }
    void Start()
    {
        numText.text = "x" + num.ToString();
    }

    void Update()
    {
        
    }

    public void DecreaseNum()
    {
        numText.text = "x" + (--num).ToString();
    }

    public void ResetNum()
    {
        num = 5;
        numText.text = "x" + num.ToString();
    }
}