using UnityEngine;

namespace Breakout.Gameplay.Data
{
    [CreateAssetMenu(fileName = "Gameplay Config SO", menuName = "Breakout/Gameplay/Gameplay Config SO", order = 0)]
    public class GameplayConfigSO : ScriptableObject
    {
        [SerializeField] private float PaddleSpeed;
        [SerializeField] private Vector2 InitialBallVelocity;
        [SerializeField] private float BallDirectionChangeBounceNormalWeight;
        [SerializeField] private int PlayerStartingHealth;
        
        internal float paddleSpeed => PaddleSpeed;
        internal Vector2 initialBallVelocity => InitialBallVelocity;
        internal float ballDirectionChangeBounceNormalWeight => BallDirectionChangeBounceNormalWeight;
        internal int playerStartingHealth => PlayerStartingHealth;
    }
}