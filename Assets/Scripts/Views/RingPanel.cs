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
    private static int[] nums = new int[] { 3, 3, 3 };

    [NonSerialized] public static Ring Large;
    [NonSerialized] public static Ring Middle;
    [NonSerialized] public static Ring Small;

    private void Awake()
    {
        _instance = this;
        var objs = GameObject.FindGameObjectsWithTag("num");
        Debug.Log("num: " + objs.Length);

        Large = GameObject.Find("LRing").GetComponent<Ring>();
        Middle = GameObject.Find("MRing").GetComponent<Ring>();
        Small = GameObject.Find("SRing").GetComponent<Ring>();
    }

    public static RingPanel Instance => _instance;

    [PunRPC]
    public static void UpdateNum(RingType ringType)
    {
        nums[(int)ringType]--;
    }

    // 启用Ring脚本
    public static void EnableRing(Ring ring)
    {
        if (ring.GetComponent<Ring>() == null) ring.AddComponent<Ring>();
    }

    // 禁用Ring脚本
    public static void DisableRing(Ring ring)
    {
        if (ring.GetComponent<Ring>()) Destroy(ring.GetComponent<Ring>());
    }
}