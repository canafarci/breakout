using UnityEngine;

using Breakout.CrossScene;
using Breakout.CrossScene.Enums;
using Breakout.CrossScene.SceneLoading;
using Breakout.Gameplay.Enums;
using Breakout.Gameplay.State;
using Breakout.Infrastructure.Extensions;

namespace Breakout.Gameplay.PauseCanvas
{
    internal class PauseCanvasController : MonoBehaviour
    {
        [SerializeField] private PauseCanvasView PauseCanvasView;
        
        private PauseCanvasState _pauseCanvasState = PauseCanvasState.Inactive;

        private void Awake()
        {
            PauseCanvasView.continueButton.onClick.AddListener(OnContinueButtonClicked);
            PauseCanvasView.mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            PauseCanvasView.showControlsButton.onClick.AddListener(OnShowControlsButtonClicked);
        }

        private void Start()
        {
            //for some reason, timeline is not initialized in its first frame.
            //this extension forces it to be in the initial frame
            PauseCanvasView.playableDirector.SetFirstFrame();
        }

        private void Update()
        {
            if (_pauseCanvasState == PauseCanvasState.Inactive && InputPoller.instance.pauseGameInput)
            {
                CursorManager.instance.EnableCursor();
                
                _pauseCanvasState = PauseCanvasState.Active;
                GameplayStateManager.instance.ChangeState(GameplayState.Paused);
                PauseCanvasView.playableDirector.Play();
            }
        }

        private void OnShowControlsButtonClicked()
        {
            PauseCanvasView.playableDirector.ReversePlay(OnOpenControls);
        }

        private void OnOpenControls()
        {
            _pauseCanvasState = PauseCanvasState.Inactive;
            PauseCanvasView.tutorialController.Initialize();
            
            CursorManager.instance.DisableCursor();
        }

        private void OnMainMenuButtonClicked()
        {
            SceneLoader.instance.LoadSceneAsync(SceneID.MainMenu);
        }

        private void OnContinueButtonClicked()
        {
            _pauseCanvasState = PauseCanvasState.Inactive;
            PauseCanvasView.playableDirector.ReversePlay(OnContinueGame);
        }
        
        private void OnContinueGame()
        {
            CursorManager.instance.DisableCursor();
                
            if (GameplayStateManager.instance.currentGameState != GameplayState.Active)
            {
                GameplayStateManager.instance.ChangeState(GameplayState.Active);
            }
        }
        
        private void OnDestroy()
        {
            PauseCanvasView.continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            PauseCanvasView.mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            PauseCanvasView.showControlsButton.onClick.RemoveListener(OnShowControlsButtonClicked);
        }
        

        private enum PauseCanvasState
        {
            Active,
            Inactive
        }
    }
}