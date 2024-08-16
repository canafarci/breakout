using UnityEngine;

using Breakout.CrossScene.Enums;
using Breakout.CrossScene.SceneLoading;

namespace Breakout.Startup
{
    //all cross scene singletons are initialized in the Startup scene, 
    //this is a good place to initialize classes which need async
    //operations, e.g. analytics, SDKs, etc
    internal class StartupEntryPoint : MonoBehaviour
    {
        private void Start()
        {
            SceneLoader.instance.LoadSceneAsync(SceneID.MainMenu);
        }
    }
}