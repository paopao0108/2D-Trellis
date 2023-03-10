using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Utils;

public class GridPanel : MonoBehaviourPun
{
    public Player player;
    public Cell cell;
    public static List<List<Cell>> grids = new();
    public static int row = 3;
    public static int col = 3;

    //public Dictionary<RingType, Ring> RingPrefabs = new Dictionary<RingType, Ring> { };
    public Dictionary<RingType, Ring> RingPrefabs;

    private void Awake()
    {
        RingPrefabs = new Dictionary<RingType, Ring>
        {
            [RingType.Large] = Resources.Load("Prefabs/LRing").GetComponent<Ring>(),
            [RingType.Middle] = Resources.Load("Prefabs/MRing").GetComponent<Ring>(),
            [RingType.Small] = Resources.Load("Prefabs/SRing").GetComponent<Ring>(),
        };
        
        var gridPanelSize = GetComponent<RectTransform>().rect.width;
        var gridSpacing = GetComponent<GridLayoutGroup>().spacing.x;
        float gridPanelPadding = GetComponent<GridLayoutGroup>().padding.left;
        Cell.CellSize = (gridPanelSize - (row - 1) * gridSpacing - 2 * gridPanelPadding) / row;
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(Cell.CellSize, Cell.CellSize);
    }

    private void Start()
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
        //Debug.LogError("SetPosition: "+ "row: " +  row + " col: " + col);
        Ring clone; // 默认克隆large
        
        clone = Instantiate<Ring>(RingPrefabs[ringType], grids[col][row].transform, false);
        clone.SetColor(NetworkManager.playerTurn); // 放置在网格中的预制体ring的颜色需要根据玩家确定
        clone.GetComponent<Ring>().SetTransparency(Constants.Vars.transparency);
    }
}