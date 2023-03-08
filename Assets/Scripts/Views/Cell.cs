using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //[NonSerialized] public bool hasOutPos = true; // 最外层的位置
    //[NonSerialized] public bool hasMiddlePos = true; // 中间层的位置
    //[NonSerialized] public bool hasInnerPos = true; // 最内层的位置

    public Dictionary<string, bool> hasPos = new Dictionary<string, bool>
    {
        { "L" , true },
        { "M" , true },
        { "S" , true }
    };
    public static float CellSize = 100;
}
