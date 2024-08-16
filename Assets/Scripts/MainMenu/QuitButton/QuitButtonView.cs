using UnityEngine;
using UnityEngine.UI;

namespace Breakout.MainMenu.MainMenu.QuitButton
{
    internal class QuitButtonView : MonoBehaviour
    {
        [SerializeField] private Button Button;

        public Button button => Button;
    }
}