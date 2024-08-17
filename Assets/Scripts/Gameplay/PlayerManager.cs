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

        internal UnityEvent<int, int> OnPlayerHealthChanged = new();
        internal int playerHealth { get; private set; }

        private void Start()
        {
            playerHealth = GameplayConfigSO.playerStartingHealth;
        }
        
        internal void ChangePlayerHealth(int changeAmount)
        {
            int lastHealth = playerHealth;
            playerHealth += changeAmount;

            if (playerHealth < 0)
            {
                GameplayStateManager.instance.ChangeState(GameplayState.GameLost);
                return;
            }

            OnPlayerHealthChanged?.Invoke(playerHealth, lastHealth);
        }
    }
}