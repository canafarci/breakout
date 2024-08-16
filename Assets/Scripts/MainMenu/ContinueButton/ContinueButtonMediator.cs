using Breakout.CrossScene.Audio;
using Breakout.CrossScene.Enums;
using Breakout.Infrastructure.Lifecycle;

using System;

namespace Breakout.MainMenu.MainMenu.ContinueButton
{
    internal class ContinueButtonMediator : IDisposable, IInitializable
    {
        private readonly ContinueButtonView _view;
        internal event Action OnContinueButtonClicked;

        internal ContinueButtonMediator(ContinueButtonView view)
        {
            _view = view;
        }
        
        internal void ChangeButtonTextToStart()
        {
            string startButtonText = "Start";
            _view.buttonText.SetText(startButtonText);
        }
        
        public void Initialize()
        {
            _view.continueButton.onClick.AddListener(ContinueButtonClicked);
        }

        private void ContinueButtonClicked()
        {
            OnContinueButtonClicked?.Invoke();
            AudioPlayer.instance.PlayOneShotAudio(AudioClipID.ButtonClick);
        }
        
        public void Dispose()
        {
            _view.continueButton.onClick.RemoveAllListeners();
        }
    }
}