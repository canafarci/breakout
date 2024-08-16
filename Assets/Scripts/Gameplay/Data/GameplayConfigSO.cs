using UnityEngine;

namespace Breakout.Gameplay.Data
{
    [CreateAssetMenu(fileName = "Gameplay Config SO", menuName = "Breakout/Gameplay/Gameplay Config SO", order = 0)]
    public class GameplayConfigSO : ScriptableObject
    {
        public float PaddleSpeed;
        public Vector2 InitialBallVelocity;
        public float BallDirectionChangeBounceNormalWeight;
    }
}