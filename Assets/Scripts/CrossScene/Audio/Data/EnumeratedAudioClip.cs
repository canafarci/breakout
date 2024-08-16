using UnityEngine;

using System;

using Breakout.CrossScene.Enums;

namespace Breakout.CrossScene.Audio.Data
{
    [Serializable]
    public struct EnumeratedAudioClip
    {
        public AudioClipID AudioClipID;
        public AudioClip AudioClip;
    }
}