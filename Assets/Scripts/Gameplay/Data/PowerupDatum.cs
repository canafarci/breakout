using System;
using Breakout.Gameplay.Enums;
using UnityEngine;

namespace Breakout.Gameplay.Data
{
    [Serializable]
    public struct PowerupDatum
    {
        public Color32 PowerupBackgroundColor;
        public PowerupID PowerupID;
        public Sprite PowerupSprite;
    }
}