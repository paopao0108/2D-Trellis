using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Utils
{
    class Utils
    {
        public enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            RIGHTUP,
            RIGHTDOWN,
            LEFTUP,
            LEFTDOWN
        }


        /// <summary>
        /// 是否为连续的
        /// </summary>
        /// <param name="count">连续的个数</param>
        /// <param name="curRow">当前行</param>
        /// <param name="curCol">当前列</param>
        /// <param name="sizeType"></param>
        /// <param name="grids"></param>
        /// <returns></returns>
        public static bool IsSuccession(int count, int curRow, int curCol, string sizeType, List<List<Cell>> grids)
        {
            bool res = false;
            var upList = GetSameRingCellByDirection(count, curRow, curCol, sizeType, grids, Direction.UP);
            var downList = GetSameRingCellByDirection(count, curRow, curCol, sizeType, grids, Direction.DOWN);
            var leftList = GetSameRingCellByDirection(count, curRow, curCol, sizeType, grids, Direction.LEFT);
            var rightList = GetSameRingCellByDirection(count, curRow, curCol, sizeType, grids, Direction.RIGHT);
            var leftUpList = GetSameRingCellByDirection(count, curRow, curCol, sizeType, grids, Direction.LEFTUP);
            var leftDownList = GetSameRingCellByDirection(count, curRow, curCol, sizeType, grids, Direction.LEFTDOWN);
            var rightUpList = GetSameRingCellByDirection(count, curRow, curCol, sizeType, grids, Direction.RIGHTUP);
            var rightDownList = GetSameRingCellByDirection(count, curRow, curCol, sizeType, grids, Direction.RIGHTDOWN);
            if (upList.Count + downList.Count + 1 == count ||
                leftList.Count + rightList.Count + 1 == count ||
                leftUpList.Count + rightDownList.Count + 1 == count ||
                leftDownList.Count + rightUpList.Count + 1 == count)
                return true;
            return res;
        }

        /// <summary>
        /// 获取指定方向上的相同圆环的数量
        /// </summary>
        /// <param name="curRow">当前行</param>
        /// <param name="curCol">当前列</param>
        /// <param name="grids">所有的位置</param>
        /// <param name="direc">方向</param>
        /// <returns></returns>
        public static List<Cell> GetSameRingCellByDirection(int count, int curRow, int curCol, string sizeType, List<List<Cell>> grids, Direction direc)
        {
            List<Cell> res = new List<Cell>();
            switch (direc)
            {
                case Direction.UP:
                    
                    GetSameRingCell(count, curRow - 1, curCol, curRow, curCol, sizeType, grids, res);
                    // if (grids[curCol][curRow - 1] && grids[curCol][curRow - 1].Pos[sizeType] != "" && (grids[curCol][curRow - 1].Pos[sizeType] == grids[curCol][curRow].Pos[sizeType]))
                    // {
                    //     res.Add(grids[curCol][curRow - 1]);
                    //     var resList = GetSameRingCellByDirection(count, curRow - 1, curCol, sizeType, grids, Direction.UP);
                    //     res.AddRange(resList);
                    // }
                    break;
                case Direction.DOWN:
                    GetSameRingCell(count, curRow + 1, curCol, curRow, curCol, sizeType, grids, res);

                    // if (grids[curCol][curRow + 1] && grids[curCol][curRow + 1].Pos[sizeType] != "" && (grids[curCol][curRow + 1].Pos[sizeType] == grids[curCol][curRow].Pos[sizeType]))
                    // {
                    //     res.Add(grids[curCol][curRow + 1]);
                    //     var resList = GetSameRingCellByDirection(count, curRow + 1, curCol, sizeType, grids, Direction.DOWN);
                    //     res.AddRange(resList);
                    // }
                    break;
                case Direction.LEFT:
                    GetSameRingCell(count, curRow, curCol - 1, curRow, curCol, sizeType, grids, res);

                    // if (grids[curCol - 1][curRow] && grids[curCol - 1][curRow].Pos[sizeType] != "" && (grids[curCol - 1][curRow].Pos[sizeType] == grids[curCol][curRow].Pos[sizeType]))
                    // {
                    //     res.Add(grids[curCol - 1][curRow]);
                    //     var resList = GetSameRingCellByDirection(count, curRow, curCol - 1, sizeType, grids, Direction.UP);
                    //     res.AddRange(resList);
                    // }
                    break;
                case Direction.RIGHT:
                    GetSameRingCell(count, curRow, curCol + 1, curRow, curCol, sizeType, grids, res);

                    // if (grids[curCol + 1][curRow] && grids[curCol + 1][curRow].Pos[sizeType] != "" && (grids[curCol + 1][curRow].Pos[sizeType] == grids[curCol][curRow].Pos[sizeType]))
                    // {
                    //     res.Add(grids[curCol + 1][curRow]);
                    //     var resList = GetSameRingCellByDirection(count, curRow, curCol + 1, sizeType, grids, Direction.UP);
                    //     res.AddRange(resList);
                    // }
                    break;
                case Direction.LEFTUP:
                    GetSameRingCell(count, curRow -1, curCol - 1, curRow, curCol, sizeType, grids, res);

                    // if (grids[curCol - 1][curRow - 1] && grids[curCol - 1][curRow - 1].Pos[sizeType] != "" && (grids[curCol - 1][curRow - 1].Pos[sizeType] == grids[curCol][curRow].Pos[sizeType]))
                    // {
                    //     res.Add(grids[curCol - 1][curRow - 1]);
                    //     var resList = GetSameRingCellByDirection(count, curRow - 1, curCol - 1, sizeType, grids, Direction.UP);
                    //     res.AddRange(resList);
                    // }
                    break;
                case Direction.LEFTDOWN:
                    GetSameRingCell(count, curRow + 1, curCol - 1, curRow, curCol, sizeType, grids, res);

                    // if (grids[curCol + 1][curRow - 1] && grids[curCol + 1][curRow - 1].Pos[sizeType] != "" && (grids[curCol + 1][curRow - 1].Pos[sizeType] == grids[curCol][curRow].Pos[sizeType]))
                    // {
                    //     res.Add(grids[curCol + 1][curRow - 1]);
                    //     var resList = GetSameRingCellByDirection(count, curRow - 1, curCol + 1, sizeType, grids, Direction.UP);
                    //     res.AddRange(resList);
                    // }
                    break;
                case Direction.RIGHTUP:
                    GetSameRingCell(count, curRow - 1, curCol + 1, curRow, curCol, sizeType, grids, res);

                    // if (grids[curCol - 1][curRow + 1] && grids[curCol - 1][curRow + 1].Pos[sizeType] != "" && (grids[curCol - 1][curRow + 1].Pos[sizeType] == grids[curCol][curRow].Pos[sizeType]))
                    // {
                    //     res.Add(grids[curCol - 1][curRow + 1]);
                    //     var resList = GetSameRingCellByDirection(count, curRow + 1, curCol - 1, sizeType, grids, Direction.UP);
                    //     res.AddRange(resList);
                    // }
                    break;
                case Direction.RIGHTDOWN:
                    GetSameRingCell(count, curRow + 1, curCol + 1, curRow, curCol, sizeType, grids, res);
                    // if (grids[curCol + 1][curRow + 1]  && grids[curCol + 1][curRow + 1].Pos[sizeType] != "" && (grids[curCol + 1][curRow + 1].Pos[sizeType] == grids[curCol][curRow].Pos[sizeType]))
                    // {
                    //     res.Add(grids[curCol + 1][curRow + 1]);
                    //     var resList = GetSameRingCellByDirection(count, curRow + 1, curCol + 1, sizeType, grids, Direction.UP);
                    //     res.AddRange(resList);
                    // }
                    break;
            }
            return res;
        }


        private static void GetSameRingCell(int count, int nextRow, int nextCol, int curRow, int curCol, string sizeType, List<List<Cell>> grids, List<Cell> res) {
            // List<Cell> res = new List<Cell>();
            Debug.LogError("下一行，下一列: " + nextRow + " " + nextCol);
            if (!(nextCol < count && nextCol >= 0 && nextRow < count && nextRow >= 0) || grids[nextCol][nextRow].Pos[sizeType] == "")
            {
                Debug.LogError("返回");
                return;
            }
            Debug.LogError("进入判断");

            if ((grids[nextCol][nextRow].Pos[sizeType] == grids[curCol][curRow].Pos[sizeType]))
            {
                Debug.LogError("进入");

                res.Add(grids[nextCol][nextRow]);
                var resList = GetSameRingCellByDirection(count, nextRow, nextCol, sizeType, grids, Direction.UP);
                res.AddRange(resList);
                Debug.LogError("res: " + res);

            }
            Debug.LogError("if外");

        }
        

    }
}
