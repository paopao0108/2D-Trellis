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

    public static Dictionary<ERingType, int> Nums = new()
    {
        [ERingType.LRing] = Constants.Vars.PerRingNum,
        [ERingType.MRing] = Constants.Vars.PerRingNum,
        [ERingType.SRing] = Constants.Vars.PerRingNum,
    };

    [NonSerialized] public static Ring LRing;
    [NonSerialized] public static Ring MRing;
    [NonSerialized] public static Ring SRing;

    private void Awake()
    {
        _instance = this;
        LRing = GameObject.Find("LRing").GetComponent<Ring>();
        MRing = GameObject.Find("MRing").GetComponent<Ring>();
        SRing = GameObject.Find("SRing").GetComponent<Ring>();
    }

    public static RingPanel Instance => _instance;

    public static void Decrease(ERingType ringType)
    {
        Nums[ringType]--;
    }

    public static bool HasEnough(ERingType ringType)
    {
        return Nums[ringType] > 0;
    }

    // 初始化ring颜色
    public static void InitRingColor(EPlayerType ePlayerType)
    {
        LRing.SetColor(ePlayerType);
        MRing.SetColor(ePlayerType);
        SRing.SetColor(ePlayerType);
    }
}