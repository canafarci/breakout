using System;
using Breakout.Infrastructure;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Breakout.CrossScene.SceneLoading
{
    public class LoadingScreen : UnitySingleton<LoadingScreen>
    {
        [SerializeField] private PlayableDirector Timeline;
        [SerializeField] private Image LoadingBarFillImage;

        private float _currentAfterLoadingFinishAnimationDuration;
        private LoadingScreenState _loadingScreenState = LoadingScreenState.Disabled;
        private AsyncOperation _loadOperation;

        private const float AFTER_LOADING_FINISH_ANIMATION_DURATION = 1f;
        
        public UnityEvent OnLoadingScreenClosed = new UnityEvent();
        
        public void Activate(AsyncOperation loadOperation)
        {
            Initialize(loadOperation);
            //change state
            _loadingScreenState = LoadingScreenState.Active;
            gameObject.SetActive(true);
        }
        
        private void Initialize(AsyncOperation loadOperation)
        {
            //play timeline animation
            Timeline.Play();
            //set handle for fill image percent
            _loadOperation = loadOperation;
            //reset after animation finish duration timer
            _currentAfterLoadingFinishAnimationDuration = AFTER_LOADING_FINISH_ANIMATION_DURATION;
            //reset fill image
            LoadingBarFillImage.fillAmount = 0f;
        }
        
        //calling disable changes state in the loading screen, as we visually continue increasing fill amount
        //loading screen actually closes after fill image animation ends
        public void DisableLoadingScreen()
        {
            _loadingScreenState = LoadingScreenState.WaitingToBeDisabled;
        }
        
        //do lerps in FixedUpdate for equal timesteps
        private void FixedUpdate()
        {
            if (_loadingScreenState == LoadingScreenState.Disabled) return;

            if (!_loadOperation.isDone)
            {
                float progress = _loadOperation.progress;

                // Lerp fill amount towards target progress
                LerpProgressBar(progress);
                return;
            }
            
            //async operation finishes at 90%, lerp to full value for a while
            if (_currentAfterLoadingFinishAnimationDuration > 0)
            {
                _currentAfterLoadingFinishAnimationDuration -= Time.fixedDeltaTime;
                // Lerp fill amount towards target progress
                LerpProgressBar(1f);
                return;
            }
            
            //after all these steps, if loading screen is waiting to be disabled, deactivate it
            if (_loadingScreenState == LoadingScreenState.WaitingToBeDisabled)
            {
                Disable();
            }
        }
        
        //disable and notify the app that loading screen has been closed
        private void Disable()
        {
            _loadingScreenState = LoadingScreenState.Disabled;
            gameObject.SetActive(false);
            OnLoadingScreenClosed?.Invoke();
        }
        
        private void LerpProgressBar(float targetProgress)
        {
            LoadingBarFillImage.fillAmount = Mathf.Lerp(LoadingBarFillImage.fillAmount, targetProgress,
                                                         Time.fixedDeltaTime * 10f);
        }
        
        private enum LoadingScreenState
        {
            Disabled,
            WaitingToBeDisabled,
            Active
        }
    }
}