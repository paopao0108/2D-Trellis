using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public enum PlayerType
    {
        MasterPlayer,
        ClientPlayer
    }

    public enum RingType
    {
        Large,
        Middle,
        Small
    }

    public static class Constants
    {
        public static class Colors
        {
            public static readonly Color MasterColor = new(23 / 255f, 199 / 255f, 255 / 255f, 230 / 255f);
            public static readonly Color ClientColor = Color.white;
            public static readonly Color Highlight = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0);
        }

        public static class Vars
        {
            public static readonly float transparency = 0.5f;
        }
    }
}