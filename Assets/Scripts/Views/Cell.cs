using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //[NonSerialized] public bool hasOutPos = true; // ������λ��
    //[NonSerialized] public bool hasMiddlePos = true; // �м���λ��
    //[NonSerialized] public bool hasInnerPos = true; // ���ڲ��λ��

    public Dictionary<string, bool> hasPos = new Dictionary<string, bool>
    {
        { "L" , true },
        { "M" , true },
        { "S" , true }
    };
    public static float CellSize = 100;
}
