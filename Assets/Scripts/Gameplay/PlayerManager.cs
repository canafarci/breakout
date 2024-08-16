using UnityEngine;
using UnityEngine.Events;

using Breakout.Gameplay.Data;
using Breakout.Gameplay.Enums;
using Breakout.Gameplay.State;
using Breakout.Infrastructure;

namespace Breakout.Gameplay
{
    internal class PlayerManager : UnitySingleton<PlayerManager>
    {
        [SerializeField] private GameplayConfigSO GameplayConfigSO;

        internal UnityEvent<int> OnPlayerHealthChanged = new();
        internal int playerHealth { get; private set; }

        private void Start()
        {
            SetPlayerHealth(GameplayConfigSO.playerStartingHealth);
        }
        
        internal void ChangePlayerHealth(int changeAmount)
        {
            playerHealth += changeAmount;

            if (playerHealth < 0)
            {
                GameplayStateManager.instance.ChangeState(GameplayState.GameLost);
                return;
            }

            SetPlayerHealth(playerHealth);
        }

        private void SetPlayerHealth(int health)
        {
            playerHealth = health;
            OnPlayerHealthChanged?.Invoke(playerHealth);
        }
    }
}