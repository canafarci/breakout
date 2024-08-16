using UnityEngine.Events;

using Breakout.Gameplay.Enums;
using Breakout.Infrastructure;

namespace Breakout.Gameplay.State
{
    //class to oversee and notify state changes in-game
    internal class GameplayStateManager : UnitySingleton<GameplayStateManager>
    {
        internal UnityEvent<GameplayState, GameplayState> OnGameplayStateChanged = new();

        internal GameplayState currentGameState { get ; private set; }
        
        internal void ChangeState(GameplayState gameplayState)
        {
            GameplayState lastState = currentGameState;
            currentGameState = gameplayState;
            
            OnGameplayStateChanged?.Invoke(currentGameState, lastState);
        }
    }
}