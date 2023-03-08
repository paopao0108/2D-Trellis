using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class GridPanel : MonoBehaviourPun
{
    public Cell cell;
    public static List<List<Cell>> grids = new();
    public static int row = 3;
    public static int col = 3;

    // public Dictionary<RingType, Ring> RingPrefabs;
    public Ring LRingPrefab;

    private void Start()
    {
        var gridPanelSize = GetComponent<RectTransform>().rect.width;
        var gridSpacing = GetComponent<GridLayoutGroup>().spacing.x;
        float gridPanelPadding = GetComponent<GridLayoutGroup>().padding.left;
        Cell.CellSize = (gridPanelSize - (row - 1) * gridSpacing - 2 * gridPanelPadding) / row;
        Debug.Log("?????: " + gridPanelSize + "??????: " + gridSpacing + "???????:  " + Cell.CellSize);
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(Cell.CellSize, Cell.CellSize);
        Init();
    }

    public void Init()
    {
        for (var i = 0; i < row; i++)
        {
            grids.Add(new List<Cell>());
            for (var j = 0; j < col; j++)
                grids[i].Add(Instantiate(cell, transform));
        }
    }

    [PunRPC]
    public void SetPosition(RingType ringType, int row, int col)
    {
        Debug.LogError("SetPosition: " + ringType + " " + row + " " + col);
        var clone = Instantiate<Ring>(LRingPrefab, grids[col][row].transform, false);
        clone.GetComponent<Ring>().SetTransparency(Constants.Vars.transparency);
    }
}