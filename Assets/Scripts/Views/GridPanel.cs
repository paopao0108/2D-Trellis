using System;
using System.Collections.Generic;
using Controller;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    RIGHTUP,
    RIGHTDOWN,
    LEFTUP,
    LEFTDOWN,
}


public class GridPanel : MonoBehaviourPun
{
    public Cell cell;
    public static List<List<Cell>> Grids = new();
    public static int row = Constants.Vars.RowNum;
    public static int col = Constants.Vars.ColNum;

    public Dictionary<ERingType, Ring> RingPrefabs;

    private void Awake()
    {
        RingPrefabs = new Dictionary<ERingType, Ring>
        {
            [ERingType.LRing] = Resources.Load("Prefabs/LRing").GetComponent<Ring>(),
            [ERingType.MRing] = Resources.Load("Prefabs/MRing").GetComponent<Ring>(),
            [ERingType.SRing] = Resources.Load("Prefabs/SRing").GetComponent<Ring>(),
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
            Grids.Add(new List<Cell>());
            for (var j = 0; j < col; j++)
                Grids[i].Add(Instantiate(cell, transform));
        }
    }

    [PunRPC]
    public void SetPosition(ERingType ringType, int i, int j)
    {
        Debug.LogError("SetPosition: " + ringType + " " + i + " " + j + "  玩家： " + NetworkManager.EPlayerTurn);

        Ring clone = Instantiate<Ring>(RingPrefabs[ringType], Grids[i][j].transform, false);
        clone.SetColor(NetworkManager.EPlayerTurn); // 放置在网格中的预制体ring的颜色需要根据玩家确定
        clone.GetComponent<Ring>().SetTransparency(Constants.Vars.Transparency);
    }

    // 从指定格子开始检查指定玩家是否已经赢得游戏
    public bool Check(int i, int j, ERingType ringType)
    {
        if (
            (count(i, j, Direction.UP, ringType) + count(i, j, Direction.DOWN, ringType) >= 2)
            ||
            (count(i, j, Direction.LEFT, ringType) + count(i, j, Direction.RIGHT, ringType) >= 2)
            ||
            (count(i, j, Direction.LEFTUP, ringType) + count(i, j, Direction.RIGHTDOWN, ringType) >= 2)
            ||
            (count(i, j, Direction.LEFTDOWN, ringType) + count(i, j, Direction.RIGHTUP, ringType) >= 2)
        )
            return true;
        return false;
    }

    // 递归检查指定位置的指定方向
    private int count(int i, int j, Direction direction, ERingType ringType)
    {
        if (i < 0 || j < 0 || i >= row || j >= row) return 0;
        int i2 = i, j2 = j;

        switch (direction)
        {
            case Direction.UP:
                i2 = i + 1;
                break;
            case Direction.DOWN:
                i2 = i - 1;
                break;
            case Direction.LEFT:
                j2 = j - 1;
                break;
            case Direction.RIGHT:
                j2 = j + 1;
                break;
            case Direction.LEFTUP:
                i2 = i + 1;
                j2 = j - 1;
                break;
            case Direction.LEFTDOWN:
                i2 = i - 1;
                j2 = j - 1;
                break;
            case Direction.RIGHTUP:
                i2 = i + 1;
                j2 = j + 1;
                break;
            case Direction.RIGHTDOWN:
                i2 = i - 1;
                j2 = j + 1;
                break;
        }

        // 判断同类型的相邻块的颜色相同, 则进一步判断
        if (i2 < 0 || j2 < 0 || i2 >= row || j2 >= row ||
            Grids[i][j].Pos[ringType] != Grids[i2][j2].Pos[ringType]) return 0;
        return count(i2, j2, direction, ringType) + 1;
    }
}