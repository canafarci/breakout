using UnityEngine;

using System;

namespace Breakout.Gameplay.Data
{
    [Serializable]
    public struct BrickDatum
    {
        public Color32 BrickColor;
        public int BrickHealth;
        public int BrickScore;
    }
}