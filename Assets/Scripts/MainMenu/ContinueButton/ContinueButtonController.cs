using System;
using System.Collections.Generic;

using Breakout.CrossScene.Data;
using Breakout.CrossScene.Enums;
using Breakout.CrossScene.SceneLoading;
using Breakout.Infrastructure.Lifecycle;

namespace Breakout.MainMenu.MainMenu.ContinueButton
{
    internal class ContinueButtonController : IInitializable, IDisposable
    {
        private readonly ContinueButtonMediator _mediator;
        private readonly Dictionary<int, SceneID> _sceneIDToCompletedLevelLookup = new Dictionary<int, SceneID>()
                                                                                   {
                                                                                       { 1, SceneID.Level1 },
                                                                                       { 2, SceneID.Level2 },
                                                                                       { 3, SceneID.Level3 },
                                                                                   };
        
        internal ContinueButtonController(ContinueButtonMediator mediator)
        {
            _mediator = mediator;
        }
        
        public void Initialize()
        {
            SetUpButtonAppearance();

            _mediator.OnContinueButtonClicked += ContinueButtonClickedHandler;
        }
        
        //if user hasnt completed a level yet, make continue button text "Start"
        private void SetUpButtonAppearance()
        {
            int lastCompletedLevel = PersistentGameplayData.instance.lastCompletedLevel;
            
            if (lastCompletedLevel == 0)
            {
                _mediator.ChangeButtonTextToStart();
            }
        }

        private void ContinueButtonClickedHandler()
        {
            int lastCompletedLevel = PersistentGameplayData.instance.lastCompletedLevel;
            SceneID sceneIDToLoad = _sceneIDToCompletedLevelLookup[++lastCompletedLevel];
            SceneLoader.instance.LoadSceneAsync(sceneIDToLoad);
            
            //if player has reached the maximum level count in game, loop the level count from the start
            if (lastCompletedLevel == GameDataHolder.instance.levelCountInGame)
            {
                lastCompletedLevel = 0;
            }
        }

        public void Dispose()
        {
            _mediator.OnContinueButtonClicked -= ContinueButtonClickedHandler;
        }
    }
}