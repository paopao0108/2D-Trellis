using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public enum EPlayerType
    {
        None,
        MasterPlayer,
        ClientPlayer,
    }

    public static class Constants
    {
        public static class Colors
        {
            public static readonly Color MasterColor = new(23 / 255f, 199 / 255f, 255 / 255f, 230 / 255f);
            public static readonly Color ClientColor = Color.white;
            public static readonly Color transparent = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0);
        }

        public static class Vars
        {
            public const float Transparency = 0.5f;
            public const int PerRingNum = 3; // 初始各类大小的圆圈数)
            public const int RowNum = 3; // 表格行/列数
            public const int ColNum = 3; // 表格行/列数
        }
    }
}