using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Dictionary<string, string> Pos = new Dictionary<string,string>
    {
        { "L" , "" },
        { "M" , "" },
        { "S" , "" }
    };
    public static float CellSize = 100;
}
