using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

using Breakout.Gameplay.Tutorial;

namespace Breakout.Gameplay.PauseCanvas
{
    internal class PauseCanvasView : MonoBehaviour
    {
        [SerializeField] private PlayableDirector PlayableDirector;
        [SerializeField] private Button MainMenuButton;
        [SerializeField] private Button ContinueButton;
        [SerializeField] private Button ShowControlsButton;
        [SerializeField] private TutorialController TutorialController;

        internal PlayableDirector playableDirector => PlayableDirector;
        internal Button mainMenuButton => MainMenuButton;
        internal Button continueButton => ContinueButton;
        internal Button showControlsButton => ShowControlsButton;
        internal TutorialController tutorialController => TutorialController;
    }
}