using UnityEngine;

namespace Breakout.Gameplay.Powerups.Commands
{
    //decorator for template command, changes paddle size
    internal class ChangePaddleSizeCommand : PowerupCommand
    {
        private readonly IPaddle _paddle;
        private readonly Vector2 _targetSize;

        internal ChangePaddleSizeCommand(IPaddle paddle, Vector2 targetSize)
        {
            _paddle = paddle;
            _targetSize = targetSize;
        }
        
        public override void Execute()
        {
            _paddle.ChangePaddleSize(_targetSize);
        }
    }
}