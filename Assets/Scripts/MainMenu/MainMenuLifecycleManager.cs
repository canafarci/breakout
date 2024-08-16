using UnityEngine;

using System;
using System.Collections.Generic;

using Breakout.CrossScene.SceneLoading;
using Breakout.Infrastructure.Lifecycle;
using Breakout.MainMenu.MainMenu.ContinueButton;
using Breakout.MainMenu.MainMenu.QuitButton;

namespace Breakout.MainMenu.MainMenu
{
    //this class manages lifecycles of MVC implementations of main menu buttons.
    //this class initializes and cleans up resources when OnDestroy from Unity lifecycle is triggered
    //creation logic can be further separated by creating a factory class for these types
    internal class MainMenuLifecycleManager : MonoBehaviour
    {
        [SerializeField] private ContinueButtonView ContinueButtonView;
        [SerializeField] private QuitButtonView QuitButtonView;

        private readonly List<IInitializable> _initializables = new();
        private readonly List<IDisposable> _disposables = new();

        private void Awake()
        {
            SceneLoader.instance.OnSceneLoadStarted.AddListener(SceneLoadStartedHandler);
        }
        
        internal void SetUpMainMenu()
        {
            SetUpContinueButton();
            SetUpQuitButton();
            Initialize();
        }

        private void Initialize()
        {
            _initializables.ForEach(x => x.Initialize());
        }

        private void SetUpContinueButton()
        {
            ContinueButtonMediator continueButtonMediator = new ContinueButtonMediator(ContinueButtonView);
            AddReferencesToLifecycleMethods(continueButtonMediator, continueButtonMediator);

            ContinueButtonController continueButtonController = new ContinueButtonController(continueButtonMediator);
            AddReferencesToLifecycleMethods(continueButtonController, continueButtonController);
        }

        private void AddReferencesToLifecycleMethods(IInitializable initializable, IDisposable disposable)
        {
            _initializables.Add(initializable);
            _disposables.Add(disposable);
        }
        
        private void SetUpQuitButton()
        {
            QuitButtonMediator quitButtonMediator = new QuitButtonMediator(QuitButtonView);
            AddReferencesToLifecycleMethods(quitButtonMediator, quitButtonMediator);
        }
        
        //cleanup
        private void SceneLoadStartedHandler()
        {
            _disposables.ForEach(x => x.Dispose());
        }
        
        private void OnDestroy()
        {
            SceneLoader.instance.OnSceneLoadStarted.RemoveListener(SceneLoadStartedHandler);
        }
    }
}