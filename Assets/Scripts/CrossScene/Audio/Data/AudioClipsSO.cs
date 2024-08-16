using UnityEngine;

using System.Collections.Generic;

namespace Breakout.CrossScene.Audio.Data
{
    [CreateAssetMenu(fileName = "Audio Clips SO", menuName = "Breakout/Audio/Audio Clips SO", order = 0)]
    public class AudioClipsSO : ScriptableObject
    {
        public List<EnumeratedAudioClip> EnumeratedAudioClips;
    }
}