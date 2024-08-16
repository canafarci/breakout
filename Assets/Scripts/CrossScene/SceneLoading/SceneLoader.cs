using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using Breakout.CrossScene.Enums;
using Breakout.Infrastructure;

namespace Breakout.CrossScene.SceneLoading
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        public readonly UnityEvent OnSceneLoadStarted = new UnityEvent();
        
        public void LoadSceneAsync(SceneID sceneID)
        {
            OnSceneLoadStarted?.Invoke();
            
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)sceneID);
            LoadingScreen.instance.Activate(asyncOperation);
        }
    }
}