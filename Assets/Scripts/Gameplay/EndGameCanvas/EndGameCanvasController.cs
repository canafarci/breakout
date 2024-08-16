using UnityEngine;

using Breakout.CrossScene.Audio;
using Breakout.CrossScene.Enums;
using Breakout.CrossScene.SceneLoading;
using Breakout.Gameplay.Enums;
using Breakout.Gameplay.State;
using Breakout.Infrastructure.Extensions;

namespace Breakout.Gameplay.EndGameCanvas
{
    internal class EndGameCanvasController : MonoBehaviour
    {
        [SerializeField] private EndGameCanvasView View;

        private void Start()
        {
            GameplayStateManager.instance.OnGameplayStateChanged.AddListener(GameplayStateChangedHandler);
            View.mainMenuButton.onClick.AddListener(MainMenuButtonClickHandler);
            View.playableDirector.SetFirstFrame();
        }

        private void MainMenuButtonClickHandler()
        {
            SceneLoader.instance.LoadSceneAsync(SceneID.MainMenu);
        }

        private void GameplayStateChangedHandler(GameplayState newState, GameplayState oldState)
        {
            if (newState == GameplayState.GameWon)
            {
                View.endGameTitleText.SetText("Level Complete!");
                View.playableDirector.Play();
                AudioPlayer.instance.PlayOneShotAudio(AudioClipID.LevelWon);

                EnableCursor();
            }
            else if (newState == GameplayState.GameLost)
            {
                View.endGameTitleText.SetText("You Lose!");
                View.playableDirector.Play();
                AudioPlayer.instance.PlayOneShotAudio(AudioClipID.LevelFail);
                
                EnableCursor();
            }
        }

        private void OnDestroy()
        {
            GameplayStateManager.instance?.OnGameplayStateChanged.RemoveListener(GameplayStateChangedHandler);
            View.mainMenuButton.onClick.RemoveListener(MainMenuButtonClickHandler);
        }
        
        private void EnableCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}