using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;

using Breakout.CrossScene.Data;
using Breakout.Gameplay.Enums;
using Breakout.Infrastructure;

namespace Breakout.Gameplay.State
{
    internal class GameManager : UnitySingleton<GameManager>
    {
        //could use a foreach loop to search below a transform,
        //however that is computationally expensive
        //although this is more manual labor, it saves computational power in the initialization phase
        [SerializeField] private List<Brick> BricksInGame;
        
        private int _bricksToBeDestroyed;

        internal List<Brick> bricksInGame => BricksInGame;
        internal int playerScore { get; private set; }

        internal readonly UnityEvent<int> OnPlayerScoreChanged = new();
        
        private void Start()
        {
            Brick.OnBrickDestroyed += BrickDestroyedHandler;
            _bricksToBeDestroyed = bricksInGame.Count;
        }

        private void BrickDestroyedHandler(Brick brick)
        {
            _bricksToBeDestroyed--;
            playerScore = playerScore + brick.brickDestroyScore;
            
            OnPlayerScoreChanged?.Invoke(playerScore);
            
            if (_bricksToBeDestroyed == 0)
            {
                GameplayStateManager.instance.ChangeState(GameplayState.GameWon);
                PersistentGameplayData.instance.lastCompletedLevel++;
            }
        }

        private void OnDestroy()
        {
            Brick.OnBrickDestroyed -= BrickDestroyedHandler;
        }
    }
}