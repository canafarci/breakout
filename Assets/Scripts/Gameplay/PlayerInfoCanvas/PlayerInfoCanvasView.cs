using UnityEngine;

using TMPro;

namespace Breakout.Gameplay.PlayerInfoCanvas
{
    internal class PlayerInfoCanvasView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ScoreText;
        [SerializeField] private TextMeshProUGUI HealthText;

        public TextMeshProUGUI scoreText => ScoreText;
        public TextMeshProUGUI healthText => HealthText;
    }
}