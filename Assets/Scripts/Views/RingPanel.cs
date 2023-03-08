using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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

    //[PunRPC]
    //public void InitRingColor(PlayerType playerType)
    //{
    //    Large.SetColor(playerType);
    //    Middle.SetColor(playerType);
    //    Small.SetColor(playerType);
    //}
}