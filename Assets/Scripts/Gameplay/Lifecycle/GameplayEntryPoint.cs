﻿using UnityEngine;

using Breakout.CrossScene.Audio;
using Breakout.CrossScene.Enums;
using Breakout.CrossScene.SceneLoading;
using Breakout.Gameplay.Enums;
using Breakout.Gameplay.State;

namespace Breakout.Gameplay.Lifecycle
{
    internal class GameplayEntryPoint : MonoBehaviour
    {
        private void Start()
        {
            DisableCursor();
            
            AudioPlayer.instance.PlayMusic(AudioClipID.GameplayMusic);
            
            LoadingScreen.instance.OnLoadingScreenClosed.AddListener(Initialize);
            LoadingScreen.instance.DisableLoadingScreen();
        }

        private void DisableCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Initialize()
        {
            GameplayStateManager.instance.ChangeState(GameplayState.WaitingToStart);
            LoadingScreen.instance.DisableLoadingScreen();
            
            //TODO add tutorial to show controls
            
            GameplayStateManager.instance.ChangeState(GameplayState.Active);
        }

        private void OnDestroy()
        {
            LoadingScreen.instance.OnLoadingScreenClosed.RemoveListener(Initialize);
        }
    }
}