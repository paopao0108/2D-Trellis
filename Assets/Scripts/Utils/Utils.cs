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
                    Debug.Log("UP方向");
                    GetSameRingCell(count, curRow, curCol + 1, curRow, curCol, sizeType, grids, direc, res);
                    break;
                case Direction.DOWN:
                    Debug.Log("DOWN方向");
                    GetSameRingCell(count, curRow, curCol - 1, curRow, curCol, sizeType, grids, direc, res);
                    break;
                case Direction.LEFT:
                    Debug.Log("Left方向");
                    GetSameRingCell(count, curRow - 1, curCol, curRow, curCol, sizeType, grids, direc, res);
                    break;
                case Direction.RIGHT:
                    Debug.Log("RIGHT方向");
                    GetSameRingCell(count, curRow + 1, curCol, curRow, curCol, sizeType, grids, direc, res);
                    break;
                case Direction.LEFTUP:
                    Debug.Log("LEFTUP方向");
                    GetSameRingCell(count, curRow - 1, curCol + 1, curRow, curCol, sizeType, grids, direc, res);
                    break;
                case Direction.LEFTDOWN:
                    Debug.Log("LEFTUP方向");
                    GetSameRingCell(count, curRow + 1, curCol - 1, curRow, curCol, sizeType, grids, direc, res);
                    break;
                case Direction.RIGHTUP:
                    Debug.Log("RIGHTUP方向");
                    GetSameRingCell(count, curRow + 1, curCol + 1, curRow, curCol, sizeType, grids, direc, res);
                    break;
                case Direction.RIGHTDOWN:
                    Debug.Log("RIGHTDOWN方向");
                    GetSameRingCell(count, curRow - 1, curCol - 1, curRow, curCol, sizeType, grids, direc, res);
                    break;
            }
            return res;
        }


        private static void GetSameRingCell(int count, int nextRow, int nextCol, int curRow, int curCol, string sizeType, List<List<Cell>> grids, Direction direc, List<Cell> res) {
            //Debug.LogError("当前cell位置: " + curCol + " " + curRow);
            //Debug.LogError("下次判断位置: " + nextCol + " " + nextRow);
            if (!(nextCol < count && nextCol >= 0 && nextRow < count && nextRow >= 0) || grids[nextCol][nextRow].Pos[sizeType] == "") return;

            if ((grids[nextCol][nextRow].Pos[sizeType] == grids[curCol][curRow].Pos[sizeType]))
            {
                res.Add(grids[nextCol][nextRow]);
                var resList = GetSameRingCellByDirection(count, nextRow, nextCol, sizeType, grids, direc);
                res.AddRange(resList);
            }
        }

    }
}
