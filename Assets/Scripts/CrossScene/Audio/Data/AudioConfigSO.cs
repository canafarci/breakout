using UnityEngine;

namespace Breakout.CrossScene.Audio.Data
{
    [CreateAssetMenu(fileName = "Audio Config SO", menuName = "Breakout/Audio/Audio Config SO", order = 0)]
    public class AudioConfigSO : ScriptableObject
    {
        [SerializeField] private int OneShotAudioSourceCount;

        internal int oneShotAudioSourceCount => OneShotAudioSourceCount;
    }
}