using UnityEngine;
using UnityEngine.Playables;

namespace Breakout.Gameplay.Tutorial
{
    internal class TutorialView : MonoBehaviour
    {
        [SerializeField] private PlayableDirector PlayableDirector;
        internal PlayableDirector playableDirector => PlayableDirector;
    }
}