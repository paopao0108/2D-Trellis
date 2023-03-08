using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Utils;

public class RingPanel : MonoBehaviour
{
    private static RingPanel _instance;
    private static int[] nums = new int[] { 3, 3, 3 };

    private void Awake()
    {
        _instance = this;
        var objs = GameObject.FindGameObjectsWithTag("num");
        Debug.Log("num: " + objs.Length);
    }

    public static RingPanel Instance => _instance;

    [PunRPC]
    public static void UpdateNum(RingType ringType)
    {
        nums[(int)ringType]--;
    }
}