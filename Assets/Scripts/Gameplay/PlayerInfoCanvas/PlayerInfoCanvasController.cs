using UnityEngine;

using Breakout.Gameplay.State;

namespace Breakout.Gameplay.PlayerInfoCanvas
{
    internal class PlayerInfoCanvasController : MonoBehaviour
    {
        [SerializeField] private PlayerInfoCanvasView _view;

        private void Start()
        {
            PlayerManager.instance.OnPlayerHealthChanged.AddListener(PlayerHealthChangedHandler);
            GameManager.instance.OnPlayerScoreChanged.AddListener(PlayerEarnedScoreHandler);
            SetPlayerHealthText(PlayerManager.instance.playerHealth);
        }

        private void PlayerEarnedScoreHandler(int playerScore)
        {
            string newScoreText = $"Score: {playerScore}";
            _view.scoreText.SetText(newScoreText);
        }

        private void PlayerHealthChangedHandler(int playerNewHealth)
        {
            SetPlayerHealthText(playerNewHealth);
        }

        private void SetPlayerHealthText(int playerNewHealth)
        {
            string newHealthText = $"x{playerNewHealth}";
            _view.healthText.SetText(newHealthText);
        }

        private void OnDestroy()
        {
            PlayerManager.instance?.OnPlayerHealthChanged.RemoveListener(PlayerHealthChangedHandler);
            GameManager.instance?.OnPlayerScoreChanged.RemoveListener(PlayerEarnedScoreHandler);
        }
    }
}