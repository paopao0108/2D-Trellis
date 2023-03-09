using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Cell : MonoBehaviour
{
    public static float CellSize = 100;

    public Dictionary<ERingType, EPlayerType> Pos = new() // 格子上指定大小是否有空余位置
    {
        [ERingType.LRing] = EPlayerType.None,
        [ERingType.MRing] = EPlayerType.None,
        [ERingType.SRing] = EPlayerType.None,
    };
}