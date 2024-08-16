using UnityEngine;

using Breakout.CrossScene;
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
            CursorManager.instance.EnableCursor();
            
            AudioPlayer.instance.PlayMusic(AudioClipID.MainMenuMusic);
            
            LifecycleManager.SetUpMainMenu();
            
            LoadingScreen.instance.DisableLoadingScreen();
        }
        

    }
}