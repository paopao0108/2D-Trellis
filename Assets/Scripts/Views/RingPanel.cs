using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class RingPanel : MonoBehaviour
{
    private static RingPanel _instance;
    //private static int[] nums = new int[] { 3, 3, 3 };

    [NonSerialized] public static Ring LRing;
    [NonSerialized] public static Ring MRing;
    [NonSerialized] public static Ring SRing;
    [NonSerialized] public static Number LRingNum;
    [NonSerialized] public static Number MRingNum;
    [NonSerialized] public static Number SRingNum;
    public Dictionary<string, Number> ringNums = new Dictionary<string, Number>();

    private void Awake()
    {
        _instance = this;
        LRing = GameObject.Find("LRing").GetComponent<Ring>();
        MRing = GameObject.Find("MRing").GetComponent<Ring>();
        SRing = GameObject.Find("SRing").GetComponent<Ring>();
        LRingNum = GameObject.Find("LRingPanel").transform.Find("num").GetComponent<Number>();
        MRingNum = GameObject.Find("MRingPanel").transform.Find("num").GetComponent<Number>();
        SRingNum = GameObject.Find("SRingPanel").transform.Find("num").GetComponent<Number>();
        ringNums = new Dictionary<string, Number>
        {
            {"L",  LRingNum},
            {"M",  MRingNum},
            {"S",  SRingNum},
        };
    }

    public static RingPanel Instance => _instance;

    /// <summary>
    /// 初始化ring颜色
    /// </summary>
    /// <param name="playerType"></param>
    public static void InitRingColor(PlayerType playerType)
    {
        LRing.SetColor(playerType);
        MRing.SetColor(playerType);
        SRing.SetColor(playerType);
    }
}