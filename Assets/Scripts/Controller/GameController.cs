using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool IsGameOver(int row, int col, List<List<Cell>> grids)
    {
        //for (int i = 0; i < row; i++)
        //{
        //    for (int j =0; j < col; j++)
        //    {
        //        // 有一行、一列、对角线相等，则游戏结束
        //        // 判断一行是否相等
        //        if ((grids[j][i].Pos["L"] != "") && (grids[j][i].Pos["L"] == grids[j][i+1].Pos["L"]))
        //        // 判断一列是否相等
        //        // 判断对角线是否相等
        //    }
        //}
        return false;
    }


}
