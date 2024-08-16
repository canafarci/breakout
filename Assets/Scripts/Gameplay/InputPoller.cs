using UnityEngine;

using Breakout.Infrastructure;

namespace Breakout.Gameplay
{
    internal class InputPoller : UnitySingleton<InputPoller>
    {
        internal float horizontalInput { get; private set; }
        internal bool fireBallInput { get; private set; }
        internal bool pauseGameInput { get; private set; }
        
        private const string HORIZONTAL_AXIS_NAME = "Horizontal";
        private void Update()
        {
            horizontalInput = Input.GetAxis(HORIZONTAL_AXIS_NAME);
            fireBallInput = Input.GetKeyDown(KeyCode.Space);
            pauseGameInput = Input.GetKeyDown(KeyCode.Escape);
        }
    }
}