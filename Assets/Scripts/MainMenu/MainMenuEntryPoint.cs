using UnityEngine;

using Breakout.CrossScene.Audio;
using Breakout.CrossScene.Enums;

using Breakout.CrossScene.SceneLoading;

namespace Breakout.MainMenu.MainMenu
{
    internal class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private MainMenuLifecycleManager LifecycleManager;
        
        private void Start()
        {
            EnableCursor();
            
            AudioPlayer.instance.PlayMusic(AudioClipID.MainMenuMusic);
            
            LifecycleManager.SetUpMainMenu();
            
            LoadingScreen.instance.DisableLoadingScreen();
        }
        
        private void EnableCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}