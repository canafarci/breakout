using UnityEngine;

using Breakout.Gameplay.State;

namespace Breakout.Gameplay.PlayerInfoCanvas
{
    internal class PlayerInfoCanvasController : MonoBehaviour
    {
        [SerializeField] private PlayerInfoCanvasView View;
        
        //cache animation hashes, as it is cheaper to calculate them only one
        private readonly int _healthIncreaseAnimationHash = Animator.StringToHash("HealthIncrease");
        private readonly int _healthDecreaseAnimationhash = Animator.StringToHash("HealthDecrease");
        private readonly int _scoreChangeAnimationhash = Animator.StringToHash("ScoreChange");

        private void Start()
        {
            PlayerManager.instance.OnPlayerHealthChanged.AddListener(PlayerHealthChangedHandler);
            GameManager.instance.OnPlayerScoreChanged.AddListener(PlayerEarnedScoreHandler);
            SetPlayerHealthText(PlayerManager.instance.playerHealth);
        }

        private void PlayerEarnedScoreHandler(int playerScore)
        {
            string newScoreText = $"Score: {playerScore}";
            View.scoreText.SetText(newScoreText);
            View.scoreTextAnimator.SetTrigger(_scoreChangeAnimationhash);
        }

        private void PlayerHealthChangedHandler(int playerNewHealth, int playerLastHealth)
        {
            SetPlayerHealthText(playerNewHealth);
            PlayHealthTextAnimation(playerNewHealth, playerLastHealth);
        }

        private void PlayHealthTextAnimation(int playerNewHealth, int playerLastHealth)
        {
            if (playerNewHealth > playerLastHealth)
            {
                View.healthTextAnimator.SetTrigger(_healthIncreaseAnimationHash);
            }
            else
            {
                View.healthTextAnimator.SetTrigger(_healthDecreaseAnimationhash);
            }
        }

        private void SetPlayerHealthText(int playerNewHealth)
        {
            string newHealthText = $"x{playerNewHealth}";
            View.healthText.SetText(newHealthText);
        }

        private void OnDestroy()
        {
            PlayerManager.instance?.OnPlayerHealthChanged.RemoveListener(PlayerHealthChangedHandler);
            GameManager.instance?.OnPlayerScoreChanged.RemoveListener(PlayerEarnedScoreHandler);
        }
    }
}