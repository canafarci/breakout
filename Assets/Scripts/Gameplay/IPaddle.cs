using UnityEngine;

namespace Breakout.Gameplay
{
    public interface IPaddle
    {
        void ChangePaddleSize(Vector2 targetSize);
    }
}