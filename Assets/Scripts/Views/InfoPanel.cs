using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Assertions;
using Views;

public class InfoPanel : MonoBehaviour
{
    private static InfoPanel _instance;

    [NonSerialized] public static PlayerPanel PlayerPanel1;
    [NonSerialized] public static PlayerPanel PlayerPanel2;

    private void Awake()
    {
        _instance = this;
        PlayerPanel1 = transform.Find("PlayerPanel1").GetComponent<PlayerPanel>();
        PlayerPanel2 = transform.Find("PlayerPanel2").GetComponent<PlayerPanel>();
    }

    public static InfoPanel Instance => _instance;
}