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


    public void clear()
    {
        Transform[] trans = GetComponentsInChildren<Transform>();
        foreach(var item in trans)
        {
            if (item.GetComponent<Cell>() == null) Destroy(item.gameObject);
        }
        Pos["L"] = "";
        Pos["M"] = "";
        Pos["S"] = "";
    }
}
