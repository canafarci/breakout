using UnityEngine;

using System;

using Breakout.CrossScene.Audio;
using Breakout.CrossScene.Enums;
using Breakout.Infrastructure.Lifecycle;

namespace Breakout.MainMenu.MainMenu.QuitButton
{
    internal class QuitButtonMediator : IInitializable, IDisposable
    {
        private readonly QuitButtonView _view;

        internal QuitButtonMediator(QuitButtonView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _view.button.onClick.AddListener(QuitButtonClickedHandler);
        }

        private void QuitButtonClickedHandler()
        {
            AudioPlayer.instance.PlayOneShotAudio(AudioClipID.ButtonClick);
            Application.Quit();
        }

        public void Dispose()
        {
            _view.button.onClick.RemoveAllListeners();
        }
    }
}