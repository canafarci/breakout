using UnityEngine;

using Breakout.CrossScene.Enums;
using Breakout.CrossScene.SceneLoading;

namespace Breakout.Startup
{
    public class StartupEntryPoint : MonoBehaviour
    {
        private void Start()
        {
            SceneLoader.instance.LoadSceneAsync(SceneID.MainMenu);
        }
    }
}