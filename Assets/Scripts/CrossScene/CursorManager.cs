using UnityEngine;

using Breakout.Infrastructure;

namespace Breakout.CrossScene
{
    public class CursorManager : Singleton<CursorManager>
    {
        public void DisableCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        public void EnableCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}