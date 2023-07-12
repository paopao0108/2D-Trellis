using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    private TMP_Text numText;

    public int num = 5; // 默认数量
    private void Awake()
    {
        numText = GetComponent<TMP_Text>();
        Debug.Log("数量文本：" + numText.text);
    }
    void Start()
    {
        numText.text = "x" + num.ToString();
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
