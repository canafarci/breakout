using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

using TMPro;

namespace Breakout.Gameplay.EndGameCanvas
{
    internal class EndGameCanvasView : MonoBehaviour
    {
        [SerializeField] private PlayableDirector PlayableDirector;
        [SerializeField] private TextMeshProUGUI EndGameTitleText;
        [SerializeField] private Button MainMenuButton;

        internal TextMeshProUGUI endGameTitleText => EndGameTitleText;
        internal Button mainMenuButton => MainMenuButton;
        internal PlayableDirector playableDirector => PlayableDirector;
    }
}