using UnityEngine;

using TMPro;

namespace Breakout.Gameplay.PlayerInfoCanvas
{
    internal class PlayerInfoCanvasView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ScoreText;
        [SerializeField] private TextMeshProUGUI HealthText;
        [SerializeField] private Animator ScoreTextAnimator;
        [SerializeField] private Animator HealthTextAnimator;

        internal TextMeshProUGUI scoreText => ScoreText;
        internal TextMeshProUGUI healthText => HealthText;
        internal Animator healthTextAnimator => HealthTextAnimator;
        internal Animator scoreTextAnimator => ScoreTextAnimator;
    }
}