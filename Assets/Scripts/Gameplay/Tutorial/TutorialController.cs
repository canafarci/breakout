using UnityEngine;

using Breakout.CrossScene.Data;
using Breakout.Gameplay.Enums;
using Breakout.Gameplay.State;
using Breakout.Infrastructure.Extensions;

namespace Breakout.Gameplay.Tutorial
{
    internal class TutorialController : MonoBehaviour
    {
        [SerializeField] private TutorialView View;
        private TutorialState _tutorialState = TutorialState.Inactive;

        private float _minimumTutorialShowDuration;
        
        private const float MINIMUM_TUTORIAL_SHOW_DURATION = 1f;
        
        internal void Initialize()
        {
            View.gameObject.SetActive(true);
            View.playableDirector.Play();
            
            _minimumTutorialShowDuration = MINIMUM_TUTORIAL_SHOW_DURATION;
            _tutorialState = TutorialState.Active;
        }

        private void Update()
        {
            if (_tutorialState == TutorialState.Inactive) return;

            _minimumTutorialShowDuration -= Time.deltaTime;
            
            //there is a buffer time to prevent accidental closing
            //of the tutorial panel
            if (_minimumTutorialShowDuration < 0f && Input.anyKey)
            {
                _tutorialState = TutorialState.Inactive;
                //this is an extension method, it executes the delegate at the end
                //of the timeline
                View.playableDirector.ReversePlay(OnTutorialClose);
            }
        }

        private void OnTutorialClose()
        {
            View.gameObject.SetActive(false);
            PersistentGameplayData.instance.hasCompletedControlsTutorial = true;
            
            //continue game after closing the tutorial
            if (GameplayStateManager.instance.currentGameState != GameplayState.Active)
            {
                GameplayStateManager.instance.ChangeState(GameplayState.Active);
            }
        }
        

        private enum TutorialState
        {
            Active,
            Inactive
        }
    }
}